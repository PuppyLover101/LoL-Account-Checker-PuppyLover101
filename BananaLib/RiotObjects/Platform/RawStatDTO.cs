
// Type: BananaLib.RiotObjects.Platform.RawStatDTO



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.statistics.RawStatDTO")]
  [Serializable]
  public class RawStatDTO
  {
    [SerializedName("value")]
    public double Value { get; set; }

    [SerializedName("statTypeName")]
    public string StatTypeName { get; set; }
  }
}
