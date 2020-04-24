
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.catalog.ItemEffect")]
  [Serializable]
  public class ItemEffect
  {
    [SerializedName("effectId")]
    public int EffectId { get; set; }

    [SerializedName("itemEffectId")]
    public int ItemEffectId { get; set; }

    [SerializedName("effect")]
    public Effect Effect { get; set; }

    [SerializedName("value")]
    public string Value { get; set; }

    [SerializedName("itemId")]
    public int ItemId { get; set; }
  }
}
