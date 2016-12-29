
// Type: BananaLib.RiotObjects.Platform.SummonerLevelAndPoints



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerLevelAndPoints")]
  [Serializable]
  public class SummonerLevelAndPoints
  {
    [SerializedName("infPoints")]
    public double InfPoints { get; set; }

    [SerializedName("expPoints")]
    public double ExpPoints { get; set; }

    [SerializedName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
