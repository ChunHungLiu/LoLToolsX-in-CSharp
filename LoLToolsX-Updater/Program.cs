using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace LoLToolsX_Updater
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("LoLToolsX 更新安裝器");
            Console.WriteLine("開始進行更新...");

            Thread.Sleep(1000);
            Thread.Sleep(1000);
            Thread.Sleep(1000);

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"download\LoLToolsX.exe"))
            {
                Console.WriteLine("找不到更新檔，正在重新啟動 LoLToolsX...");
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"LoLToolsX.exe");
                Environment.Exit(0);
            }

            try
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"download\LoLToolsX.exe", AppDomain.CurrentDomain.BaseDirectory + @"LoLToolsX.exe", true);
                Console.WriteLine("更新完成!");

            }
            catch
            {
                Console.WriteLine("更新失敗...");
            }
            finally
            {
                Console.WriteLine("正在重新啟動 LoLToolsX...");
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                Thread.Sleep(1000);
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"LoLToolsX.exe");
                Environment.Exit(0);
            }
        }
    }
}
