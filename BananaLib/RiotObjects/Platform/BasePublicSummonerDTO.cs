
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.BasePublicSummonerDTO")]
  [Serializable]
  public class BasePublicSummonerDTO
  {
    [SerializedName("sumId")]
    public double SumId { get; set; }

    [SerializedName("acctId")]
    public double AccountId { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("publicName")]
    public string InternalName { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }

    [SerializedName("previousSeasonHighestTier")]
    public string PreviousSeasonHighestTier { get; set; }
  }
}
