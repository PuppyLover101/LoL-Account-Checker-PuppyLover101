
// Type: BananaLib.RiotObjects.Platform.SummonerTalentsAndPoints



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerTalentsAndPoints")]
  [Serializable]
  public class SummonerTalentsAndPoints
  {
    [SerializedName("talentPoints")]
    public int TalentPoints { get; set; }

    [SerializedName("modifyDate")]
    public DateTime ModifyDate { get; set; }

    [SerializedName("createDate")]
    public DateTime CreateDate { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
