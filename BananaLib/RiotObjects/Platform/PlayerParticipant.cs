
// Type: BananaLib.RiotObjects.Platform.PlayerParticipant



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlayerParticipant")]
  [Serializable]
  public class PlayerParticipant : GameParticipant
  {
    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [SerializedName("clientInSynch")]
    public bool ClientInSynch { get; set; }
  }
}
