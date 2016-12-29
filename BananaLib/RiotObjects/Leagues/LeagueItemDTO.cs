
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Leagues
{
  [SerializedName("com.riotgames.leagues.pojo.LeagueItemDTO")]
  [Serializable]
  public class LeagueItemDTO
  {
    [SerializedName("previousDayLeaguePosition")]
    public int PreviousDayLeaguePosition { get; set; }

    [SerializedName("timeLastDecayMessageShown")]
    public double TimeLastDecayMessageShown { get; set; }

    [SerializedName("hotStreak")]
    public bool HotStreak { get; set; }

    [SerializedName("leagueName")]
    public string LeagueName { get; set; }

    [SerializedName("tier")]
    public string Tier { get; set; }

    [SerializedName("freshBlood")]
    public bool FreshBlood { get; set; }

    [SerializedName("lastPlayed")]
    public double LastPlayed { get; set; }

    [SerializedName("playerOrTeamId")]
    public string PlayerOrTeamId { get; set; }

    [SerializedName("playerOrTeamName")]
    public string PlayerOrTeamName { get; set; }

    [SerializedName("leaguePoints")]
    public int LeaguePoints { get; set; }

    [SerializedName("leaguePointsDelta")]
    public double LeaguePointsDelta { get; set; }

    [SerializedName("inactive")]
    public bool Inactive { get; set; }

    [SerializedName("inactivityStatus")]
    public string InactivityStatus { get; set; }

    [SerializedName("timeUntilInactivityStatusChanges")]
    public double TimeUntilInactivityStatusChanges { get; set; }

    [SerializedName("rank")]
    public string Rank { get; set; }

    [SerializedName("veteran")]
    public bool Veteran { get; set; }

    [SerializedName("queueType")]
    public string QueueType { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("timeUntilDecay")]
    public double TimeUntilDecay { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }

    [SerializedName("displayDecayWarning")]
    public bool DisplayDecayWarning { get; set; }

    [SerializedName("demotionWarning")]
    public int demotionWarning { get; set; }

    [SerializedName("miniSeries")]
    public MiniSeriesDTO MiniSeries { get; set; }
  }
}
