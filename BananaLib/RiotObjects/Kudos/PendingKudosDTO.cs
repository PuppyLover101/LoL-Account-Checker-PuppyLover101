
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Kudos
{
  [SerializedName("com.riotgames.kudos.dto.PendingKudosDTO")]
  [Serializable]
  public class PendingKudosDTO
  {
    [SerializedName("pendingCounts")]
    public int[] PendingCounts { get; set; }
  }
}
