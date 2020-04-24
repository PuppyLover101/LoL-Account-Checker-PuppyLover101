
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.ClientLoginKickNotification")]
  [Serializable]
  public class ClientLoginKickNotification
  {
    [SerializedName("sessionToken")]
    public string sessionToken { get; set; }
  }
}
