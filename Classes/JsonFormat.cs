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

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

#endregion

namespace LoLAccountChecker.Classes
{
    internal class JsonFormat
    {
        public JsonFormat(List<Account> accounts)
        {
            Accounts = accounts;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string Version { get; set; }
        public List<Account> Accounts { get; set; }

        public static JsonFormat Import(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string jso = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<JsonFormat>(jso);
            }
        }

        public static void Export(string file, List<Account> accounts)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                string jso = JsonConvert.SerializeObject(new JsonFormat(accounts));

                sw.WriteLine(jso);
            }
        }
    }
}