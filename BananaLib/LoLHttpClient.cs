
// Type: BananaLib.LoLHttpClient



using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BananaLib
{
  public class LoLHttpClient : HttpClient
  {
    public LoLHttpClient()
      : base((HttpMessageHandler) LoLHttpClient.Handler())
    {
      this.DefaultRequestHeaders.Add("Referer", "app:/LolClient.swf/[[DYNAMIC]]/9");
      this.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows; U; en-US) AppleWebKit/533.19.4 (KHTML, like Gecko) AdobeAIR/21.0");
      this.DefaultRequestHeaders.Add("x-flash-version", "21,0,0,174");
      this.DefaultRequestHeaders.Add("Accept", "text/xml, application/xml, application/xhtml+xml, text/html;q=0.9, text/plain;q=0.8, text/css, image/png, image/jpeg, image/gif;q=0.8, application/x-shockwave-flash, video/mp4;q=0.9, flv-application/octet-stream;q=0.8, video/x-flv;q=0.7, audio/mp4, application/futuresplash, */*;q=0.5, application/x-mpegURL");
    }

    private static HttpClientHandler Handler()
    {
      return new HttpClientHandler()
      {
        UseCookies = true,
        CookieContainer = new CookieContainer()
      };
    }

    public Task<HttpResponseMessage> PostAsync(string url, string data, bool isJson = false)
    {
      string str = isJson ? "application/json" : "application/x-www-form-urlencoded";
      StringContent stringContent = new StringContent(data);
      stringContent.Headers.Clear();
      stringContent.Headers.Add("Content-type", str);
      return this.PostAsync(url, (HttpContent) stringContent);
    }
  }
}
