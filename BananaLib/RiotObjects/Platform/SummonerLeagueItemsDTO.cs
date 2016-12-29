
// Type: BananaLib.RiotObjects.Platform.SummonerLeagueItemsDTO



using BananaLib.RiotObjects.Leagues;
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.leagues.client.dto.SummonerLeagueItemsDTO")]
  [Serializable]
  public class SummonerLeagueItemsDTO
  {
    [SerializedName("summonerLeagues")]
    public List<LeagueItemDTO> SummonerLeagues { get; set; }
  }
}
