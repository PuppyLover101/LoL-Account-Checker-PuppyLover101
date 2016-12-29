
// Type: BananaLib.RiotObjects.Platform.SummonerActiveBoostsDTO



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.boost.SummonerActiveBoostsDTO")]
  [Serializable]
  public class SummonerActiveBoostsDTO
  {
    [SerializedName("xpBoostEndDate")]
    public double XpBoostEndDate { get; set; }

    [SerializedName("xpBoostPerWinCount")]
    public int XpBoostPerWinCount { get; set; }

    [SerializedName("xpLoyaltyBoost")]
    public int XpLoyaltyBoost { get; set; }

    [SerializedName("ipBoostPerWinCount")]
    public int IpBoostPerWinCount { get; set; }

    [SerializedName("ipLoyaltyBoost")]
    public int IpLoyaltyBoost { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("ipBoostEndDate")]
    public double IpBoostEndDate { get; set; }
  }
}
