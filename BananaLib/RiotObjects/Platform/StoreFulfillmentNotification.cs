
// Type: BananaLib.RiotObjects.Platform.StoreFulfillmentNotification



using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.messaging.StoreFulfillmentNotification")]
  [Serializable]
  public class StoreFulfillmentNotification
  {
    [SerializedName("rp")]
    public double Rp { get; set; }

    [SerializedName("ip")]
    public double Ip { get; set; }

    [SerializedName("inventoryType")]
    public string InventoryType { get; set; }

    [SerializedName("data")]
    public object Data { get; set; }
  }
}
