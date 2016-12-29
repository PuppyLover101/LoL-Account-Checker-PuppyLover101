
using RtmpSharp.IO;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.Script.Serialization;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.serviceproxy.dispatch.LcdsServiceProxyResponse")]
  [Serializable]
  public class LcdsServiceProxyResponse
  {
    private string _payload;
    private string _decompressedPayload;

    [SerializedName("compressedPayload")]
    public bool CompressedPayload { get; set; }

    [SerializedName("payload")]
    public string Payload
    {
      get
      {
        if (!this.CompressedPayload)
          return this._payload;
        return this.GetDecompressedPayload();
      }
      set
      {
        this._payload = value;
      }
    }

    [SerializedName("status")]
    public string Status { get; set; }

    [SerializedName("messageId")]
    public string MessageId { get; set; }

    [SerializedName("methodName")]
    public string MethodName { get; set; }

    [SerializedName("serviceName")]
    public string ServiceName { get; set; }

    public string GetCompressedPayload()
    {
      if (!this.CompressedPayload)
        return (string) null;
      return this._payload;
    }

    public string GetDecompressedPayload()
    {
      if (!string.IsNullOrEmpty(this._decompressedPayload))
        return this._decompressedPayload;
      using (MemoryStream memoryStream1 = new MemoryStream(Convert.FromBase64String(this._payload)))
      {
        using (GZipStream gzipStream = new GZipStream((Stream) memoryStream1, CompressionMode.Decompress))
        {
          byte[] buffer = new byte[4096];
          using (MemoryStream memoryStream2 = new MemoryStream())
          {
            int count;
            while ((count = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
              memoryStream2.Write(buffer, 0, count);
            this._decompressedPayload = Encoding.UTF8.GetString(memoryStream2.ToArray());
          }
        }
      }
      return this._decompressedPayload;
    }

    public T GetDeserializedPayload<T>() where T : class, new()
    {
      if (this.Payload == null)
        return Activator.CreateInstance<T>();
      return new JavaScriptSerializer().Deserialize<T>(this.Payload);
    }
  }
}
