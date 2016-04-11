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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using LoLAccountChecker.Classes;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;

#endregion

namespace LoLAccountChecker.Views
{
    public partial class MainWindow
    {
        public static MainWindow Instance;
        private static readonly ICollectionView CheckedAccountsView;
        public bool IsFilterActive;
        internal static Predicate<object> CheckedAccountsViewDefaultFilter;

        static MainWindow()
        {
            CheckedAccountsView = new CollectionViewSource {Source = Checker.Accounts}.View;
            CheckedAccountsViewDefaultFilter = item => ((Account)item).State == Account.Result.Success;
            CheckedAccountsView.Filter = CheckedAccountsViewDefaultFilter;
        }

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            AccountsDataGrid.ItemsSource = CheckedAccountsView;
            AccountsDataGrid.PreviewKeyDown += Utils.AccountsDataDataGridSearchByLetterKey;
            AccountsDataGrid.Loaded += (s, e) =>
            {
                foreach(var col in AccountsDataGrid.Columns)
                {
                    col.MinWidth = col.ActualWidth;
                    col.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            };

            Loaded += WindowLoaded;
            Closed += WindowClosed;

            Checker.Accounts.CollectionChanged += (sender, args) =>
            {
                if (args != null)
                {
                    if (args.NewItems != null)
                    {
                        foreach (INotifyPropertyChanged item in args.NewItems)
                        {
                            item.PropertyChanged += (s, e) => UpdateControls();
                        }
                        foreach (Account acc in args.NewItems)
                        {
                            acc.PropertyChanged += (o, e) =>
                            {
                                if (e.PropertyName == nameof(Account.State))
                                {
                                    CheckedAccountsView.Refresh();
                                }
                            };
                        }
                    }
                    if (args.OldItems != null)
                    {
                        foreach (INotifyPropertyChanged item in args.OldItems)
                        {
                            item.PropertyChanged -= (s, e) => UpdateControls();
                        }
                    }
                }
                UpdateControls();
            };
            UpdateControls();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await LeagueData.Load();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Settings.Save();
            Application.Current.Shutdown();
        }

        public void UpdateControls()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(UpdateControls);
                return;
            }

            int numCheckedAcccounts = Checker.Accounts.Count(a => a.State != Account.Result.Unchecked);

            ProgressBar.Value = Checker.Accounts.Any() ? numCheckedAcccounts * 100f / Checker.Accounts.Count : 0;

            ImportButton.IsEnabled = !Checker.IsChecking;
            ExportButton.IsEnabled = numCheckedAcccounts > 0;
            FilterButton.IsEnabled = numCheckedAcccounts > 0;

            StartButton.Content = Checker.IsChecking ? "Stop" : "Start";
            if (Checker.CancellationTokenSource != null && Checker.CancellationTokenSource.IsCancellationRequested)
            {
                StartButton.IsEnabled = false;
                StatusLabel.Content = "Status: Stopping...";
            }
            else
            {
                StartButton.IsEnabled = numCheckedAcccounts < Checker.Accounts.Count;

                if (Checker.IsChecking)
                {
                    StatusLabel.Content = "Status: Checking...";
                }
                else if (numCheckedAcccounts > 0 && numCheckedAcccounts == Checker.Accounts.Count)
                {
                    StatusLabel.Content = "Status: Finished!";
                }
                else
                {
                    StatusLabel.Content = "Status: Stopped!";
                }
            }

            CheckedLabel.Content = $"Checked: {numCheckedAcccounts}/{Checker.Accounts.Count}";

