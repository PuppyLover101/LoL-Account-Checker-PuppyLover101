
// Type: BananaLib.RiotObjects.Platform.PlayerChampionSelectionDTO



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlayerChampionSelectionDTO")]
  [Serializable]
  public class PlayerChampionSelectionDTO
  {
    [SerializedName("summonerInternalName")]
    public string SummonerInternalName { get; set; }

    [SerializedName("championId")]
    public int ChampionId { get; set; }

    [SerializedName("selectedSkinIndex")]
    public int SelectedSkinIndex { get; set; }

    [SerializedName("spell1Id")]
    public double Spell1Id { get; set; }

    [SerializedName("spell2Id")]
    public double Spell2Id { get; set; }
  }
}
