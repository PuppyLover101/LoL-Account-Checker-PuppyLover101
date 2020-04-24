
// Type: BananaLib.RiotObjects.Platform.PlayerStatSummaries



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.PlayerStatSummaries")]
  [Serializable]
  public class PlayerStatSummaries
  {
    [SerializedName("playerStatSummarySet")]
    public List<PlayerStatSummary> PlayerStatSummarySet { get; set; }

    [SerializedName("userId")]
    public double UserId { get; set; }
  }
}
