
// Type: BananaLib.RiotObjects.Team.PlayerDto



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.PlayerDTO")]
  [Serializable]
  public class PlayerDto
  {
    [SerializedName("playerId")]
    public double PlayerId { get; set; }

    [SerializedName("teamsSummary")]
    public List<object> TeamsSummary { get; set; }

    [SerializedName("createdTeams")]
    public List<object> CreatedTeams { get; set; }

    [SerializedName("playerTeams")]
    public List<object> PlayerTeams { get; set; }
  }
}
