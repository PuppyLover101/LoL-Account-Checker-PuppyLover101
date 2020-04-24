
// Type: BananaLib.RiotObjects.Platform.PracticeGameSearchResult



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.practice.PracticeGameSearchResult")]
  [Serializable]
  public class PracticeGameSearchResult
  {
    [SerializedName("spectatorCount")]
    public int SpectatorCount { get; set; }

    [SerializedName("glmGameId")]
    public object GlmGameId { get; set; }

    [SerializedName("glmHost")]
    public object GlmHost { get; set; }

    [SerializedName("glmPort")]
    public int GlmPort { get; set; }

    [SerializedName("gameModeString")]
    public string GameModeString { get; set; }

    [SerializedName("allowSpectators")]
    public string AllowSpectators { get; set; }

    [SerializedName("gameMapId")]
    public int GameMapId { get; set; }

    [SerializedName("maxNumPlayers")]
    public int MaxNumPlayers { get; set; }

    [SerializedName("glmSecurePort")]
    public int GlmSecurePort { get; set; }

    [SerializedName("gameMode")]
    public string GameMode { get; set; }

    [SerializedName("id")]
    public double Id { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("privateGame")]
    public bool PrivateGame { get; set; }

    [SerializedName("owner")]
    public PlayerParticipant Owner { get; set; }

    [SerializedName("team1Count")]
    public int Team1Count { get; set; }

    [SerializedName("team2Count")]
    public int Team2Count { get; set; }
  }
}
