
// Type: BananaLib.RiotObjects.Team.TeamStatSummary



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.stats.TeamStatSummary")]
  [Serializable]
  public class TeamStatSummary
  {
    [SerializedName("teamStatDetails")]
    public List<TeamStatDetail> TeamStatDetails { get; set; }

    [SerializedName("teamIdString")]
    public string TeamIdString { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
