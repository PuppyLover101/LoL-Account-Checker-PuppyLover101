
// Type: BananaLib.RiotObjects.Platform.SummaryAggStats



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.SummaryAggStats")]
  [Serializable]
  public class SummaryAggStats
  {
    [SerializedName("statsJson")]
    public object StatsJson { get; set; }

    [SerializedName("stats")]
    public List<SummaryAggStat> Stats { get; set; }
  }
}
