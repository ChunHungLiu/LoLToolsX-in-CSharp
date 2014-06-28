using System;
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
            //Wait wait = new Wait();
            //wait.Show();
            //wait.progressBar1.Value = 0;

            Logger.log("檢查 LoLToolsX 更新...",Logger.LogType.Info);
            var request = (HttpWebRequest)WebRequest.Create("http://lolnx.pixub.com/loltoolsx/version.txt");
            WebResponse response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            //wait.progressBar1.Value = 10;

            WebClient test = new WebClient();
            test.DownloadFile("http://lolnx.pixub.com/loltoolsx/version.txt", Directory.GetCurrentDirectory() + @"\download\version.txt");

            try
            {
                //wait.progressBar1.Value = 20;
                string result = reader.ReadToEnd();
                if (Application.ProductVersion != result)
                {
                    Logger.log("LoLToolsX 發現可用更新 : " + result, Logger.LogType.Info);

                    if (MessageBox.Show("有可用更新 按'確定'下載更新", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Variable.haveUpdate = true;    //有更新
                        //wait.progressBar1.Value = 40;
                        WebClient wc = new WebClient();
                        try
                        {
                            string downloadPath = "https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/LoLToolsX" + result + "/LoLToolsX" + result + ".exe";
                            try
                            {
                                //wait.progressBar1.Value = 60;
                                Logger.log("LoLToolsX 開始下載更新", Logger.LogType.Info);
                                wc.DownloadFile(downloadPath, Directory.GetCurrentDirectory() + @"\download\" + @"LoLToolsX.exe");
                                //wait.progressBar1.Value = 100;
                                if (MessageBox.Show("更新下載完成 按確定重新啟動程式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    //wait.progressBar1.Value = 0;
                                    //wait.Dispose();
                                    Logger.log("LoLToolsX 更新下載完成", Logger.LogType.Info);
                                    Logger.log("正在關閉程式並進行更新", Logger.LogType.Info);
                                    Logger.log("啟動 Updater.exe 進行更新", Logger.LogType.Info);
                                    Logger.log("Starting " + Directory.GetCurrentDirectory() + @"\Updater.exe");
                                    ProcessStartInfo pi = new ProcessStartInfo();
                                    pi.FileName = "Updater.exe";
                                    pi.WorkingDirectory = Directory.GetCurrentDirectory();
                                    Process.Start(pi);
                                    //Process.Start(Directory.GetCurrentDirectory() + @"\Updater.exe");
                                    Application.Exit();
                                }
                                else
                                {
                                    //ait.progressBar1.Value = 0;
                                    //wait.Dispose();
                                }
                            }
                            catch (System.ComponentModel.Win32Exception err)
                            {
                                //wait.progressBar1.Value = 0;
                                //wait.Dispose();
                                Logger.log("找不到 Updater.exe ,更新失敗", Logger.LogType.Error);
                                Logger.log(err, Logger.LogType.Error);
                                MessageBox.Show("找不到 Updater.exe ,更新失敗", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception e)
                            {
                                //wait.progressBar1.Value = 0;
                                //wait.Dispose();
                                Logger.log("LoLToolsX 下載更新失敗", Logger.LogType.Error);
                                Logger.log(e, Logger.LogType.Error);
                            }

                        }
                        catch (Exception e2)
                        {
                            //wait.progressBar1.Value = 0;
                            //wait.Dispose();
                            Logger.log("LoLToolsX 更新失敗", Logger.LogType.Error);
                            Logger.log(e2, Logger.LogType.Error);
                        }
                        finally
                        {
                            //wait.progressBar1.Value = 0;
                            //wait.Dispose();
                            wc = null;
                        }
                    }
                }
                else
                {
                    //wait.progressBar1.Value = 0;
                    //wait.Dispose();
                    Logger.log("LoLToolsX 沒有可用更新", Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                //wait.progressBar1.Value = 0;
                //wait.Dispose();
                Logger.log("LoLToolsX 下載更新資訊失敗", Logger.LogType.Info);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                reader = null;
                request = null;
                //wait.Close();
                //wait.Dispose();
                GC.Collect();
            }
           

        }
    }
}
