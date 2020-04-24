
// Type: BananaLib.RiotObjects.Platform.Talent



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.Talent")]
  [Serializable]
  public class Talent
  {
    [SerializedName("index")]
    public int Index { get; set; }

    [SerializedName("level5Desc")]
    public string Level5Desc { get; set; }

    [SerializedName("minLevel")]
    public int MinLevel { get; set; }

    [SerializedName("maxRank")]
    public int MaxRank { get; set; }

    [SerializedName("level4Desc")]
    public string Level4Desc { get; set; }

    [SerializedName("tltId")]
    public int TltId { get; set; }

    [SerializedName("level3Desc")]
    public string Level3Desc { get; set; }

    [SerializedName("talentGroupId")]
    public int TalentGroupId { get; set; }

    [SerializedName("gameCode")]
    public int GameCode { get; set; }

    [SerializedName("minTier")]
    public int MinTier { get; set; }

    [SerializedName("prereqTalentGameCode")]
    public object PrereqTalentGameCode { get; set; }

    [SerializedName("level2Desc")]
    public string Level2Desc { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("talentRowId")]
    public int TalentRowId { get; set; }

    [SerializedName("level1Desc")]
    public string Level1Desc { get; set; }
  }
}
