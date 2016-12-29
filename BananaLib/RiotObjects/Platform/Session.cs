
// Type: BananaLib.RiotObjects.Platform.Session



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.login.Session")]
  [Serializable]
  public class Session
  {
    [SerializedName("token")]
    public string Token { get; set; }

    [SerializedName("password")]
    public string Password { get; set; }

    [SerializedName("accountSummary")]
    public AccountSummary AccountSummary { get; set; }
  }
}
