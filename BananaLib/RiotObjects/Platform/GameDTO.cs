
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.GameDTO")]
  [Serializable]
  public class GameDTO
  {
    [SerializedName("id")]
    public double Id { get; set; }

    [SerializedName("mapId")]
    public int MapId { get; set; }

    [SerializedName("ownerSummary")]
    public PlayerParticipant OwnerSummary { get; set; }

    [SerializedName("gameState")]
    public string GameState { get; set; }

    [SerializedName("gameStateString")]
    public string GameStateString { get; set; }

    [SerializedName("teamOne")]
    public List<Participant> TeamOne { get; set; }

    [SerializedName("teamTwo")]
    public List<Participant> TeamTwo { get; set; }

    [SerializedName("passwordSet")]
    public bool PasswordSet { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("queuePosition")]
    public int QueuePosition { get; set; }

    [SerializedName("maxNumPlayers")]
    public int MaxNumPlayers { get; set; }

    [SerializedName("queueTypeName")]
    public string QueueTypeName { get; set; }

    [SerializedName("optimisticLock")]
    public double OptimisticLock { get; set; }

    [SerializedName("gameType")]
    public string GameType { get; set; }

    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("gameTypeConfigId")]
    public int GameTypeConfigId { get; set; }

    [SerializedName("pickTurn")]
    public int PickTurn { get; set; }

    [SerializedName("expiryTime")]
    public double ExpiryTime { get; set; }

    [SerializedName("roomName")]
    public string RoomName { get; set; }

    [SerializedName("joinTimerDuration")]
    public int JoinTimerDuration { get; set; }

    [SerializedName("roomPassword")]
    public string RoomPassword { get; set; }

    [SerializedName("bannedChampions")]
    public List<BannedChampion> BannedChampions { get; set; }

    [SerializedName("banOrder")]
    public List<int> BanOrder { get; set; }

    [SerializedName("playerChampionSelections")]
    public List<PlayerChampionSelectionDTO> PlayerChampionSelections { get; set; }

    [SerializedName("observers")]
    public List<GameObserver> Observers { get; set; }

    [SerializedName("spectatorDelay")]
    public int SpectatorDelay { get; set; }

    [SerializedName("spectatorsAllowed")]
    public string SpectatorsAllowed { get; set; }

    [SerializedName("terminatedCondition")]
    public string TerminatedCondition { get; set; }

    [SerializedName("glmHost")]
    public object GlmHost { get; set; }

    [SerializedName("glmPort")]
    public int GlmPort { get; set; }

    [SerializedName("glmSecurePort")]
    public int GlmSecurePort { get; set; }

    [SerializedName("statusOfParticipants")]
    public string StatusOfParticipants { get; set; }
  }
}
