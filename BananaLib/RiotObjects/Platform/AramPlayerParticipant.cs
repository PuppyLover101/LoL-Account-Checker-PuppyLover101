
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.reroll.pojo.AramPlayerParticipant")]
  [Serializable]
  public class AramPlayerParticipant : PlayerParticipant
  {
    [SerializedName("pointSummary")]
    public List<PointSummary> pointSummary { get; set; }
  }
}
