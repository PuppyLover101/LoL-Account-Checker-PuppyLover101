
// Type: BananaLib.RiotObjects.Platform.QueueInfo



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.QueueInfo")]
  [Serializable]
  public class QueueInfo
  {
    [SerializedName("waitTime")]
    public double WaitTime { get; set; }

    [SerializedName("queueId")]
    public double QueueId { get; set; }

    [SerializedName("queueLength")]
    public int QueueLength { get; set; }
  }
}
