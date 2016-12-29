
using RtmpSharp.IO;
using RtmpSharp.IO.AMF3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.broadcast.BroadcastNotification")]
  [Serializable]
  public class BroadcastNotification : IExternalizable
  {
    public ArrayList broadcastMessages { get; set; }

    public string Json { get; set; }

    public void ReadExternal(IDataInput input)
    {
      this.Json = input.ReadUtf((int) input.ReadUInt32());
      Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(this.Json);
      Type type = typeof (BroadcastNotification);
      foreach (KeyValuePair<string, object> keyValuePair in dictionary)
        type.GetProperty(keyValuePair.Key).SetValue((object) this, keyValuePair.Value);
    }

    public void WriteExternal(IDataOutput output)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(this.Json);
      output.WriteInt32(bytes.Length);
      output.WriteBytes(bytes);
    }
  }
}
