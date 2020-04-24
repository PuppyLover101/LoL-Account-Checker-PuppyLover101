
// Type: BananaLib.RiotObjects.Team.RosterDto



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.dto.RosterDTO")]
  [Serializable]
  public class RosterDto
  {
    [SerializedName("ownerId")]
    public double OwnerId { get; set; }

    [SerializedName("memberList")]
    public List<TeamMemberInfoDto> MemberList { get; set; }
  }
}
