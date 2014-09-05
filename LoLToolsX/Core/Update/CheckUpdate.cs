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
using LoLToolsX.Core.Update;
using System.Xml.Linq;

namespace LoLToolsX.Core.Update
{
    class CheckUpdate
    {
        /// <summary>
        /// LoLToolsX 更新
        /// </summary>



        public static void checkUpdate()
        {
            /********************
             * 檢查LoLToolsX更新 *
             ********************/

            string verValue = "";           //版本
            List<string> updateInfo = new List<string>();    //儲存更新內容

            try
            {
                //檢查最新版本訊息
                Variable.updating = true;          //正在更新
                Logger.log("檢查 LoLToolsX 更新...", Logger.LogType.Info);
                XDocument doc = XDocument.Load("http://nitroxenon.com/loltoolsx/version.xml");       //讀取最新版本
                var tmp = doc.Descendants("Version");
                foreach (var s in tmp)
                {
                    verValue = s.Value;         //取得最新版本
                }
            }
            catch
            {
                MessageBox.Show("檢查更新失敗!");
                return;
            }

            try
            {
                //檢查最新版本的更新內容
                XDocument doc2 = XDocument.Load("http://nitroxenon.com/loltoolsx/info.xml");       //讀取更新內容
                var tmp2 = doc2.Descendants("Info");
                foreach (var s in tmp2)
                {
                    updateInfo.Add(s.Value);                //加入到updateInfo的List
                }
                
            }
            catch
            {
                MessageBox.Show("檢查更新失敗!");
                return;
            }

            try
            {
                //閱讀更新信息
                //版本比對
                if (Application.ProductVersion != verValue)
                {
                    //有更新
                    Variable.haveUpdate = true;
                    UpdateForm uf = new UpdateForm(verValue,updateInfo);
                    uf.Show();
                    //Call Form an contiune original work (can use invoke)
                    Application.Run();
                }
                else
                {
                    Variable.updating = false;
                    Logger.log("LoLToolsX 沒有可用更新", Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                Variable.updating = false;
                Logger.log("LoLToolsX 下載更新資訊失敗", Logger.LogType.Info);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                Variable.updating = false;
                //釋放記憶體
                GC.Collect();
            }

            Variable.updating = false;
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

