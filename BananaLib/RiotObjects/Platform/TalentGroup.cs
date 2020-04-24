
// Type: BananaLib.RiotObjects.Platform.TalentGroup



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.TalentGroup")]
  [Serializable]
  public class TalentGroup
  {
    [SerializedName("index")]
    public int Index { get; set; }

    [SerializedName("talentRows")]
    public List<TalentRow> TalentRows { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("tltGroupId")]
    public int TltGroupId { get; set; }
  }
}
