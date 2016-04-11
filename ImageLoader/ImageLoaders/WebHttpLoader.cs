using System.IO;
using System.Net;

namespace ImageLoader.ImageLoaders
{
    public class WebHttpLoader : ILoader
    {
        private string url;

        public WebHttpLoader(string url)
        {
            this.url = url;
        }

        public byte[] Load()
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Timeout = 5 * 1000;

                byte[] byteStream = null;

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream responseStream = webResponse.GetResponseStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    responseStream.CopyTo(memoryStream);
                    byteStream = memoryStream.ToArray();
                }

                return byteStream;
            }
            catch
            {
                return null;
            }
        }
    }
}