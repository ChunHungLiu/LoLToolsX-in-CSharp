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
            Thread.Sleep(1000);
            Thread.Sleep(1000);
            Thread.Sleep(1000);

            string cd = Directory.GetCurrentDirectory();

                //FileStream fs = new FileStream(cd + @"\Logs\Log.txt", FileMode.Append, FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs);

            Console.WriteLine("LoLToolsX 更新安裝器");
            Console.WriteLine("開始進行更新...");

                try
                {
                    File.Copy(cd + @"\download\LoLToolsX.exe", cd + @"\LoLToolsX.exe", true);
                    //sw.WriteLine(DateTime.Now + "信息: LoLToolsX 更新完成!");
                    //sw.WriteLine(DateTime.Now + "信息: 重新啟動 LoLToolsX...");
                    Console.WriteLine("更新完成! 正在重新啟動 LoLToolsX...");
                    Thread.Sleep(1000);
                    Thread.Sleep(1000);
                    Thread.Sleep(1000);
                    Process.Start(cd + @"\LoLToolsX.exe");

                }
                catch
                {
                    Console.WriteLine("更新失敗...");
                    Thread.Sleep(1000);
                    Thread.Sleep(1000);
                    Thread.Sleep(1000);
                    //sw.WriteLine(DateTime.Now + "錯誤: LoLToolsX 更新失敗!");
                    //sw.WriteLine(DateTime.Now + "錯誤: " + e);
                }
                finally
                {
                    //sw.Flush();
                    //sw.Close();
                    //fs.Close();
                    Environment.Exit(Environment.ExitCode);
                }

        }
    }
}
