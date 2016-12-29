
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.AggregatedStat")]
  [Serializable]
  public class AggregatedStat
  {
    [SerializedName("statType")]
    public string StatType { get; set; }

    [SerializedName("count")]
    public double Count { get; set; }

    [SerializedName("value")]
    public double Value { get; set; }

    [SerializedName("championId")]
    public double ChampionId { get; set; }
  }
}
