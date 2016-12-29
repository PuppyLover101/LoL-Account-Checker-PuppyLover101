
// Type: BananaLib.RiotObjects.Platform.SimpleDialogMessageResponse



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.persistence.SimpleDialogMessageResponse")]
  [Serializable]
  public class SimpleDialogMessageResponse
  {
    [SerializedName("command")]
    public string Command { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("msgId")]
    public string MessageId { get; set; }
  }
}
