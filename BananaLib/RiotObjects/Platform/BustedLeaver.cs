
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.BustedLeaver")]
  [Serializable]
  public class BustedLeaver : FailedJoinPlayer
  {
    [SerializedName("accessToken")]
    public string AccessToken { get; set; }

    [SerializedName("leaverPenaltyMillisRemaining")]
    public double LeaverPenaltyMilisRemaining { get; set; }
  }
}
