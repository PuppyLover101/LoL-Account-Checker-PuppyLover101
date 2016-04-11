using System;
using System.Threading;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Concurrent;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using ImageLoader.ImageLoaders;

namespace ImageLoader
{
    internal sealed class Manager
    {
        internal class LoadImageRequest
        {
            public string SourceUri { get; set; }
            public Image Image { get; set; }
        }

        #region Properties
        private const int MaxThreads = 3;
        private const int ItemCacheExpirationMinutes = 5;
        
        private readonly AutoResetEvent _loaderThreadEvent = new AutoResetEvent(false);
        private readonly ConcurrentStack<LoadImageRequest> _loadQueue = new ConcurrentStack<LoadImageRequest>();

        private readonly ObjectCache _cache = MemoryCache.Default;

        private static readonly SHA1Managed Sha1Managed = new SHA1Managed();
        private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding();

        private readonly DrawingImage _loadingImage;
        private readonly DrawingImage _errorThumbnail;
        private readonly TransformGroup _loadingAnimationTransform;
        #endregion

        #region Singleton Implementation
        private static Manager _instance;
        private Manager()
        {
            #region Create Loading Threads
            for (int i = 0; i < MaxThreads; i++)
            {
                Thread loaderThread = new Thread(LoaderWork)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal,
                    Name = $"ImageLoaderThread{i + 1}"
                };
                loaderThread.Start();
            }
            #endregion

            #region Load Images from Resources
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("ImageLoader;component/Resources.xaml", UriKind.Relative);
            _loadingImage = resourceDictionary["ImageLoading"] as DrawingImage;
            _loadingImage?.Freeze();
            _errorThumbnail = resourceDictionary["ImageError"] as DrawingImage;
            _errorThumbnail?.Freeze();
            #endregion

            # region Create Loading Animation
            ScaleTransform scaleTransform = new ScaleTransform(0.5, 0.5);
            SkewTransform skewTransform = new SkewTransform(0, 0);
            RotateTransform rotateTransform = new RotateTransform(0);
            TranslateTransform translateTransform = new TranslateTransform(0, 0);

            TransformGroup group = new TransformGroup();
            group.Children.Add(scaleTransform);
            group.Children.Add(skewTransform);
            group.Children.Add(rotateTransform);
            group.Children.Add(translateTransform);

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 359, new TimeSpan(0, 0, 0, 1))
            {
                RepeatBehavior = RepeatBehavior.Forever
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);

            _loadingAnimationTransform = group;
            #endregion
        }
        public static Manager Instance => _instance ?? (_instance = new Manager());
        #endregion

        #region Public Methods
        public void LoadImage(Image image)
        {
            LoadImageRequest loadTask = new LoadImageRequest()
            {
                Image = image,
                SourceUri = image.SourceUri
            };

            BeginLoading(image);

            _loadQueue.Push(loadTask);

            _loaderThreadEvent.Set();
        }
        #endregion

        #region Private Methods
        private void BeginLoading(Image image)
        {
            image.Dispatcher.BeginInvoke(new Action(delegate
            {
                image.IsLoading = true;
                image.ErrorDetected = false;

                if (Equals(image.RenderTransform, Transform.Identity) && image.DisplayWaitingAnimationDuringLoading)
                {
                    image.Source = _loadingImage;
                    image.RenderTransformOrigin = new Point(0.5, 0.5);
                    image.RenderTransform = _loadingAnimationTransform;
                }
            }));
        }

        private void EndLoading(Image image, ImageSource imageSource)
        {
            image.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (Equals(image.RenderTransform, _loadingAnimationTransform))
                {
                    image.RenderTransform = Transform.Identity;
                }

                if (image.ErrorDetected && image.DisplayErrorThumbnailOnError)
                {
                    imageSource = _errorThumbnail;
                    image.Stretch = Stretch.Uniform;
                }

                image.Source = imageSource;
                image.IsLoading = false;
            }));
        }

        private ImageSource GetBitmapSource(LoadImageRequest loadTask)
        {
            Image image = loadTask.Image;
            string sourceUri = loadTask.SourceUri;

            if (string.IsNullOrWhiteSpace(sourceUri))
            {
                SetError(image);
                return null;
            }

            double width = double.NaN;
            double height = double.NaN;

            image.Dispatcher.Invoke(new ThreadStart(delegate
            {
                width = image.Width;
                height = image.Height;
            }));

            string cacheKey = ComputeHash(sourceUri);

            byte[] byteImage = _cache.Get(cacheKey) as byte[];

            if (byteImage == null)
            {
                byteImage = LoaderFactory.CreateLoader(sourceUri).Load();

                if (byteImage != null)
                {
                    try
                    {
                        _cache.Set(
                            cacheKey,
                            byteImage,
                            new CacheItemPolicy() { 
                                SlidingExpiration = new TimeSpan(0, 0, ItemCacheExpirationMinutes, 0, 0)
                            }
                        );
                    }
                    catch
                    {

                    }
                }
            }

            if (byteImage == null)
            {
                SetError(image);
                return null;
            }
            
            try
            {
                using (MemoryStream memStream = new MemoryStream(byteImage))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    if (!double.IsNaN(width) && !double.IsNaN(height))
                    {
                        bitmapImage.DecodePixelWidth = (int)width;
                        bitmapImage.DecodePixelHeight = (int)height;
                    }
                    bitmapImage.StreamSource = memStream;
                    bitmapImage.EndInit();

                    bitmapImage.Freeze();

                    return bitmapImage;
                }
            }
            catch
            {
                SetError(image);
                return null;
            }
        }

        private static void SetError(Image image)
        {
            image.Dispatcher.BeginInvoke(new Action(delegate
            {
                image.ErrorDetected = true;
            }));
        }

        private static string ComputeHash(string s)
        {
            var hash = Sha1Managed.ComputeHash(Utf8Encoding.GetBytes(s.ToCharArray()));
            return BitConverter.ToString(hash).Replace("-", "");
        }

        private void LoaderWork()
        {
            while (true)
            {
                _loaderThreadEvent.WaitOne();

                LoadImageRequest loadTask = null;

                while (_loadQueue.TryPop(out loadTask))
                {
                    ImageSource bitmapSource = GetBitmapSource(loadTask);
                    EndLoading(loadTask.Image, bitmapSource);
                }
            }
        }
        #endregion
    }
}
