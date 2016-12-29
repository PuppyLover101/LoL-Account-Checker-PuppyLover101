
using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Leagues
{
  [SerializedName("com.riotgames.leagues.pojo.MiniSeriesDTO")]
  [Serializable]
  public class MiniSeriesDTO
  {
    [SerializedName("progress")]
    public object Progress { get; set; }

    [SerializedName("target")]
    public int Target { get; set; }

    [SerializedName("losses")]
    public int Losses { get; set; }

    [SerializedName("timeLeftToPlayMillis")]
    public double TimeLeftToPlayMillis { get; set; }

    [SerializedName("wins")]
    public int Wins { get; set; }
  }
}
