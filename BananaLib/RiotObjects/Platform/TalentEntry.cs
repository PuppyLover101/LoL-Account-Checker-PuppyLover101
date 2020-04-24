
// Type: BananaLib.RiotObjects.Platform.TalentEntry



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.masterybook.TalentEntry")]
  [Serializable]
  public class TalentEntry
  {
    [SerializedName("rank")]
    public int Rank { get; set; }

    [SerializedName("talentId")]
    public int TalentId { get; set; }

    [SerializedName("talent")]
    public Talent Talent { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
