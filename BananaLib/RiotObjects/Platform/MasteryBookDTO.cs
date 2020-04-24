
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.masterybook.MasteryBookDTO")]
  [Serializable]
  public class MasteryBookDTO
  {
    [SerializedName("bookPagesJson")]
    public object BookPagesJson { get; set; }

    [SerializedName("bookPages")]
    public List<MasteryBookPageDTO> BookPages { get; set; }

    [SerializedName("dateString")]
    public string DateString { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
