
// Type: BananaLib.RiotObjects.Platform.RuneType



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.runes.RuneType")]
  [Serializable]
  public class RuneType
  {
    [SerializedName("runeTypeId")]
    public int RuneTypeId { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }
  }
}
