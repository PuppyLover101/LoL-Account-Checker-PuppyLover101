using System;

namespace ImageLoader.ImageLoaders
{
    internal static class LoaderFactory
    {
        public static ILoader CreateLoader(string sourceUri)
        {
            Uri uri = new Uri(sourceUri);

            if (uri.Scheme.StartsWith("http"))
            {
                return new WebHttpLoader(sourceUri);
            }

            if (uri.Scheme.Equals("file"))
            {
                return new LocalDiskLoader(sourceUri);
            }

            throw new NotImplementedException(
                string.Format("Loader not implemented for Uri scheme={0}", uri.Scheme)
            );
        }
    }
}
