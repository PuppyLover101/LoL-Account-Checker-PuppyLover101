
// Type: BananaLib.RiotObjects.Platform.RawStat



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.RawStat")]
  [Serializable]
  public class RawStat
  {
    [SerializedName("statType")]
    public string StatType { get; set; }

    [SerializedName("value")]
    public double Value { get; set; }
  }
}
