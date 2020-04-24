
using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.game.BotParticipant")]
  [Serializable]
  public class BotParticipant : GameParticipant
  {
    [SerializedName("botSkillLevelName")]
    public string BotSkillLevelName { get; set; }

    [SerializedName("botSkillLevel")]
    public double BotSkillLevel { get; set; }

    [SerializedName("teamId")]
    public string TeamId { get; set; }

    [SerializedName("champion")]
    public List<ChampionDTO> Champion { get; set; }
  }
}
