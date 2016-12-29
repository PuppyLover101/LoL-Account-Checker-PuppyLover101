
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.matchmaking.GameQueueConfig")]
  [Serializable]
  public class GameQueueConfig
  {
    [SerializedName("blockedMinutesThreshold")]
    public int BlockedMinutesThreshold { get; set; }

    [SerializedName("minimumParticipantListSize")]
    public int MinimumParticipantListSize { get; set; }

    [SerializedName("ranked")]
    public bool Ranked { get; set; }

    [SerializedName("maxLevel")]
    public int MaxLevel { get; set; }

    [SerializedName("minLevel")]
    public int MinLevel { get; set; }

    [SerializedName("gameTypeConfigId")]
    public int GameTypeConfigId { get; set; }

    [SerializedName("thresholdEnabled")]
    public bool ThresholdEnabled { get; set; }

    [SerializedName("queueState")]
    public string QueueState { get; set; }

    [SerializedName("type")]
    public string Type { get; set; }

    [SerializedName("cacheName")]
    public string CacheName { get; set; }

    [SerializedName("id")]
    public double Id { get; set; }

    [SerializedName("queueBonusKey")]
    public string QueueBonusKey { get; set; }

    [SerializedName("queueStateString")]
    public string QueueStateString { get; set; }

    [SerializedName("pointsConfigKey")]
    public string PointsConfigKey { get; set; }

    [SerializedName("teamOnly")]
    public bool TeamOnly { get; set; }

    [SerializedName("minimumQueueDodgeDelayTime")]
    public int MinimumQueueDodgeDelayTime { get; set; }

    [SerializedName("supportedMapIds")]
    public List<int> SupportedMapIds { get; set; }

    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("typeString")]
    public string TypeString { get; set; }

    [SerializedName("numPlayersPerTeam")]
    public int NumPlayersPerTeam { get; set; }

    [SerializedName("maximumParticipantListSize")]
    public int MaximumParticipantListSize { get; set; }

    [SerializedName("disallowFreeChampions")]
    public bool DisallowFreeChampions { get; set; }

    [SerializedName("mapSelectionAlgorithm")]
    public string MapSelectionAlgorithm { get; set; }

    [SerializedName("thresholdSize")]
    public double ThresholdSize { get; set; }

    [SerializedName("matchingThrottleConfig")]
    public MatchingThrottleConfig MatchingThrottleConfig { get; set; }
  }
}
