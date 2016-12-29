
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.MatchingThrottleConfig")]
  [Serializable]
  public class MatchingThrottleConfig
  {
    [SerializedName("limit")]
    public double Limit { get; set; }

    [SerializedName("matchingThrottleProperties")]
    public List<object> MatchingThrottleProperties { get; set; }

    [SerializedName("cacheName")]
    public string CacheName { get; set; }
  }
}
