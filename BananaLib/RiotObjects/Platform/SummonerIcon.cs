
// Type: BananaLib.RiotObjects.Platform.SummonerIcon



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.icon.SummonerIcon")]
  [Serializable]
  public class SummonerIcon
  {
    [SerializedName("iconId")]
    public int IconId { get; set; }

    [SerializedName("purchaseDate")]
    public DateTime PurchaseDate { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
