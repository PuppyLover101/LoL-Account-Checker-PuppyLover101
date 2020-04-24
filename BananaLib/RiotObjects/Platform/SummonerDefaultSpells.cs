
// Type: BananaLib.RiotObjects.Platform.SummonerDefaultSpells



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerDefaultSpells")]
  [Serializable]
  public class SummonerDefaultSpells
  {
    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("summonerDefaultSpellsJson")]
    public object SummonerDefaultSpellsJson { get; set; }

    [SerializedName("summonerDefaultSpellMap")]
    public object SummonerDefaultSpellMap { get; set; }
  }
}
