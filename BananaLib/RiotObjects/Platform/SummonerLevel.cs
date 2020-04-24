
// Type: BananaLib.RiotObjects.Platform.SummonerLevel



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.SummonerLevel")]
  [Serializable]
  public class SummonerLevel
  {
    [SerializedName("summonerLevel")]
    public int Level { get; set; }

    [SerializedName("summonerTier")]
    public double SummonerTier { get; set; }

    [SerializedName("infTierMod")]
    public double InfTierMod { get; set; }

    [SerializedName("expTierMod")]
    public double ExpTierMod { get; set; }

    [SerializedName("expToNextLevel")]
    public double ExpToNextLevel { get; set; }
  }
}
