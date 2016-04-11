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

using System.Diagnostics;
using System.Windows;
using LoLAccountChecker.Classes;
using System.Linq;
using System;
using System.Windows.Data;
using System.ComponentModel;

#endregion

namespace LoLAccountChecker.Views
{
    public partial class AccountWindow
    {
        private Account mAccount;

        private bool mChampsWithSkins = false;

        public AccountWindow(Account account)
        {
            InitializeComponent();

            mAccount = account;

            Title = string.Format("{0} - View account", mAccount.Username);

            SortingBoxChamps.ItemsSource = new string[] { "Name", "Purchase Date", "With Skin(s)" };
            SortingBoxChamps.SelectedIndex = 0;

            var champsWithSkin = mAccount.ChampionList.Where(c => c.HasSkin == true).OrderBy(x => x.Name);

            if (champsWithSkin.Any())
            {
                mChampsWithSkins = true;
                FilterBoxSkins.ItemsSource = champsWithSkin;
                FilterBoxSkins.DisplayMemberPath = "Name";
                FilterBoxSkins.SelectedValuePath = "Id";
                FilterBoxSkins.IsEnabled = true;
            }

            if (mAccount.ChampionList != null)
            {
                ChampionsListBox.ItemsSource = mAccount.ChampionList.OrderBy(x => x.Name);
            }

            if (mAccount.SkinList != null)
            {
                SkinsListBox.ItemsSource = mAccount.SkinList;
                ((CollectionView)CollectionViewSource.GetDefaultView(SkinsListBox.ItemsSource)).Filter = null;
            }

            if (mAccount.Runes != null)
            {
                RunesGrid.ItemsSource = mAccount.Runes;
            }

            if (mAccount.Transfers != null)
            {
                TransfersGrid.ItemsSource = mAccount.Transfers;
            }
        }

        public bool ChampsWithSkins
        {
            get { return mChampsWithSkins; }
        }

        private void CmViewModel(object sender, RoutedEventArgs e)
        {
            SkinData selectedSkin = SkinsListBox.SelectedItem as SkinData;

            if (selectedSkin == null)
            {
                return;
            }

            Process.Start(
                string.Format("http://www.lolking.net/models/?champion={0}&skin={1}", selectedSkin.ChampionId, selectedSkin.Skin.Num)
            );
        }

        private void CmViewSkins(object sender, RoutedEventArgs e)
        {
            ChampionData selectedChampion = ChampionsListBox.SelectedItem as ChampionData;

            if ((selectedChampion == null) || !selectedChampion.HasSkin)
            {
                return;
            }

            FilterBoxSkins.SelectedItem = selectedChampion;
            AccountTabs.SelectedIndex = 1;
        }

        private void SortingBoxChamps_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(ChampionsListBox.ItemsSource);

            if (cv == null)
            {
                return;
            }

            using (cv.DeferRefresh())
            {
                int i = SortingBoxChamps.SelectedIndex;
                switch (i)
                {
                    case 0:
                        cv.SortDescriptions.Clear();
                        cv.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                        break;
                    
                    case 1:
                        cv.SortDescriptions.Clear();
                        cv.SortDescriptions.Add(new SortDescription("PurchaseDate", ListSortDirection.Ascending));
                        break;
                    
                    case 2:
                        if (mChampsWithSkins)
                        {
                            cv.SortDescriptions.Clear();
                            cv.SortDescriptions.Add(new SortDescription("HasSkin", ListSortDirection.Descending));
                            cv.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                        }
                        break;
                    
                    default:
                        break;
                }
            }
        }
        private void FilterBoxSkins_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FilterBoxSkins.SelectedIndex == -1)
            {
                return;
            }

            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(SkinsListBox.ItemsSource);
            cv.Filter = (item) =>
            {
                SkinData skinData = item as SkinData;
                if (skinData == null)
                {
                    return false;
                }
                return skinData.ChampionId == int.Parse(FilterBoxSkins.SelectedValue.ToString());
            };

            FilterCheckBoxSkins.IsChecked = true;
            FilterCheckBoxSkins.IsEnabled = true;
        }

        private void FilterCheckBoxSkins_Click(object sender, RoutedEventArgs e)
        {
            ((CollectionView)CollectionViewSource.GetDefaultView(SkinsListBox.ItemsSource)).Filter = null;
            FilterBoxSkins.SelectedIndex = -1;
            FilterCheckBoxSkins.IsChecked = false;
            FilterCheckBoxSkins.IsEnabled = false;
        }
    }
}