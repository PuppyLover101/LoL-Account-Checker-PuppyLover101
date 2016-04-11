using System.IO;

namespace ImageLoader.ImageLoaders
{
    internal class LocalDiskLoader: ILoader
    {
        private string filePath;
        public LocalDiskLoader(string filePath)
        {
            this.filePath = filePath;
        }
        public byte[] Load()
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch
            {
                return null;
            }
        }
    }
}
