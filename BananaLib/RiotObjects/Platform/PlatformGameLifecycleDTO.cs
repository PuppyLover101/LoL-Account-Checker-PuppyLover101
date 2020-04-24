
// Type: BananaLib.RiotObjects.Platform.PlatformGameLifecycleDTO



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.PlatformGameLifecycleDTO")]
  [Serializable]
  public class PlatformGameLifecycleDTO
  {
    [SerializedName("gameSpecificLoyaltyRewards")]
    public object GameSpecificLoyaltyRewards { get; set; }

    [SerializedName("reconnectDelay")]
    public int ReconnectDelay { get; set; }

    [SerializedName("lastModifiedDate")]
    public object LastModifiedDate { get; set; }

    [SerializedName("game")]
    public GameDTO Game { get; set; }

    [SerializedName("playerCredentials")]
    public PlayerCredentialsDto PlayerCredentials { get; set; }

    [SerializedName("gameName")]
    public string GameName { get; set; }

    [SerializedName("connectivityStateEnum")]
    public object ConnectivityStateEnum { get; set; }
  }
}
