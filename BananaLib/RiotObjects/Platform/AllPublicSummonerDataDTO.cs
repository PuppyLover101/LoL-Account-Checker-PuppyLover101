
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.AllPublicSummonerDataDTO")]
  [Serializable]
  public class AllPublicSummonerDataDTO
  {
    [SerializedName("summoner")]
    public BasePublicSummonerDTO Summoner { get; set; }

    [SerializedName("summonerLevelAndPoints")]
    public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

    [SerializedName("summonerTalentsAndPoints")]
    public SummonerTalentsAndPoints SummonerTalentsAndPoints { get; set; }

    [SerializedName("summonerDefaultSpells")]
    public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

    [SerializedName("summonerLevel")]
    public SummonerLevel SummonerLevel { get; set; }

    [SerializedName("spellBook")]
    public SpellBookDTO SpellBook { get; set; }
  }
}
