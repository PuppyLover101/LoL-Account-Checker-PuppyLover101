
// Type: BananaLib.RiotObjects.Platform.SpellBookPageDTO



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.spellbook.SpellBookPageDTO")]
  [Serializable]
  public class SpellBookPageDTO
  {
    [SerializedName("slotEntries")]
    public List<SlotEntry> SlotEntries { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("createDate")]
    public DateTime CreateDate { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("pageId")]
    public int PageId { get; set; }

    [SerializedName("current")]
    public bool Current { get; set; }
  }
}
