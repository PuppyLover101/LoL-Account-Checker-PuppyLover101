
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.AggregatedStats")]
  [Serializable]
  public class AggregatedStats
  {
    [SerializedName("lifetimeStatistics")]
    public List<AggregatedStat> LifetimeStatistics { get; set; }

    [SerializedName("modifyDate")]
    public object ModifyDate { get; set; }

    [SerializedName("key")]
    public AggregatedStatsKey Key { get; set; }

    [SerializedName("aggregatedStatsJson")]
    public string AggregatedStatsJson { get; set; }
  }
}
