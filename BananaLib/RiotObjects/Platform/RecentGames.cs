
// Type: BananaLib.RiotObjects.Platform.RecentGames



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.RecentGames")]
  [Serializable]
  public class RecentGames
  {
    [SerializedName("recentGamesJson")]
    public object RecentGamesJson { get; set; }

    [SerializedName("playerGameStatsMap")]
    public object PlayerGameStatsMap { get; set; }

    [SerializedName("gameStatistics")]
    public List<PlayerGameStats> GameStatistics { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }
  }
}
