
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.FailedJoinPlayer")]
  [Serializable]
  public class FailedJoinPlayer
  {
    [SerializedName("summoner")]
    public Summoner Summoner { get; set; }

    [SerializedName("reasonFailed")]
    public string ReasonFailed { get; set; }
  }
}
