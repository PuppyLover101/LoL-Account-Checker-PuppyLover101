
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.Effect")]
  [Serializable]
  public class Effect
  {
    [SerializedName("effectId")]
    public int EffectId { get; set; }

    [SerializedName("gameCode")]
    public string GameCode { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("categoryId")]
    public object CategoryId { get; set; }

    [SerializedName("runeType")]
    public RuneType RuneType { get; set; }
  }
}
