
// Type: BananaLib.RiotObjects.Platform.SummonerRuneInventory



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.runes.SummonerRuneInventory")]
  [Serializable]
  public class SummonerRuneInventory
  {
    [SerializedName("summonerRunesJson")]
    public object SummonerRunesJson { get; set; }

    [SerializedName("dateString")]
    public string DateString { get; set; }

    [SerializedName("summonerRunes")]
    public List<SummonerRune> SummonerRunes { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
