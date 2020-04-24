
using System.ComponentModel;

namespace BananaLib.LoLTypes
{
  public enum GameMode
  {
    [Description("CLASSIC")] SummonersRift = 1,
    [Description("ODIN")] Dominion = 8,
    [Description("CLASSIC")] TwistedTreeline = 10,
    [Description("ARAM")] HowlingAbyss = 12,
    [Description("TUTORIAL")] Tutorial = 13,
  }
}
