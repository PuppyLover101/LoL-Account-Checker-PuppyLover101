
// Type: BananaLib.RiotObjects.Platform.SearchingForMatchNotification



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.SearchingForMatchNotification")]
  [Serializable]
  public class SearchingForMatchNotification
  {
    [SerializedName("playerJoinFailures")]
    public List<FailedJoinPlayer> PlayerJoinFailures { get; set; }

    [SerializedName("ghostGameSummoners")]
    public object GhostGameSummoners { get; set; }

    [SerializedName("joinedQueues")]
    public List<QueueInfo> JoinedQueues { get; set; }
  }
}