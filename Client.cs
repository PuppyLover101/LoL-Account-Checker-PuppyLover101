#region License

// Copyright 2015 LoLAccountChecker
// 
// This file is part of LoLAccountChecker.
// 
// LoLAccountChecker is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// LoLAccountChecker is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License 
// along with LoLAccountChecker. If not, see http://www.gnu.org/licenses/.

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LoLAccountChecker.Classes;
using PVPNetClient;
using PVPNetClient.RiotObjects.Platform;
using RtmpSharp.IO;
using RtmpSharp.Net;

#endregion

namespace LoLAccountChecker
{
    public class Client
    {
        private readonly PvpClient _pvpnet;
        public Account Data;

        public TaskCompletionSource<bool> IsCompleted;

        public Client(Region region, string username, string password, string ip, SerializationContext context)
        {
            Data = new Account
            {
                Username = username,
                Password = password,
                Region = region,
                Refunds = 0
            };

            IsCompleted = new TaskCompletionSource<bool>();

            _pvpnet = new PvpClient(username, password, region, Settings.Config.ClientVersion)
            {
                SerializationContext = context,
                LoLIpAddress = ip
            };
            _pvpnet.OnError += OnError;

            GetData();
        }

        public void Disconnect()
        {
            if (_pvpnet.IsConnected)
            {
                try
                {
                    _pvpnet.Disconnect();
                }
                catch
                {
                    
                }
            }
        }

        private void OnError(object sender, Error error)
        {
            Data.ErrorMessage = error.Message;
            Data.State = Account.Result.Error;
            IsCompleted.TrySetResult(true);
        }

        private async void GetData()
        {
            if (!await _pvpnet.ConnectAndLogin())
            {
                return;
            }

            try
            {
                var loginDataPacket = await _pvpnet.GetLoginDataPacketForUser();

                if (loginDataPacket.AllSummonerData == null)
                {
                    OnError(this, new Error
                    {
                        Message = "Summoner Not Created",
                        Type = ErrorType.Login
                    });
                    return;
                }

                List<Task> tasks = new List<Task>
                {
                    GetLoginData(loginDataPacket),
                    _pvpnet.GetStoreUrl().ContinueWith(url => GetStoreData(url.Result)),
                    GetChampions(),
                    GetRunes(loginDataPacket.AllSummonerData.Summoner.SummonerId)
                };

                await Task.WhenAll(tasks.ToArray());

                Data.CheckedTime = DateTime.Now;
                Data.State = Account.Result.Success;
            }
            catch (ClientDisconnectedException)
            {
                Data.ErrorMessage = "Server is Busy. Try Again Later.";
                Data.State = Account.Result.Error;
            }
            catch (Exception e)
            {
                Utils.ExportException(e);
                Data.ErrorMessage = $"Exception found: {e.Message}";
                Data.State = Account.Result.Error;
            }
            IsCompleted.TrySetResult(true);
        }

        private async Task GetLoginData(LoginDataPacket loginDataPacket)
        {
            Data.AccountId = (int)loginDataPacket.AllSummonerData.Summoner.AccountId;
            Data.Summoner = loginDataPacket.AllSummonerData.Summoner.Name;
            Data.SummonerId = (int)loginDataPacket.AllSummonerData.Summoner.SummonerId;
            Data.Level = (int)loginDataPacket.AllSummonerData.SummonerLevel.Level;
            Data.RpBalance = (int)loginDataPacket.RpBalance;
            Data.IpBalance = (int)loginDataPacket.IpBalance;
            Data.RunePages = loginDataPacket.AllSummonerData.SpellBook.BookPages.Count;
            Data.LastPlay = loginDataPacket.AllSummonerData.Summoner.LastGameDate;

            if (loginDataPacket.EmailStatus != null)
            {
                var emailStatus = loginDataPacket.EmailStatus.Replace('_', ' ');
                Data.EmailStatus = char.ToUpper(emailStatus[0]) + emailStatus.Substring(1);
            }
            else
            {
                Data.EmailStatus = "Unknown";
            }

            string prevSeasonRank = loginDataPacket.AllSummonerData.Summoner.PreviousSeasonHighestTier;

            Data.PreviousSeasonRank = !string.IsNullOrEmpty(prevSeasonRank) 
                ? prevSeasonRank[0] + prevSeasonRank.Substring(1).ToLower()
                : "Unranked";

            if (Data.Level == 30)
            {
                var myLeagues = await _pvpnet.GetMyLeaguePositions();
                var soloqLeague = myLeagues.SummonerLeagues.FirstOrDefault(l => l.QueueType == "RANKED_SOLO_5x5");
                Data.SoloQRank = soloqLeague != null
                    ? $"{char.ToUpper(soloqLeague.Tier[0])}{soloqLeague.Tier.Substring(1).ToLower()} {soloqLeague.Rank}"
                    : "Unranked";
            }
            else
            {
                Data.SoloQRank = "Unranked";
            }
        }

