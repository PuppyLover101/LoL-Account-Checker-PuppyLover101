
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.ChampionBanInfoDTO")]
  [Serializable]
  public class ChampionBanInfoDTO
  {
    [SerializedName("enemyOwned")]
    public bool EnemyOwned { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("owned")]
    public bool Owned { get; set; }
  }
}
