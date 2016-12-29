
// Type: BananaLib.RiotObjects.Team.CreatedTeam



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.CreatedTeam")]
  [Serializable]
  public class CreatedTeam
  {
    [SerializedName("timeStamp")]
    public double TimeStamp { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }
  }
}