        private async Task GetStoreData(string storeUrl)
        {
            Data.Transfers = new List<TransferData>();

            Regex regexTransfers = new Regex("\\\'account_transfer(.*)\\\'\\)", RegexOptions.Multiline);
            Regex regexTransferData = new Regex("rp_cost\\\":(.*?),(?:.*)name\\\":\\\"(.*?)\\\"");
            Regex regexRefunds = new Regex("credit_counter\\\">(\\d[1-3]?)<");
            Regex regexRegion = new Regex("\\.(.*?)\\.");

            var region = regexRegion.Match(storeUrl).Groups[1];

            string storeUrlMisc = $"https://store.{region}.lol.riotgames.com/store/tabs/view/misc";
            string storeUrlHist = $"https://store.{region}.lol.riotgames.com/store/accounts/rental_history";

            CookieContainer cookies = new CookieContainer();

            await Utils.GetHtmlResponse(storeUrl, cookies);

            string miscHtml = await Utils.GetHtmlResponse(storeUrlMisc, cookies);
            string histHtml = await Utils.GetHtmlResponse(storeUrlHist, cookies);

            foreach (Match match in regexTransfers.Matches(miscHtml))
            {
                var data = regexTransferData.Matches(match.Value);

                var transfer = new TransferData
                {
                    Price = int.Parse(data[0].Groups[1].Value.Replace("\"", "")),
                    Name = data[0].Groups[2].Value
                };

                Data.Transfers.Add(transfer);
            }

            if (regexRefunds.IsMatch(histHtml))
            {
                Data.Refunds = int.Parse(regexRefunds.Match(histHtml).Groups[1].Value);
            }
        }

        private async Task GetChampions()
        {
            var champions = await _pvpnet.GetAvailableChampions();

            Data.ChampionList = new List<ChampionData>();
            Data.SkinList = new List<SkinData>();

            foreach (ChampionDTO champion in champions.Where(champ => champ.Owned))
            {
                Champion championData = LeagueData.Champions.FirstOrDefault(c => c.Id == champion.ChampionId);

                if (championData == null)
                {
                    continue;
                }

                Data.ChampionList.Add(new ChampionData
                {
                    Id = championData.Id,
                    Name = championData.Name,
                    PurchaseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(champion.PurchaseDate / 1000d))
                });

                foreach (var skin in champion.ChampionSkins.Where(skin => skin.Owned))
                {
                    Skin skinData = championData.Skins.FirstOrDefault(s => s.Id == skin.SkinId);

                    if (skinData == null)
                    {
                        continue;
                    }

                    Data.SkinList.Add(new SkinData
                    {
                        Id = skinData.Id,
                        Name = skinData.Name,
                        StillObtainable = skin.StillObtainable,
                        ChampionId = championData.Id,
                        PurchaseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(skin.PurchaseDate / 1000d))
                    });
                }

                foreach (ChampionData champ in Data.ChampionList)
                {
                    List<SkinData> skins = Data.SkinList.Where(c => c.ChampionId == champ.Id).ToList();
                    champ.HasSkin = skins.Count > 0;
                    champ.Skins = skins.Count;
                }
            }
        }

        private async Task GetRunes(double summmonerId)
        {
            Data.Runes = new List<RuneData>();

            var runes = await _pvpnet.GetSummonerRuneInventory(summmonerId);

            if(runes == null)
            {
                return;
            }

            foreach (SummonerRune rune in runes.SummonerRunes)
            {
                var runeData = LeagueData.Runes.FirstOrDefault(r => r.Id == rune.RuneId);

                if (runeData == null)
                {
                    continue;
                }

                Data.Runes.Add(new RuneData
                {
                    Name = runeData.Name,
                    Description = runeData.Description,
                    Quantity = rune.Quantity,
                    Tier = runeData.Tier
                });
            }
        }
    }
}