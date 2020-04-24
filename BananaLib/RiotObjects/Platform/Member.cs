




using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Member")]
  [Serializable]
  public class Member
  {
    [SerializedName("hasDelegatedInvitePower")]
    public bool hasDelegatedInvitePower { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
