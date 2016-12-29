
// Type: BananaLib.RiotObjects.Platform.Player



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Player")]
  [Serializable]
  public class Player
  {
    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
