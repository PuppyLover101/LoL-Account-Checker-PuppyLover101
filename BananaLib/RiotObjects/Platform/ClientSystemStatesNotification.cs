
using RtmpSharp.IO;
using RtmpSharp.IO.AMF3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.systemstate.ClientSystemStatesNotification")]
  [Serializable]
  public class ClientSystemStatesNotification : IExternalizable
  {
    public bool championTradeThroughLCDS { get; set; }

    public bool practiceGameEnabled { get; set; }

    public bool advancedTutorialEnabled { get; set; }

    public int[] practiceGameTypeConfigIdList { get; set; }

    public int minNumPlayersForPracticeGame { get; set; }

    public int[] PracticeGameTypeConfigIdList { get; set; }

    public int[] freeToPlayChampionIdList { get; set; }

    public int[] inactiveChampionIdList { get; set; }

    public int[] inactiveSpellIdList { get; set; }

    public int[] inactiveTutorialSpellIdList { get; set; }

    public int[] inactiveClassicSpellIdList { get; set; }

    public int[] inactiveOdinSpellIdList { get; set; }

    public int[] inactiveAramSpellIdList { get; set; }

    public int[] enabledQueueIdsList { get; set; }

    public int[] unobtainableChampionSkinIDList { get; set; }

    public int[] freeToPlayChampionForNewPlayersIdList { get; set; }

    public Dictionary<string, object> gameModeToInactiveSpellIds { get; set; }

    public bool archivedStatsEnabled { get; set; }

    public Dictionary<string, object> queueThrottleDTO { get; set; }

    public Dictionary<string, object>[] gameMapEnabledDTOList { get; set; }

    public bool storeCustomerEnabled { get; set; }

    public bool socialIntegrationEnabled { get; set; }

    public bool runeUniquePerSpellBook { get; set; }

    public bool tribunalEnabled { get; set; }

    public bool observerModeEnabled { get; set; }

    public int currentSeason { get; set; }

    public int freeToPlayChampionsForNewPlayersMaxLevel { get; set; }

    public int spectatorSlotLimit { get; set; }

    public int clientHeartBeatRateSeconds { get; set; }

    public string[] observableGameModes { get; set; }

    public string observableCustomGameModes { get; set; }

    public bool teamServiceEnabled { get; set; }

    public bool leagueServiceEnabled { get; set; }

    public bool modularGameModeEnabled { get; set; }

    public Decimal riotDataServiceDataSendProbability { get; set; }

    public bool displayPromoGamesPlayedEnabled { get; set; }

    public bool masteryPageOnServer { get; set; }

    public int maxMasteryPagesOnServer { get; set; }

    public bool tournamentSendStatsEnabled { get; set; }

    public string replayServiceAddress { get; set; }

    public bool kudosEnabled { get; set; }

    public bool buddyNotesEnabled { get; set; }

    public bool localeSpecificChatRoomsEnabled { get; set; }

    public Dictionary<string, object> replaySystemStates { get; set; }

    public bool sendFeedbackEventsEnabled { get; set; }

    public string[] knownGeographicGameServerRegions { get; set; }

    public bool leaguesDecayMessagingEnabled { get; set; }

    public bool tournamentShortCodesEnabled { get; set; }

    public string Json { get; set; }

    public void ReadExternal(IDataInput input)
    {
      this.Json = input.ReadUtf((int) input.ReadUInt32());
      Dictionary<string, object> dictionary = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(this.Json);
      Type type = typeof (ClientSystemStatesNotification);
      foreach (KeyValuePair<string, object> keyValuePair in dictionary)
      {
        try
        {
          PropertyInfo property = type.GetProperty(keyValuePair.Key);
          if (!(property == (PropertyInfo) null))
          {
            if (keyValuePair.Value.GetType() == typeof (ArrayList))
            {
              ArrayList arrayList = keyValuePair.Value as ArrayList;
              if (arrayList != null && arrayList.Count > 0)
                property.SetValue((object) this, (object) ((ArrayList) keyValuePair.Value).ToArray(arrayList[0].GetType()));
              else
                property.SetValue((object) this, (object) null);
            }
            else
              property.SetValue((object) this, keyValuePair.Value);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error: {0}", (object) ex);
        }
      }
    }

    public void WriteExternal(IDataOutput output)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(this.Json);
      output.WriteInt32(bytes.Length);
      output.WriteBytes(bytes);
    }
  }
}
