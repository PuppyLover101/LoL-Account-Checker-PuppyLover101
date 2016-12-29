
// Type: BananaLib.RiotObjects.Platform.TeamPlayerAggregatedStatsDTO



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.team.TeamPlayerAggregatedStatsDTO")]
  [Serializable]
  public class TeamPlayerAggregatedStatsDTO
  {
    [SerializedName("playerId")]
    public double PlayerId { get; set; }

    [SerializedName("aggregatedStats")]
    public AggregatedStats AggregatedStats { get; set; }
  }
}
