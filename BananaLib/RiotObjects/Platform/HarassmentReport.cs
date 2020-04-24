
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.harassment.HarassmentReport")]
  [Serializable]
  public class HarassmentReport
  {
    [SerializedName("reportedSummonerId")]
    public double SummonerID { get; set; }

    [SerializedName("ipAddress")]
    public double IPAddress { get; set; }

    [SerializedName("gameId")]
    public double GameID { get; set; }

    [SerializedName("reportSource")]
    public string ReportSource { get; set; }

    [SerializedName("comment")]
    public string Comment { get; set; }

    [SerializedName("reportingSummonerId")]
    public double ReportingSummonerID { get; set; }

    [SerializedName("offense")]
    public string Offense { get; set; }
  }
}
