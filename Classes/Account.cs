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
using Newtonsoft.Json;
using PVPNetClient;

#endregion

namespace LoLAccountChecker.Classes
{
    public class Account : BaseViewModel
    {
        public enum Result
        {
            Unchecked,
            Success,
            Error
        }

        public int AccountId { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetAndNotify(ref _username, value, nameof(Username)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetAndNotify(ref _password, value, nameof(Password)); }
        }
        public int SummonerId { get; set; }
        public string Summoner { get; set; }
        public int Level { get; set; }
        public string EmailStatus { get; set; }
        public int RpBalance { get; set; }
        public int IpBalance { get; set; }
        public int RunePages { get; set; }
        public int Refunds { get; set; }
        public string PreviousSeasonRank { get; set; }
        public string SoloQRank { get; set; }
        public DateTime LastPlay { get; set; }
        public DateTime CheckedTime { get; set; }
        public List<ChampionData> ChampionList { get; set; }
        public List<SkinData> SkinList { get; set; }
        public List<RuneData> Runes { get; set; }
        public List<TransferData> Transfers { get; set; }
        public string ErrorMessage { get; set; }

        private Result _state;
        public Result State
        {
            get { return _state; }
            set
            {
                SetAndNotify(ref _state, value, nameof(State));
                RaisePropertyChanged(nameof(StateDisplay));
            }
        }

        private Region _region;
        public Region Region
        {
            get { return _region; }
            set { SetAndNotify(ref _region, value, nameof(Region)); }
        }

        [JsonIgnore]
        public int Champions => ChampionList?.Count ?? 0;

        [JsonIgnore]
        public int Skins => SkinList?.Count ?? 0;

        private bool _showPassword = Settings.Config.ShowPasswords;
        [JsonIgnore]
        public bool ShowPassword
        {
            get { return _showPassword; }
            set
            {
                SetAndNotify(ref _showPassword, value, nameof(ShowPassword));
                RaisePropertyChanged(nameof(PasswordDisplay));
            }
        }
        [JsonIgnore]
        public string PasswordDisplay => ShowPassword ? Password : new string('\u2022', Password.Length);

        [JsonIgnore]
        public string StateDisplay
        {
            get
            {
                switch (State)
                {
                    case Result.Success:
                        return "Successfully Checked";

                    case Result.Unchecked:
                        return "Unchecked";

                    case Result.Error:
                        return ErrorMessage;

                    default:
                        return string.Empty;
                }
            }
        }
    }
}