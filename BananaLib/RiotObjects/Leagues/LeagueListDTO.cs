
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Leagues
{
  [SerializedName("com.riotgames.leagues.pojo.LeagueListDTO")]
  [Serializable]
  public class LeagueListDTO
  {
    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("entries")]
    public List<LeagueItemDTO> Entries { get; set; }

    [SerializedName("queue")]
    public string Queue { get; set; }

    [SerializedName("tier")]
    public string Tier { get; set; }

    [SerializedName("requestorsRank")]
    public string RequestorsRank { get; set; }

    [SerializedName("requestorsName")]
    public string RequestorsName { get; set; }

    [SerializedName("maxLeagueSize")]
    public int MaxLeagueSize { get; set; }
  }
}
