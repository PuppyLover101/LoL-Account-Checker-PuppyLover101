using System.Windows;

namespace ImageLoader
{
    public partial class Image : System.Windows.Controls.Image
    {
        public string SourceUri
        {
            get { return (string)base.GetValue(SourceUriProperty); }
            set { base.SetValue(SourceUriProperty, value); }
        }
        
        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.Register(
            "SourceUri",
            typeof(string),
            typeof(ImageLoader.Image),
            new UIPropertyMetadata(string.Empty, OnSourceChanged)
        );

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Manager.Instance.LoadImage(d as ImageLoader.Image);
        }
        
        public bool IsLoading
        {
            get { return (bool)base.GetValue(IsLoadingProperty); }
            set { base.SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(
            "IsLoading",
            typeof(bool),
            typeof(ImageLoader.Image),
            new UIPropertyMetadata(false)
        );

        public bool DisplayWaitingAnimationDuringLoading
        {
            get { return (bool)base.GetValue(DisplayWaitingAnimationDuringLoadingProperty); }
            set { base.SetValue(DisplayWaitingAnimationDuringLoadingProperty, value); }
        }

        public static readonly DependencyProperty DisplayWaitingAnimationDuringLoadingProperty = DependencyProperty.Register(
            "DisplayWaitingAnimationDuringLoading",
            typeof(bool),
            typeof(ImageLoader.Image),
            new UIPropertyMetadata(true)
        );
        public bool ErrorDetected
        {
            get { return (bool)base.GetValue(ErrorDetectedProperty); }
            set { base.SetValue(ErrorDetectedProperty, value); }
        }
        
        public static readonly DependencyProperty ErrorDetectedProperty = DependencyProperty.Register(
            "ErrorDetected",
            typeof(bool),
            typeof(ImageLoader.Image),
            new UIPropertyMetadata(false)
        );

        public bool DisplayErrorThumbnailOnError
        {
            get { return (bool)base.GetValue(DisplayErrorThumbnailOnErrorProperty); }
            set { base.SetValue(DisplayErrorThumbnailOnErrorProperty, value); }
        }

        public static readonly DependencyProperty DisplayErrorThumbnailOnErrorProperty = DependencyProperty.Register(
            "DisplayErrorThumbnailOnError",
            typeof(bool),
            typeof(ImageLoader.Image),
            new UIPropertyMetadata(true)
        );
    }
}
