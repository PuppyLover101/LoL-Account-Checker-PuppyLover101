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
using System.Windows;
using System.Windows.Controls;
using LoLAccountChecker.Classes;
using BananaLib;

#endregion

namespace LoLAccountChecker.Views
{
    public partial class ImportWindow
    {
        private readonly List<string[]> _accounts;

        public ImportWindow(List<string[]> accounts)
        {
            InitializeComponent();

            _accounts = accounts;

            AccountsGrid.ItemsSource = _accounts;
            RegionBox.ItemsSource = Enum.GetValues(typeof(Region)).Cast<Region>();
            RegionBox.SelectedItem = Settings.Config.SelectedRegion;
        }

        private void OnChangeRegion(object sender, SelectionChangedEventArgs e)
        {
            Settings.Config.SelectedRegion = (Region) RegionBox.SelectedIndex;
        }

        private void BtnImportClick(object sender, RoutedEventArgs e)
        {
            foreach (string[] account in _accounts.Where(a => Checker.Accounts.All(aa => !string.Equals(aa.Username, a[0], StringComparison.CurrentCultureIgnoreCase))))
            {
                try
                {
                    Region region;
                    if (account.Length < 3 || !Enum.TryParse(account[2], true, out region))
                    {
                        region = Settings.Config.SelectedRegion;
                    }

                    Account loginData = new Account
                    {
                        Username = account[0],
                        Password = account[1],
                        State = Account.Result.Unchecked,
                        Region = region
                    };

                    Checker.Accounts.Add(loginData);
                }
                catch
                {
                }
            }
            
            Close();
        }
    }
}
