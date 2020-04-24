
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.GameTypeConfigDTO")]
  [Serializable]
  public class GameTypeConfigDTO
  {
    [SerializedName("id")]
    public int Id { get; set; }

    [SerializedName("allowTrades")]
    public bool AllowTrades { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("mainPickTimerDuration")]
    public int MainPickTimerDuration { get; set; }

    [SerializedName("exclusivePick")]
    public bool ExclusivePick { get; set; }

    [SerializedName("duplicatePick")]
    public bool DuplicatePick { get; set; }

    [SerializedName("teamChampionPool")]
    public bool TeamChampionPool { get; set; }

    [SerializedName("pickMode")]
    public string PickMode { get; set; }

    [SerializedName("maxAllowableBans")]
    public int MaxAllowableBans { get; set; }

    [SerializedName("banTimerDuration")]
    public int BanTimerDuration { get; set; }

    [SerializedName("postPickTimerDuration")]
    public int PostPickTimerDuration { get; set; }

    [SerializedName("crossTeamChampionPool")]
    public bool CrossTeamChampionPool { get; set; }
  }
}