            if (AccountsWindow.Instance != null)
            {
                AccountsWindow.Instance.UpdateControls();
            }
        }

        #region Right Window Commands

        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=X9559SH2MKQ7S");
        }

        private void BtnGithubClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/yokrysty/LoLAccountChecker");
        }

        #endregion

        #region Buttons

        private async void BtnImportClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "JavaScript Object Notation (*.json)|*.json"
            };

            if (ofd.ShowDialog() != true)
            {
                return;
            }

            string file = ofd.FileName;
            if (!File.Exists(file))
            {
                return;
            }

            var jsonformat = JsonFormat.Import(file);

            if (jsonformat == null)
            {
                await this.ShowMessageAsync("Error", "Unable to load JSON file, it might be corrupted or from an old version.");
                return;
            }

            var importedVer = Version.Parse(jsonformat.Version);
            var currentVer = Assembly.GetExecutingAssembly().GetName().Version;

            if (importedVer != currentVer)
            {
                var msg = await this.ShowMessageAsync(
                            "Warning",
                            string.Format(
                                "The file you are importing is from a {0} version. " +
                                "Some functions might not might not work, do you still want to load it?",
                                importedVer < currentVer ? "old" : "new"), MessageDialogStyle.AffirmativeAndNegative);

                if (msg == MessageDialogResult.Negative)
                {
                    return;
                }
            }

            int count = 0;
            foreach (Account account in jsonformat.Accounts)
            {
                if (Checker.Accounts.All(a => !string.Equals(a.Username, account.Username, StringComparison.CurrentCultureIgnoreCase)))
                {
                    foreach (ChampionData champion in account.ChampionList)
                    {
                        SkinData skin = account.SkinList.FirstOrDefault(c => c.ChampionId == champion.Id);

                        champion.HasSkin = skin != null;
                    }

                    Checker.Accounts.Add(account);
                    count++;
                }
            }

            if (count > 0)
            {
                AccountsDataGrid.Focus();
                return;
            }

            await this.ShowMessageAsync("Import", "No new accounts found.");
        }

        private void BtnExportToFileClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = "output",
                Filter = "JavaScript Object Notation (*.json)|*.json"
            };

            if (sfd.ShowDialog() != true)
            {
                return;
            }
            var accounts = Checker.Accounts.Where(a => a.State == Account.Result.Success).ToList();
            JsonFormat.Export(sfd.FileName, accounts);
            this.ShowMessageAsync("Export", $"Exported {accounts.Count} accounts.");
        }

        private void BtnAccountsClick(object sender, RoutedEventArgs e)
        {
            if (AccountsWindow.Instance == null)
            {
                AccountsWindow.Instance = new AccountsWindow();
                AccountsWindow.Instance.Show();
                return;
            }
            
            if (AccountsWindow.Instance.WindowState == WindowState.Minimized)
            {
                AccountsWindow.Instance.WindowState = WindowState.Normal;
                return;
            }
            
            AccountsWindow.Instance.Activate();
        }

        private void BtnStartCheckingClick(object sender, RoutedEventArgs e)
        {
            if (Checker.IsChecking)
            {
                Checker.Stop();
                UpdateControls();
                return;
            }
            Checker.Start();
        }

        private void BtnFilterClick(object sender, RoutedEventArgs e)
        {
            if (IsFilterActive)
            {
                ClearFilter();
                return;
            }
            SearchWindow sw = new SearchWindow();
            sw.ShowDialog();
        }

        #endregion

        #region Context Menu

        private void AccountsDataGrid_Cm(object sender, RoutedEventArgs e)
        {
            Utils.AccountsDataGrid_RightClickCommand(sender, AccountsDataGrid);
        }

        private void ViewAccount()
        {
            if (AccountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }
            AccountWindow window = new AccountWindow((Account)AccountsDataGrid.SelectedItem);
            window.Show();
        }

        private void CmViewAccount(object sender, RoutedEventArgs e)
        {
            ViewAccount();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewAccount();
        }

        private void CmExportJson(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = "output",
                Filter = "JavaScript Object Notation (*.json)|*.json"
            };

            var accounts = AccountsDataGrid.SelectedItems.Cast<Account>().ToList();

            if (sfd.ShowDialog() == true)
            {
                var file = sfd.FileName;
                JsonFormat.Export(file, accounts);
                this.ShowMessageAsync("Export", $"Exported {accounts.Count} accounts.");
            }
        }

        private void CmExportCustom(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }
            ExportWindow window = new ExportWindow(AccountsDataGrid.SelectedItems.Cast<Account>());
            window.ShowDialog();
        }

        #endregion

        public void ClearFilter()
        {
            if (!IsFilterActive)
            {
                return;
            }
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(AccountsDataGrid.ItemsSource);
            if (cv != null)
            {
                cv.Filter = CheckedAccountsViewDefaultFilter;
            }
            FilterButton.Content = "Filter";
            IsFilterActive = false;
        }
    }
}