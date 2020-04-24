
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.message.GameNotification")]
  [Serializable]
  public class GameNotification
  {
    [SerializedName("messageCode")]
    public string MessageCode { get; set; }

    [SerializedName("type")]
    public string Type { get; set; }

    [SerializedName("messageArgument")]
    public object MessageArgument { get; set; }
  }
}
