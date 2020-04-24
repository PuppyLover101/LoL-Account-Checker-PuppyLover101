
// Type: BananaLib.RiotObjects.Platform.SlotEntry



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.spellbook.SlotEntry")]
  [Serializable]
  public class SlotEntry
  {
    [SerializedName("runeId")]
    public int RuneId { get; set; }

    [SerializedName("runeSlotId")]
    public int RuneSlotId { get; set; }
  }
}
