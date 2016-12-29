
// Type: BananaLib.RiotObjects.Platform.SummonerCatalog



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerCatalog")]
  [Serializable]
  public class SummonerCatalog
  {
    [SerializedName("items")]
    public object Items { get; set; }

    [SerializedName("talentTree")]
    public List<TalentGroup> TalentTree { get; set; }

    [SerializedName("spellBookConfig")]
    public List<RuneSlot> SpellBookConfig { get; set; }
  }
}
