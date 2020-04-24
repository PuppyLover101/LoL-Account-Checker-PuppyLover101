
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.masterybook.MasteryBookPageDTO")]
  [Serializable]
  public class MasteryBookPageDTO
  {
    [SerializedName("talentEntries")]
    public List<TalentEntry> TalentEntries { get; set; }

    [SerializedName("pageId")]
    public double PageId { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("current")]
    public bool Current { get; set; }

    [SerializedName("createDate")]
    public object CreateDate { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
