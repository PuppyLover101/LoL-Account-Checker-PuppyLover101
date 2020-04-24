
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.champion.ChampionDTO")]
  [Serializable]
  public class ChampionDTO
  {
    [SerializedName("searchTags")]
    public string[] SearchTags { get; set; }

    [SerializedName("ownedByYourTeam")]
    public bool OwnedByYourTeam { get; set; }

    [SerializedName("botEnabled")]
    public bool BotEnabled { get; set; }

    [SerializedName("banned")]
    public bool Banned { get; set; }

    [SerializedName("skinName")]
    public string SkinName { get; set; }

    [SerializedName("displayName")]
    public string DisplayName { get; set; }

    [SerializedName("championData")]
    public object ChampionData { get; set; }

    [SerializedName("owned")]
    public bool Owned { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("freeToPlayReward")]
    public bool FreeToPlayReward { get; set; }

    [SerializedName("freeToPlay")]
    public bool FreeToPlay { get; set; }

    [SerializedName("ownedByEnemyTeam")]
    public bool OwnedByEnemyTeam { get; set; }

    [SerializedName("active")]
    public bool Active { get; set; }

    [SerializedName("championSkins")]
    public List<ChampionSkinDTO> ChampionSkins { get; set; }

    [SerializedName("description")]
    public string Description { get; set; }

    [SerializedName("winCountRemaining")]
    public int WinCountRemaining { get; set; }

    [SerializedName("purchaseDate")]
    public double PurchaseDate { get; set; }

    [SerializedName("endDate")]
    public double EndDate { get; set; }
  }
}
