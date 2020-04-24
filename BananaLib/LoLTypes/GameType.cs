
// Type: BananaLib.LoLTypes.GameType



using System.ComponentModel;

namespace BananaLib.LoLTypes
{
  public enum GameType
  {
    [Description("RANKED_TEAM_GAME")] RankedTeamGame,
    [Description("RANKED_GAME")] RankedGame,
    [Description("NORMAL_GAME")] NormalGame,
    [Description("GROUPFINDER")] TeamBuilder,
    [Description("CUSTOM_GAME")] CustomGame,
    [Description("TUTORIAL_GAME")] TutorialGame,
    [Description("PRACTICE_GAME")] PracticeGame,
    [Description("RANKED_GAME_SOLO")] RankedGameSolo,
    [Description("COOP_VS_AI")] CoopVsAi,
    [Description("RANKED_GAME_PREMADE")] RankedGamePremade,
  }
}
