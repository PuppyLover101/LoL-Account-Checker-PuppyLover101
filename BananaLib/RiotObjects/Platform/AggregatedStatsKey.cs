
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.AggregatedStatsKey")]
  [Serializable]
  public class AggregatedStatsKey
  {
    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }

    [SerializedName("gameModeString")]
    public string GameModeString { get; set; }
  }
}
