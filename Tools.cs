using BananaLib;
using BananaLib.RiotObjects.Platform;
using RtmpSharp.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LolAccountChecker
{
    internal static class Tools
	{
        static object lockerLog = new object();

		public static void Log(string text)
		{
            lock(lockerLog)
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\logs")) Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\logs");

                string path = AppDomain.CurrentDomain.BaseDirectory + "\\logs\\errors.txt";
                try
                {
                    bool flag = !File.Exists(path);
                    if (flag)
                    {
                        using (StreamWriter streamWriter = File.CreateText(path))
                        {
                            streamWriter.Write(string.Concat(new object[]
                            {
                            "[",
                            DateTime.Now,
                            "] ",
                            text,
                            Environment.NewLine
                            }));
                            return;
                        }
                    }
                    bool flag2 = File.Exists(path);
                    if (flag2)
                    {
                        File.AppendAllText(path, string.Concat(new object[]
                        {
                        "[",
                        DateTime.Now,
                        "] ",
                        text,
                        Environment.NewLine
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The process failed: {0}", ex.ToString());
                }
            }
		}

		public static void TitleMessage(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[" + DateTime.Now + "] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(message + "\n");
		}

        private static object locker = new object();
		public static void ConsoleMessage(string message, ConsoleColor color, bool translate = true)
		{
            lock(locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[" + DateTime.Now + "] ");
                Console.ForegroundColor = color;
                Console.Write(message + "\n");
            }
		}

		public static Task<object> ackLeaverBusterWarning(this LoLClient lolClient)
		{
			return lolClient.InvokeAsync<object>("clientFacadeService", "ackLeaverBusterWarning", new object[0]);
		}

		public static Task<object> callPersistenceMessaging(this LoLClient lolClient, SimpleDialogMessageResponse response)
		{
			return lolClient.InvokeAsync<object>("clientFacadeService", "callPersistenceMessaging", new object[]
			{
				(SerializedNameAttribute)Attribute.GetCustomAttribute(response.GetType(), typeof(SerializedNameAttribute))
			});
		}

		public static bool IsRunning(this Process process)
		{
			bool flag = process == null;
			if (flag)
			{
				throw new ArgumentNullException("process");
			}
			bool result;
			try
			{
				Process.GetProcessById(process.Id);
			}
			catch (ArgumentException)
			{
				result = false;
				return result;
			}
			result = true;
			return result;
		}

		public static T ParseEnum<T>(string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value, true));
		}
	}
}
