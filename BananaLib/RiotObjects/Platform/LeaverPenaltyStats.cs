
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.LeaverPenaltyStats")]
  [Serializable]
  public class LeaverPenaltyStats
  {
    [SerializedName("lastLevelIncrease")]
    public object LastLevelIncrease { get; set; }

    [SerializedName("level")]
    public int Level { get; set; }

    [SerializedName("lastDecay")]
    public DateTime LastDecay { get; set; }

    [SerializedName("userInformed")]
    public bool UserInformed { get; set; }

    [SerializedName("points")]
    public int Points { get; set; }
  }
}
