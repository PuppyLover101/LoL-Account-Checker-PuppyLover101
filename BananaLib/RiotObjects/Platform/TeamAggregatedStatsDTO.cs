
// Type: BananaLib.RiotObjects.Platform.TeamAggregatedStatsDTO



using BananaLib.RiotObjects.Team;
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.team.TeamAggregatedStatsDTO")]
  [Serializable]
  public class TeamAggregatedStatsDTO
  {
    [SerializedName("queueType")]
    public string QueueType { get; set; }

    [SerializedName("serializedToJson")]
    public string SerializedToJson { get; set; }

    [SerializedName("playerAggregatedStatsList")]
    public List<TeamPlayerAggregatedStatsDTO> PlayerAggregatedStatsList { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
