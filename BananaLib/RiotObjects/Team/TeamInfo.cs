
// Type: BananaLib.RiotObjects.Team.TeamInfo



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.TeamInfo")]
  [Serializable]
  public class TeamInfo
  {
    [SerializedName("secondsUntilEligibleForDeletion")]
    public double SecondsUntilEligibleForDeletion { get; set; }

    [SerializedName("memberStatusString")]
    public string MemberStatusString { get; set; }

    [SerializedName("tag")]
    public string Tag { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("memberStatus")]
    public string MemberStatus { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
