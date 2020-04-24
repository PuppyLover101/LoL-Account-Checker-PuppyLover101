
// Type: BananaLib.RiotObjects.Platform.SummonerLeaguesDTO



using BananaLib.RiotObjects.Leagues;
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.leagues.client.dto.SummonerLeaguesDTO")]
  [Serializable]
  public class SummonerLeaguesDTO
  {
    [SerializedName("summonerLeagues")]
    public List<LeagueListDTO> SummonerLeagues { get; set; }
  }
}
