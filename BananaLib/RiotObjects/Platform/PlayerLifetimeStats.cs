
// Type: BananaLib.RiotObjects.Platform.PlayerLifetimeStats



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerLifetimeStats")]
  [Serializable]
  public class PlayerLifetimeStats
  {
    [SerializedName("playerStatSummaries")]
    public PlayerStatSummaries PlayerStatSummaries { get; set; }

    [SerializedName("leaverPenaltyStats")]
    public LeaverPenaltyStats LeaverPenaltyStats { get; set; }

    [SerializedName("previousFirstWinOfDay")]
    public DateTime PreviousFirstWinOfDay { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }

    [SerializedName("dodgeStreak")]
    public int DodgeStreak { get; set; }

    [SerializedName("dodgePenaltyDate")]
    public object DodgePenaltyDate { get; set; }

    [SerializedName("playerStatsJson")]
    public object PlayerStatsJson { get; set; }

    [SerializedName("playerStats")]
    public PlayerStats PlayerStats { get; set; }
  }
}
