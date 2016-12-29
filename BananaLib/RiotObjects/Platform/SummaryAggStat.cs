
// Type: BananaLib.RiotObjects.Platform.SummaryAggStat



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.SummaryAggStat")]
  [Serializable]
  public class SummaryAggStat
  {
    [SerializedName("statType")]
    public string StatType { get; set; }

    [SerializedName("count")]
    public double Count { get; set; }

    [SerializedName("value")]
    public double Value { get; set; }
  }
}
