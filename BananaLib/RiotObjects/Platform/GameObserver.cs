
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.GameObserver")]
  [Serializable]
  public class GameObserver
  {
    [SerializedName("accountId")]
    public double AccountId { get; set; }

    [SerializedName("botDifficulty")]
    public string BotDifficulty { get; set; }

    [SerializedName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [SerializedName("locale")]
    public object Locale { get; set; }

    [SerializedName("lastSelectedSkinIndex")]
    public int LastSelectedSkinIndex { get; set; }

    [SerializedName("partnerId")]
    public string PartnerId { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }

    [SerializedName("badges")]
    public int Badges { get; set; }

    [SerializedName("pickTurn")]
    public int PickTurn { get; set; }

    [SerializedName("originalAccountId")]
    public double OriginalAccountId { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("pickMode")]
    public int PickMode { get; set; }

    [SerializedName("originalPlatformId")]
    public string OriginalPlatformId { get; set; }
  }
}
