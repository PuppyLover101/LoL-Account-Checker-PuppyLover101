
// Type: BananaLib.RiotObjects.Platform.PointSummary



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.reroll.pojo.PointSummary")]
  [Serializable]
  public class PointSummary
  {
    [SerializedName("pointsToNextRoll")]
    public double PointsToNextRoll { get; set; }

    [SerializedName("maxRolls")]
    public int MaxRolls { get; set; }

    [SerializedName("numberOfRolls")]
    public int NumberOfRolls { get; set; }

    [SerializedName("pointsCostToRoll")]
    public double PointsCostToRoll { get; set; }

    [SerializedName("currentPoints")]
    public double CurrentPoints { get; set; }
  }
}
