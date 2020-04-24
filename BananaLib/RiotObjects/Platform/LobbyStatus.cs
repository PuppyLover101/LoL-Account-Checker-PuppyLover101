
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.LobbyStatus")]
  [Serializable]
  public class LobbyStatus
  {
    [SerializedName("chatKey")]
    public string ChatKey { get; set; }

    [SerializedName("gameMetaData")]
    public string GameData { get; set; }

    [SerializedName("owner")]
    public Player Owner { get; set; }

    [SerializedName("members")]
    public List<Member> Members { get; set; }

    [SerializedName("invitees")]
    public List<Invitee> Invitees { get; set; }

    [SerializedName("invitationId")]
    public string InvitationID { get; set; }
  }
}
