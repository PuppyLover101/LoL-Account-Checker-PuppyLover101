
// Type: BananaLib.RiotObjects.Platform.StartChampSelectDTO



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.StartChampSelectDTO")]
  [Serializable]
  public class StartChampSelectDTO
  {
    [SerializedName("invalidPlayers")]
    public List<object> InvalidPlayers { get; set; }
  }
}
