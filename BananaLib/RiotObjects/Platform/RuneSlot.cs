
// Type: BananaLib.RiotObjects.Platform.RuneSlot



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.RuneSlot")]
  [Serializable]
  public class RuneSlot
  {
    [SerializedName("id")]
    public int Id { get; set; }

    [SerializedName("minLevel")]
    public int MinLevel { get; set; }

    [SerializedName("runeType")]
    public RuneType RuneType { get; set; }
  }
}
