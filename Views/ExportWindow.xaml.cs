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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using LoLAccountChecker.Classes;
using Microsoft.Win32;
using MahApps.Metro.Controls.Dialogs;
using StringLib;

#endregion

namespace LoLAccountChecker.Views
{
    public partial class ExportWindow
    {
        private readonly IEnumerable<Account> _accounts;

        public ExportWindow(IEnumerable<Account> accounts)
        {
            InitializeComponent();

            _accounts = accounts;

            if (Settings.Config.DefaultCustomExportFilename != null)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplates", Settings.Config.DefaultCustomExportFilename);
                LoadFile(filepath);
            }
        }

        public ExportWindow(string filename)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            Title = "Export Help";
            FormatBox.IsReadOnly = true;
            FormatBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            Buttons.Visibility = Visibility.Collapsed;
            LoadFile(filename);
        }

        private bool LoadFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                FormatBox.Text = File.ReadAllText(filepath);
                return true;
            }
            return false;
        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
            {
                return;
            }

            Button s = (Button) sender;

            if (s.Name == "HelpButton")
            {
                ExportWindow w = new ExportWindow("CustomExport.nfo");
                w.Show();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            if (s.Name == "OpenButton")
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    InitialDirectory = Directory.GetCurrentDirectory(),
                    Filter = "Text File(*.txt) | *.txt",
                    Multiselect = false,
                    CheckFileExists = true
                };
                if (ofd.ShowDialog() == true)
                {
                    if (LoadFile(ofd.FileName))
                    {
                        Settings.Config.DefaultCustomExportFilename = ofd.SafeFileName;
                    }
                }
            }
            else if (s.Name == "SaveButton")
            {
                sfd.Filter = "Text File (*.txt)|*.txt";

                if (sfd.ShowDialog() == true && !string.IsNullOrEmpty(sfd.FileName))
                {
                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        sw.Write(FormatBox.Text);
                    }
                    Settings.Config.DefaultCustomExportFilename = sfd.SafeFileName;
                    await this.ShowMessageAsync("Export Template", "Template has been successfully saved.");
                }
            }
            else if (s.Name == "ExportButton")
            {
                Regex accountBlockRegex = new Regex("\\[Account](.+?)\\[\\/Account]", RegexOptions.Singleline);
                Match accountBlock = accountBlockRegex.Match(FormatBox.Text);

                if (string.IsNullOrEmpty(FormatBox.Text) || !accountBlock.Success)
                {
                    return;
                }

                sfd.FileName = "output";
                sfd.Filter = "Text File (*.txt)|*.txt|Html Document (*.htm)|*.htm";

                if (new Regex(@"<[^>]+>").IsMatch(FormatBox.Text))
                {
                    sfd.DefaultExt = ".htm";
                    Utils.UseDefaultExtAsFilterIndex(sfd);
                }

                if (sfd.ShowDialog() != true || string.IsNullOrEmpty(sfd.FileName))
                {
                    return;
                }

                StringBuilder sb = new StringBuilder();

                try
                {
                    foreach (Account account in _accounts)
                    {
                        string accountTemplate = accountBlock.Groups[1].Value;
                        accountTemplate = PopulateLists(accountTemplate, account);
                        accountTemplate = accountTemplate.HenriFormat(account);

                        sb.Append(accountTemplate);
                    }

                    string output = sb.ToString().Trim(Environment.NewLine.ToCharArray());
                    output = FormatBox.Text.Replace(accountBlock.Groups[0].Value, output);

                    Regex indexRegex = new Regex("%index=?(\\d+)?:?(\\d+)?%");
                    MatchCollection indexMatches = indexRegex.Matches(output);

                    if (indexMatches.Count > 0)
                    {
                        int index = 1;
                        int digits = 1;

                        string indexStartValue = indexMatches[0].Groups[1].Value;
                        if (!string.IsNullOrEmpty(indexStartValue))
                        {
                            index = int.Parse(indexStartValue);
                        }

                        string indexDigitsValue = indexMatches[0].Groups[2].Value;
                        if (!string.IsNullOrEmpty(indexDigitsValue))
                        {
                            int d = int.Parse(indexDigitsValue);
                            if (d > 0)
                            {
                                digits = d;
                            }
                        }

                        output = indexRegex.Replace(output, m => index++.ToString(new string('0', digits)));
                    }

                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        sw.Write(output);
                    }

                    await this.ShowMessageAsync("Export", $"Accounts Exported: {_accounts.Count()}");

                    Close();
                }
                catch (Exception ex)
                {
                    Utils.ExportException(ex);
                    await this.ShowMessageAsync("Export", "An error occured. Check the logs for more information.");
                }
            }
        }

        readonly Regex _listsRegex = new Regex("\\[(\\w+)](.+?)\\[\\/\\w+]", RegexOptions.Singleline);
        // ChampionList, SkinList, RuneList
        private string PopulateLists(string template, object obj)
        {
            foreach (Match part in _listsRegex.Matches(template))
            {
                string tag = part.Groups[1].Value;
                string content = part.Groups[2].Value;

                if (obj.GetType().GetProperty(tag) == null)
                {
                    continue;
                }
                IEnumerable enumerable = (IEnumerable)Utils.GetPropertyValue(obj, tag);

                StringBuilder sbListLines = new StringBuilder();

                foreach (var o in enumerable)
                {
                    sbListLines.Append(content.HenriFormat(o));
                }
                string temp = part.Groups[0].Value;
                temp = temp.Replace(temp, sbListLines.ToString()).Trim(Environment.NewLine.ToCharArray());
                template = template.Replace(part.Groups[0].Value, temp);
            }
            return template;
        }
    }
}