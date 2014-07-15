using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SevenZip;

namespace LoLToolsX
{
    class CheckUpdate
    {
        /// <summary>
        /// LoLToolsX 更新
        /// </summary>



        public static void checkUpdate()
        {
            Variable.updating = true;
            Logger.log("檢查 LoLToolsX 更新...",Logger.LogType.Info);
            var request = (HttpWebRequest)WebRequest.Create("http://lolnx.pixub.com/loltoolsx/version.txt");
            WebResponse response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());

            var request2 = (HttpWebRequest)WebRequest.Create("http://lolnx.pixub.com/loltoolsx/info.txt");
            WebResponse response2 = request2.GetResponse();
            var reader2 = new StreamReader(response2.GetResponseStream(),Encoding.UTF8);

            try
            {
                //wait.progressBar1.Value = 20;
                string result = reader.ReadToEnd();
                if (Application.ProductVersion != result)
                {
                    Logger.log("LoLToolsX 發現可用更新 : " + result, Logger.LogType.Info);
                    string info = reader2.ReadToEnd();
                    if (MessageBox.Show("有可用更新 按'確定'下載更新\r\n更新內容: \r\n" + info, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Variable.haveUpdate = true;    //有更新
                        //wait.progressBar1.Value = 40;
                        WebClient wc = new WebClient();
                        try
                        {
                            string downloadPath = "https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/LoLToolsX" + result + "/LoLToolsX" + result + ".exe";
                            try
                            {
                                try
                                {  
                                    Variable.updating = true;
                                    //wait.progressBar1.Value = 60;
                                    Logger.log("LoLToolsX 開始下載更新", Logger.LogType.Info);
                                    wc.DownloadFile(downloadPath, Application.StartupPath + @"\download\" + @"LoLToolsX.exe");
                                    //wait.progressBar1.Value = 100;
                                }
                                catch
                                {
                                    Variable.updating = false;
                                    MessageBox.Show("下載更新失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Logger.log("下載更新失敗!", Logger.LogType.Error);
                                    return;
                                }
                                
                                if (MessageBox.Show("更新下載完成 按確定重新啟動程式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    //wait.progressBar1.Value = 0;
                                    //wait.Dispose();
                                    Logger.log("LoLToolsX 更新下載完成", Logger.LogType.Info);
                                    Logger.log("正在關閉程式並進行更新", Logger.LogType.Info);
                                    Logger.log("啟動 Updater.exe 進行更新", Logger.LogType.Info);
                                    Logger.log("Starting " + Application.StartupPath + @"\Updater.exe");
                                    ProcessStartInfo pi = new ProcessStartInfo();
                                    pi.FileName = "Updater.exe";
                                    pi.WorkingDirectory = Application.StartupPath;
                                    Process.Start(pi);
                                    //Process.Start(Application.StartupPath + @"\Updater.exe");
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
                            Variable.updating = false;
                            //wait.progressBar1.Value = 0;
                            //wait.Dispose();
                            wc = null;
                        }
                    }
                }
                else
                {
                    Variable.updating = false;
                    //wait.progressBar1.Value = 0;
                    //wait.Dispose();
                    Logger.log("LoLToolsX 沒有可用更新", Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                Variable.updating = false;
                //wait.progressBar1.Value = 0;
                //wait.Dispose();
                Logger.log("LoLToolsX 下載更新資訊失敗", Logger.LogType.Info);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                Variable.updating = false;
                reader.Close();
                request = null;
                reader.Close();
                request2 = null;
                GC.Collect();
            }
           

        }
    }

    /*
    class LangUpdate
    {
        /// <summary>
        /// LoLToolsX 更新
        /// </summary>



        public static void CheckLangUpdate()
        {
                 WebClient wc = new WebClient();

            Logger.log("檢查 LoLToolsX 語言檔更新...", Logger.LogType.Info);

            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\files\lang\kr"))
                {
                    Logger.log("語言檔有可用更新", Logger.LogType.Info);
                    if (MessageBox.Show("語言檔有可用更新 按'確定'下載更新\r\nPS.下載需時較久，請耐心等候...", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Wait wait = new Wait();
                        wait.Show();

                         if (!File.Exists(Application.StartupPath + @"\SevenZipSharp.dll"))
            {
                if (MessageBox.Show("找不到Skin安裝所需的類別庫, 按確定下載Skin安裝用類別庫。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {

                try
                {
                    wait.progressBar1.Value = 10;
                    wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/SevenZipSharp/SevenZipSharp.dll", Application.StartupPath + @"\SevenZipSharp.dll");
                    wait.progressBar1.Value = 100;
                    MessageBox.Show("下載完成 按'確定'更新語言檔!\r\nPS.下載需時較久，請耐心等候...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("SevenZipSharp.dll 下載完成!", Logger.LogType.Info);
                    wait.Close();
                }
                catch
                {
                    wait.Dispose();
                    MessageBox.Show("下載所需類別庫失敗。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    wc.Dispose();
                }
                }
                else
                {
                    return;
                }
            }
                        wait.Show();
                        try
                        {
                            string downloadPath = "https://dl.dropboxusercontent.com/u/7084520/lang.zip";
                            try
                            {
                                try
                                {
                                    Logger.log("LoLToolsX 開始下載語言檔更新\r\nPS.下載需時較久，請耐心等候...", Logger.LogType.Info);
                                    wc.DownloadFile(downloadPath, Application.StartupPath + @"\download\" + @"lang.zip");
                                    wait.progressBar1.Value = 50;
                                    SevenZip.SevenZipExtractor sz = new SevenZip.SevenZipExtractor(Application.StartupPath + @"\download\" + @"lang.zip");
                                    sz.ExtractArchive(Application.StartupPath + @"\files\lang");
                                    wait.progressBar1.Value = 100;
                                    MessageBox.Show("語言檔更新完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception e)
                                {
                                    Variable.updating = false;
                                    MessageBox.Show("更新失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Logger.log("更新失敗!", Logger.LogType.Error);
                                    Logger.log(e, Logger.LogType.Error);
                                    return;
                                }

                            }
                            catch (Exception e)
                            {
                                //wait.progressBar1.Value = 0;
                                //wait.Dispose();
                                Logger.log("下載更新失敗", Logger.LogType.Error);
                                Logger.log(e, Logger.LogType.Error);
                            }

                        }
                        catch (Exception e2)
                        {
                            //wait.progressBar1.Value = 0;
                            //wait.Dispose();
                            Logger.log("更新失敗", Logger.LogType.Error);
                            Logger.log(e2, Logger.LogType.Error);
                        }
                        finally
                        {
                            Variable.updating = false;
                            //wait.progressBar1.Value = 0;
                            //wait.Dispose();
                            wc = null;
                        }
                    }
                }
                else
                {
                    Variable.updating = false;
                    //wait.progressBar1.Value = 0;
                    //wait.Dispose();
                    Logger.log("語言檔 沒有可用更新", Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                Variable.updating = false;
                //wait.progressBar1.Value = 0;
                //wait.Dispose();
                Logger.log("語言檔 下載更新失敗", Logger.LogType.Info);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                Variable.updating = false;
                GC.Collect();
            }


        }
    }
     */
}

