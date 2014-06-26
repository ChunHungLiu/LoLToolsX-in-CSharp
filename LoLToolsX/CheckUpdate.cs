﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace LoLToolsX
{
    class CheckUpdate
    {
        /// <summary>
        /// LoLToolsX 更新
        /// </summary>

        public static void checkUpdate()
        {
            Logger.log("檢查 LoLToolsX 更新...",Logger.LogType.Info);
            var request = (HttpWebRequest)WebRequest.Create("http://lolnx.pixub.com/loltoolsx/version.txt");
            WebResponse response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());

            WebClient test = new WebClient();
            test.DownloadFile("http://lolnx.pixub.com/loltoolsx/version.txt", Directory.GetCurrentDirectory() + @"\download\version.txt");

            try
            {
                string result = reader.ReadToEnd();
                if (Application.ProductVersion != result)
                {
                    Logger.log("LoLToolsX 發現可用更新 : " + result, Logger.LogType.Info);

                    if (MessageBox.Show("有可用更新 按'確定'下載更新", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        WebClient wc = new WebClient();
                        try
                        {
                            string downloadPath = System.IO.Path.Combine(@"https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/" + result + @"/LoLToolsX" + result + @".exe");
                            try
                            {
                                Logger.log("LoLToolsX 開始下載更新", Logger.LogType.Info);
                                wc.DownloadFile(downloadPath, Directory.GetCurrentDirectory() + @"\download\" + @"LoLToolsX" + result + @".exe");
                                if (MessageBox.Show("更新下載完成 按確定重新啟動程式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    Environment.Exit(Environment.ExitCode);
                                    Process.Start(Directory.GetCurrentDirectory() + "Updater.exe");
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.log("LoLToolsX 下載更新失敗", Logger.LogType.Error);
                            }

                        }
                        catch (Exception e2)
                        {
                            Logger.log("LoLToolsX 更新失敗", Logger.LogType.Error);
                            Logger.log(e2,Logger.LogType.Error);
                        }
                        finally
                        {
                            wc = null;
                        }
                    }
                }
                else
                    Logger.log("LoLToolsX 沒有可用更新", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                Logger.log("LoLToolsX 下載更新資訊失敗", Logger.LogType.Info);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                reader = null;
                request = null;
            }
           

        }
    }
}