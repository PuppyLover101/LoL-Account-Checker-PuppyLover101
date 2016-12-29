
// Type: BananaLib.RiotObjects.Platform.SummonerIconInventoryDTO



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.icon.SummonerIconInventoryDTO")]
  [Serializable]
  public class SummonerIconInventoryDTO
  {
    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("summonerIcons")]
    public List<SummonerIcon> SummonerIcons { get; set; }
  }
}
