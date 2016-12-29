

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.ChampionStatInfo")]
  [Serializable]
  public class ChampionStatInfo
  {
    [SerializedName("totalGamesPlayed")]
    public int TotalGamesPlayed { get; set; }

    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("stats")]
    public List<AggregatedStat> Stats { get; set; }

    [SerializedName("championId")]
    public double ChampionId { get; set; }
  }
}
