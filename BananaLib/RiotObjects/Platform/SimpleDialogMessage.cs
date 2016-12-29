
// Type: BananaLib.RiotObjects.Platform.SimpleDialogMessage



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.persistence.SimpleDialogMessage")]
  [Serializable]
  public class SimpleDialogMessage
  {
    [SerializedName("titleCode")]
    public string TitleCode { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("params")]
    public object Params { get; set; }

    [SerializedName("msgId")]
    public string MessageId { get; set; }

    [SerializedName("type")]
    public string Type { get; set; }

    [SerializedName("bodyCode")]
    public string BodyCode { get; set; }
  }
}
