
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.gameinvite.contract.InvitePrivileges")]
  [Serializable]
  public class InvitePrivileges
  {
    [SerializedName("canInvite")]
    public bool canInvite { get; set; }
  }
}
