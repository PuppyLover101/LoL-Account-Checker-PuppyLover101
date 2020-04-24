
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.harassment.LcdsResponseString")]
  [Serializable]
  public class LcdsResponseString
  {
    [SerializedName("value")]
    public string Value { get; set; }
  }
}
