
// Type: BananaLib.RiotObjects.Team.TeamStatDetail



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Team
{
  [SerializedName("com.riotgames.team.stats.TeamStatDetail")]
  [Serializable]
  public class TeamStatDetail
  {
    [SerializedName("maxRating")]
    public int MaxRating { get; set; }

    [SerializedName("teamIdString")]
    public string TeamIdString { get; set; }

    [SerializedName("seedRating")]
    public int SeedRating { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("rating")]
    public int Rating { get; set; }

    [SerializedName("teamStatTypeString")]
    public string TeamStatTypeString { get; set; }

    [SerializedName("averageGamesPlayed")]
    public int AverageGamesPlayed { get; set; }

    [SerializedName("teamId")]
    public TeamId TeamId { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }

    [SerializedName("teamStatType")]
    public string TeamStatType { get; set; }
  }
}
