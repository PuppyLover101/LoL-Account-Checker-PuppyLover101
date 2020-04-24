
// Type: BananaLib.RiotObjects.Team.TeamId



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.TeamId")]
  [Serializable]
  public class TeamId
  {
    [SerializedName("fullId")]
    public string FullId { get; set; }
  }
}
