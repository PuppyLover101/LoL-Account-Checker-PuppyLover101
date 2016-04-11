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
using Newtonsoft.Json;

#endregion

namespace LoLAccountChecker.Classes
{
    public class SkinData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChampionId { get; set; }
        public bool StillObtainable { get; set; }
        public DateTime PurchaseDate { get; set; }

        [JsonIgnore]
        public string Obtainable => StillObtainable ? "Yes" : "No";

        [JsonIgnore]
        public Champion Champion => LeagueData.GetChampion(ChampionId);

        [JsonIgnore]
        public string ChampionName => Champion.Name;

        [JsonIgnore]
        public Skin Skin => LeagueData.GetSkin(Champion, Id);

        [JsonIgnore]
        public string ImageUrl
        {
            get
            {
                try
                {
                    return $"http://ddragon.leagueoflegends.com/cdn/img/champion/loading/{Champion.StrId}_{Skin.Num}.jpg";
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}