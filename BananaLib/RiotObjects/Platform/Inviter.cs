
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Inviter")]
  [Serializable]
  public class Inviter
  {
    [SerializedName("previousSeasonHighestTier")]
    public string PreviousSeasonHighestTier { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
