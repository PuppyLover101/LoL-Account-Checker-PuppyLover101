
// Type: BananaLib.RiotObjects.Platform.SpellBookDTO



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.spellbook.SpellBookDTO")]
  [Serializable]
  public class SpellBookDTO
  {
    [SerializedName("bookPagesJson")]
    public object BookPagesJson { get; set; }

    [SerializedName("bookPages")]
    public List<SpellBookPageDTO> BookPages { get; set; }

    [SerializedName("dateString")]
    public string DateString { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("defaultPage")]
    public SpellBookPageDTO DefaultPage { get; set; }
  }
}
