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

using System.IO;
using System.Linq;
using System.Windows;
using LoLAccountChecker.Classes;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows.Controls;

#endregion

namespace LoLAccountChecker.Views
{
    public partial class AccountsWindow
    {
        public static AccountsWindow Instance;

        public AccountsWindow()
        {
            InitializeComponent();
            Instance = this;

            Closed += (o, a) => Instance = null;

            CheckBoxShowPasswords.IsChecked = Settings.Config.ShowPasswords;

            AccountsDataGrid.PreviewKeyDown += Utils.AccountsDataDataGridSearchByLetterKey;

            AccountsDataGrid.ItemsSource = Checker.Accounts;

            UpdateControls();
        }

        public void UpdateControls()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(UpdateControls);
                return;
            }

            ButtonClear.IsEnabled = !Checker.IsChecking && Checker.Accounts.Any();
            ButtonExport.IsEnabled = !Checker.IsChecking && Checker.Accounts.Any();
            ButtonAdd.IsEnabled = !Checker.IsChecking;
            ButtonImport.IsEnabled = !Checker.IsChecking;
            CheckBoxShowPasswords.IsEnabled = !Checker.IsChecking;

            foreach (MenuItem menuItem in ContextMenuAccounts.Items)
            {
                if (menuItem.Header.ToString().Contains("Copy"))
                {
                    continue;
                }
                menuItem.IsEnabled = !Checker.IsChecking;
            }
        }

        private void ShowPasswordsClick(object sender, RoutedEventArgs e)
        {
            Settings.Config.ShowPasswords = CheckBoxShowPasswords.IsChecked == true;
            foreach (Account account in Checker.Accounts)
            {
                account.ShowPassword = Settings.Config.ShowPasswords;
            }
        }

        private void BtnAddAccountClick(object sender, RoutedEventArgs e)
        {
            AccountEdit w = new AccountEdit();
            w.ShowDialog();
        }

        private void BtnImportClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text File (*.txt)|*.txt"
            };

            if (ofd.ShowDialog() == true)
            {
                if (!File.Exists(ofd.FileName))
                {
                    return;
                }

                List<string[]> accounts = Utils.GetLogins(ofd.FileName);

                if (!accounts.Any())
                {
                    return;
                }

                ImportWindow window = new ImportWindow(accounts);
                window.ShowDialog();
            }
        }

        private async void BtnExportClick(object sender, RoutedEventArgs e)
        {
            List<Account> accounts = Checker.Accounts.Where(a => a.State != Account.Result.Unchecked).ToList();

            if (!accounts.Any())
            {
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = "output",
                Filter = "Text file (*.txt)|*.txt"
            };

            if (sfd.ShowDialog() == false)
            {
                return;
            }

            MetroDialogSettings settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                FirstAuxiliaryButtonText = "Cancel"
            };

            bool exportErrors = false;

            if (Checker.Accounts.Any(a => a.State == Account.Result.Error))
            {
                MessageDialogResult dialog = await this.ShowMessageAsync(
                            "Export", "Export errors?", MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary,
                            settings);

                if (dialog == MessageDialogResult.FirstAuxiliary)
                {
                    return;
                }

                exportErrors = dialog == MessageDialogResult.Affirmative;
            }

            Utils.ExportLogins(sfd.FileName, accounts, exportErrors);

            await this.ShowMessageAsync("Export", "All the accounts have been exported!");
        }

        private void CmEditClick(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }

            AccountEdit w = new AccountEdit(AccountsDataGrid.SelectedItems.Cast<Account>().ToList());
            w.ShowDialog();
        }

        private void CmCopyComboClick(object sender, RoutedEventArgs e)
        {
            Utils.AccountsDataGrid_RightClickCommand(sender, AccountsDataGrid);
        }

        private async void CmMakeUncheckedClick(object sender, RoutedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItems.Count == 0)
            {
                return;
            }

            bool uncheckSuccess = false;
            bool toAll = false;

            MetroDialogSettings settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "No",
                NegativeButtonText = "Yes",
                FirstAuxiliaryButtonText = "No to All",
                SecondAuxiliaryButtonText = "Yes to All"
            };

            foreach (Account account in AccountsDataGrid.SelectedItems)
            {
                if (AccountsDataGrid.SelectedItems.Count > 1)
                {
                    if (account.State == Account.Result.Success)
                    {
                        if (!toAll)
                        {
                            MessageDialogResult confirm = await this.ShowMessageAsync(
                                        "Make Unchecked",
                                        string.Format(
                                            "This account ({0}) was successfully checked, are you sure that you wanna make it unchecked?", account.Username),
                                        MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary,
                                        settings);

                            switch (confirm)
                            {
                                case MessageDialogResult.Affirmative:
                                    continue;
                                case MessageDialogResult.FirstAuxiliary:
                                    toAll = true;
                                    continue;
                                case MessageDialogResult.SecondAuxiliary:
                                    toAll = true;
                                    uncheckSuccess = true;
                                    break;
                            }
                        }
                        else
                        {
                            if (!uncheckSuccess)
                            {
                                continue;
                            }
                        }
                    }

                    account.State = Account.Result.Unchecked;
                }
                else
                {
                    if (account.State == Account.Result.Success)
                    {
                        MessageDialogResult confirm = await this.ShowMessageAsync(
                                    "Make Unchecked",
                                    "This account was successfully checked, are you sure that you wanna make it unchecked?",
                                    MessageDialogStyle.AffirmativeAndNegative);

                        if (confirm == MessageDialogResult.Negative)
                        {
                            return;
                        }
                    }

                    account.State = Account.Result.Unchecked;
                }
            }
        }

        private async void CmRemoveClick(object sender, RoutedEventArgs e)
        {
            int selected = AccountsDataGrid.SelectedItems.Count;

            if (selected == 0)
            {
                return;
            }

            MessageDialogResult confirm;
            if (selected > 1)
            {
                confirm = await this.ShowMessageAsync(
                            "Remove",
                            string.Format(
                                "Are you sure that you wanna remove {0} accounts?", selected),
                            MessageDialogStyle.AffirmativeAndNegative);
            }
            else
            {
                confirm = await this.ShowMessageAsync("Remove", "Are you sure?", MessageDialogStyle.AffirmativeAndNegative);
            }

            if (confirm == MessageDialogResult.Negative)
            {
                return;
            }

            foreach (Account account in AccountsDataGrid.SelectedItems.Cast<Account>().ToList())
            {
                Checker.Accounts.Remove(account);
            }
        }

        private async void BtnClearAccountsClick(object sender, RoutedEventArgs e)
        {
            MessageDialogResult confirm = await this.ShowMessageAsync("Remove", "Are you sure?", MessageDialogStyle.AffirmativeAndNegative);

            if (confirm == MessageDialogResult.Negative)
            {
                return;
            }
            Checker.Accounts.Clear();
        }
    }
}