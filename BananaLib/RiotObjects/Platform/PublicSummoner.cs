
// Type: BananaLib.RiotObjects.Platform.PublicSummoner



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.PublicSummoner")]
  [Serializable]
  public class PublicSummoner : BaseSummoner
  {
    [SerializedName("summonerLevel")]
    public double SummonerLevel { get; set; }

    [SerializedName("summonerAssociatedTalents")]
    public List<object> SummonerAssociatedTalents { get; set; }
  }
}
