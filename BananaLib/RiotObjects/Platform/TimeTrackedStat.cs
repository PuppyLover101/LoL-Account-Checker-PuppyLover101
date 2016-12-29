
// Type: BananaLib.RiotObjects.Platform.TimeTrackedStat



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.TimeTrackedStat")]
  [Serializable]
  public class TimeTrackedStat
  {
    [SerializedName("timestamp")]
    public DateTime Timestamp { get; set; }

    [SerializedName("type")]
    public string Type { get; set; }
  }
}
