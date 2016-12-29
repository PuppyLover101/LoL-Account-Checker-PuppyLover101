
using RtmpSharp.IO;

namespace BananaLib.RiotObjects.Platform
{
  public class BaseSummoner
  {
    [SerializedName("sumId")]
    public double SummonerId { get; set; }

    [SerializedName("acctId")]
    public double AccountId { get; set; }

    [SerializedName("name")]
    public string Name { get; set; }

    [SerializedName("publicName")]
    public string InternalName { get; set; }

    [SerializedName("profileIconId")]
    public int ProfileIconId { get; set; }
  }
}
