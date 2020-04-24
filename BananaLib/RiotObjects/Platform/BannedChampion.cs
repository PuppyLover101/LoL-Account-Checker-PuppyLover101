
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.BannedChampion")]
  [Serializable]
  public class BannedChampion
  {
    [SerializedName("pickTurn")]
    public int PickTurn { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("teamId")]
    public int TeamId { get; set; }
  }
}
