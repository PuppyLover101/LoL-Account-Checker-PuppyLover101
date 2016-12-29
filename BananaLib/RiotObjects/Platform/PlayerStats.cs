
// Type: BananaLib.RiotObjects.Platform.PlayerStats



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerStats")]
  [Serializable]
  public class PlayerStats
  {
    [SerializedName("timeTrackedStats")]
    public List<TimeTrackedStat> TimeTrackedStats { get; set; }

    [SerializedName("promoGamesPlayed")]
    public int PromoGamesPlayed { get; set; }

    [SerializedName("promoGamesPlayedLastUpdated")]
    public object PromoGamesPlayedLastUpdated { get; set; }
  }
}
