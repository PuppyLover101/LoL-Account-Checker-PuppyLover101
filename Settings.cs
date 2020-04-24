#region License

// Copyright 2015 LoLAccountChecker-PuppyLover101.
// 
// This file is part of LoLAccountChecker-PuppyLover101.
// 
// LoLAccountChecker-PuppyLover101 is free software: you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// LoLAccountChecker-PuppyLover101 is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License 
// along with LoLAccountChecker-PuppyLover101. If not, see http://www.gnu.org/licenses/.

#endregion

#region

using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BananaLib;

#endregion

namespace LoLAccountChecker
{
    internal class Settings
    {
        private const string File = "settings.json";
        public static Settings Config;
        public static TaskFactory TaskFactory;

        static Settings()
        {
            if (!System.IO.File.Exists(File))
            {
                Config = new Settings
                {
                    ShowPasswords = true,
                    SelectedRegion = Region.NA
                };
                Save();
                return;
            }
            Load();
            TaskFactory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(Environment.ProcessorCount));
        }

        public bool ShowPasswords { get; set; }
        public Region SelectedRegion { get; set; }
        public string ClientVersion { get; set; }
        public string DataDragonVersion { get; set; }
        public string DefaultCustomExportFilename { get; set; }

        public static void Save()
        {
            using (StreamWriter sw = new StreamWriter(File))
            {
                sw.Write(JsonConvert.SerializeObject(Config, Formatting.Indented));
            }
        }

        public static void Load()
        {
            using (StreamReader sr = new StreamReader(File))
            {
                Config = JsonConvert.DeserializeObject<Settings>(sr.ReadToEnd());
            }
        }
    }
}