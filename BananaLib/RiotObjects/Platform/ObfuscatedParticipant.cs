
// Type: BananaLib.RiotObjects.Platform.ObfuscatedParticipant



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.ObfuscatedParticipant")]
  [Serializable]
  public class ObfuscatedParticipant : Participant
  {
    [SerializedName("badges")]
    public int Badges { get; set; }

    [SerializedName("clientInSynch")]
    public bool ClientInSynch { get; set; }

    [SerializedName("gameUniqueId")]
    public int GameUniqueId { get; set; }

    [SerializedName("pickMode")]
    public int PickMode { get; set; }
  }
}
