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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LoLAccountChecker.Classes;
using LoLAccountChecker.Views;
using PVPNetClient;
using RtmpSharp.IO;

#endregion

namespace LoLAccountChecker
{
    internal delegate void NewAccount(Account account);

    internal static class Checker
    {
        public static ObservableCollection<Account> Accounts = new ObservableCollection<Account>();
        static readonly ConcurrentQueue<KeyValuePair<int, Account>> Queue = new ConcurrentQueue<KeyValuePair<int, Account>>();        
        private static readonly SerializationContext Context = PvpClient.GetContext();
        private static readonly string LoLIp = PvpClient.GetLoLIpAddress();
        public static bool IsChecking { get; private set; }
        public static CancellationTokenSource CancellationTokenSource { get; private set; }

        public static async void Start()
        {
            if (IsChecking)
            {
                return;
            }

            CancellationTokenSource = new CancellationTokenSource();

            IsChecking = true;
            MainWindow.Instance.UpdateControls();

            await Task.Run(() =>
            {
                if (!Queue.IsEmpty)
                {
                    Queue.Clear();
                }

                for (int i = 0; i <= Accounts.Count - 1; i++)
                {
                    if (Accounts[i].State == Account.Result.Unchecked)
                    {
                        Queue.Enqueue(new KeyValuePair<int, Account>(i, Accounts[i]));
                    }
                }
            });

            if (!Queue.IsEmpty)
            {
                Task[] tasks = new Task[Environment.ProcessorCount];
                for (int i = 0; i <= tasks.Length - 1; i++)
                {
                    tasks[i] = Task.Run(() => CheckAccount());
                }

                await Task.WhenAll(tasks);
            }

            IsChecking = false;
            CancellationTokenSource = null;
            MainWindow.Instance.UpdateControls();
        }

        public static void Stop()
        {
            if (!IsChecking)
            {
                return;
            }
            CancellationTokenSource.Cancel();
        }

        private static void CheckAccount()
        {
            while (!Queue.IsEmpty)
            {
                if (CancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                KeyValuePair<int, Account> item;

                if (Queue.TryDequeue(out item))
                {
                    Account account = item.Value;

                    Client client = new Client(account.Region, account.Username, account.Password, LoLIp, Context);
                    client.IsCompleted.Task.Wait();
                    client.Disconnect();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Accounts[item.Key] = client.Data;
                    });
                }
            }
        }
    }
}