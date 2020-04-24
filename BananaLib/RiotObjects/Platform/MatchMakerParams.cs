
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.MatchMakerParams")]
  [Serializable]
  public class MatchMakerParams
  {
    [SerializedName("lastMaestroMessage")]
    public object LastMaestroMessage { get; set; }

    [SerializedName("teamId")]
    public object TeamId { get; set; }

    [SerializedName("languages")]
    public object Languages { get; set; }

    [SerializedName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [SerializedName("team")]
    public List<int> Team { get; set; }

    [SerializedName("queueIds")]
    public int[] QueueIds { get; set; }

    [SerializedName("invitationId")]
    public object InvitationId { get; set; }
  }
}
