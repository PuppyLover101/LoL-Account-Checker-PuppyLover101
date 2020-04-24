
// Type: BananaLib.RiotObjects.Platform.QueueDodger



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.QueueDodger")]
  [Serializable]
  public class QueueDodger : FailedJoinPlayer
  {
    [SerializedName("dodgePenaltyRemainingTime")]
    public int PenaltyRemainingTime { get; set; }
  }
}
