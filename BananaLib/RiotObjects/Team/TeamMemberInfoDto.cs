
// Type: BananaLib.RiotObjects.Team.TeamMemberInfoDto



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.TeamMemberInfoDTO")]
  [Serializable]
  public class TeamMemberInfoDto
  {
    [SerializedName("joinDate")]
    public DateTime JoinDate { get; set; }

    [SerializedName("playerName")]
    public string PlayerName { get; set; }

    [SerializedName("inviteDate")]
    public DateTime InviteDate { get; set; }

    [SerializedName("status")]
    public string Status { get; set; }

    [SerializedName("playerId")]
    public double PlayerId { get; set; }
  }
}
