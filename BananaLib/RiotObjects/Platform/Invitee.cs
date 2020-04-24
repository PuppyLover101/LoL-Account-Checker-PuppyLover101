
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.Invitee")]
  [Serializable]
  public class Invitee
  {
    [SerializedName("inviteeStateAsString")]
    public string InviteeState { get; set; }

    [SerializedName("summonerName")]
    public string SummonerName { get; set; }

    [SerializedName("inviteeState")]
    public string inviteeState { get; set; }

    [SerializedName("summonerId")]
    public double SummonerId { get; set; }
  }
}
