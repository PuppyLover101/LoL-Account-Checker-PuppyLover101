
// Type: BananaLib.RiotObjects.Platform.Summoner



using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.Summoner")]
  [Serializable]
  public class Summoner : BaseSummoner
  {
    [SerializedName("revisionId")]
    public double RevisionId { get; set; }

    [SerializedName("revisionDate")]
    public DateTime RevisionDate { get; set; }

    [SerializedName("lastGameDate")]
    public DateTime LastGameDate { get; set; }

    [SerializedName("socialNetworkUserIds")]
    public List<object> SocialNetworkUserIds { get; set; }

    [SerializedName("previousSeasonHighestTier")]
    public string PreviousSeasonHighestTier { get; set; }

    [SerializedName("previousSeasonHighestTeamReward")]
    public int PreviousSeasonHighestTeamReward { get; set; }

    [SerializedName("tutorialFlag")]
    public bool TutorialFlag { get; set; }

    [SerializedName("helpFlag")]
    public bool HelpFlag { get; set; }

    [SerializedName("displayEloQuestionaire")]
    public bool DisplayEloQuestionaire { get; set; }

    [SerializedName("nameChangeFlag")]
    public bool NameChangeFlag { get; set; }

    [SerializedName("advancedTutorialFlag")]
    public bool AdvancedTutorialFlag { get; set; }
  }
}
