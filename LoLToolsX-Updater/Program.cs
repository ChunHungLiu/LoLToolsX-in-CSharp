using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace LoLToolsX_Updater
{
    class Program
    {


        static void Main(string[] args)
        {
            string cd = Directory.GetCurrentDirectory();

            //if (args.Length > 0)
            //{

                FileStream fs = new FileStream(cd + @"\Logs\Log.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                //switch (args[0])
                //{
                    //case "Update":
                        //{
                            try
                            {
                                File.Copy(cd + @"\download\LoLToolsX.exe", cd, true);
                                sw.WriteLine(DateTime.Now + "信息: LoLToolsX 更新完成!");
                                sw.WriteLine(DateTime.Now + "信息: 重新啟動 LoLToolsX...");
                                Process.Start(cd + @"\LoLToolsX.exe");
                                Console.ReadLine();
                                
                            }
                            catch (Exception e)
                            {
                                sw.WriteLine(DateTime.Now + "錯誤: LoLToolsX 更新失敗!");
                                sw.WriteLine(DateTime.Now + "錯誤: " + e );
                            }
                            //break;
                        //}
                //}
                    
            //}
        }
    }
}
