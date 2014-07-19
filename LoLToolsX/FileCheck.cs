using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.Threading;

namespace LoLToolsX
{
    public partial class FileCheck : Form
    {
        WebClient wc = new WebClient();

        string cd = Application.StartupPath;

        public FileCheck()
        {
            InitializeComponent();
        }

        private void FileCheck_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }
        private void CheckBegin()
        {
            if (!Directory.Exists(cd + @"\Logs"))
            {
                Directory.CreateDirectory(cd + @"\Logs");
                //會出錯
                //Logger.log("找不到Logs資料夾 重新建立資料夾", Logger.LogType.Error);
            }

            // Logger 開始記錄
            if (!Directory.Exists(Application.StartupPath + @"\Logs"))
                Directory.CreateDirectory(Application.StartupPath + @"\Logs");
            Logger.start();

            Logger.log("LoLToolsX 啟動路徑 : " + cd);

            string[] folder = {
                                  "\\bak",
                                  "\\bak\\Air",
                                  "\\bak\\Chat",
                                  "\\bak\\lang",
                                  "\\bak\\LoL",
                                  "\\bak\\na_lang",
                                  "\\bak\\na_server_prop",
                                  "\\bak\\server_prop",
                                  "\\bak\\sound",
                                  "\\bak\\UI",
                                  "\\bak\\UI\\game",
                                  "\\download",
                                  "\\Logs",
                                  "\\files",
                                  "\\files\\lang\\kr"
                              };

            string[] files = {
                                  "\\config.ini",
                                  "\\Skin.txt",
                                  "\\LoLBakRes.exe",
                                  "\\Updater.exe",
                                  "\\Logs\\Log.txt",
                                  "\\SevenZipSharp.dll",
                                  "\\7z.dll"
                             };

            int count = folder.Length + files.Length;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = count;
            progressBar1.Step = 1;

            textBox1.AppendText("檔案檢查開始!");

            foreach (string f in files)
            {
                string path = cd + f;
                textBox1.AppendText(String.Format("\r\n檢查 {0}", path));
                if (!File.Exists(path))
                {
                    textBox1.AppendText(String.Format("\r\n找不到 {0} 檔案", path));
                    switch (f)
                    {
                        case "\\config.ini":
                            wc.DownloadFile("http://lolnx.pixub.com/loltoolsx/config.ini", cd + "\\config.ini");
                            textBox1.AppendText("\r\n下載config.ini");
                            break;
                        case "\\Skin.txt":
                            File.Create(cd + "\\Skin.txt");
                            break;
                        case "\\Logs\\Log.txt":
                            File.Create(cd + "\\Logs\\Log.txt");
                            break;
                        case "\\LoLBakRes.exe":
                            MessageBox.Show("備份主控台遺失 將無法進行一鍵備份與還原");
                            textBox1.AppendText("\r\n備份主控台遺失 將無法進行一鍵備份與還原");
                            Variable.allowBakRes = false;
                            break;
                        case "\\Updater.exe":
                            MessageBox.Show("更新程式遺失 將無法進行更新");
                            textBox1.AppendText("\r\n更新程式遺失 將無法進行更新");
                            Variable.allowUpdate = false;
                            break;
                        case "\\SevenZipSharp.dll" :
                            textBox1.AppendText("\r\n程式必要的類別庫遺失 正在重新下載... 請勿關閉程式");
                            try
                            {
                                wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/SevenZipSharp/SevenZipSharp.dll", cd + "\\SevenZipSharp.dll");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                        case "\\7z.dll":
                            textBox1.AppendText("\r\n程式必要的類別庫遺失 正在重新下載... 請勿關閉程式");
                            WebClient wc3 = new WebClient();
                            try
                            {
                                wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/7z/7z.dll", cd + "\\7z.dll");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                    }

                }
                progressBar1.PerformStep();
            }

            foreach (string f in folder)
            {
                string path = cd + f;
                textBox1.AppendText(String.Format("\r\n檢查 {0}", path));
                if (!Directory.Exists(path) & f != "\\files")
                {
                    if (f != "\\files\\lang\\kr")
                    {
                        textBox1.AppendText(String.Format("\r\n找不到 {0} 目錄", path));
                        Directory.CreateDirectory(path);
                        textBox1.AppendText(String.Format("\r\n{0} 建立成功!", path));
                    }
                }
                if (f.Contains("bak"))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    di.Refresh();
                }
                if (!Directory.Exists(path) & f == "\\files\\lang\\kr")
                {
                    textBox1.AppendText("\r\n找不到韓文語言檔案 正在重新下載... 請勿關閉程式");
                    wc.DownloadFile("https://dl.dropboxusercontent.com/u/7084520/LoLToolsX/KR_lang/kr.zip", cd + "\\download\\kr.zip");
                    SevenZip.SevenZipExtractor Extractor = new SevenZip.SevenZipExtractor(cd + "\\download\\kr.zip");
                    Extractor.ExtractArchive(cd + "\\files\\lang");
                    textBox1.AppendText("\r\n下載完成!");

                }
                if (!Directory.Exists(path) & f == "\\files")
                {
                    textBox1.AppendText(String.Format("\r\n找不到 {0} 目錄\r\n程式無法繼續執行...", path));
                    textBox1.AppendText(String.Format("\r\n程式將會於 5 秒後退出"));
                    textBox1.AppendText(".");
                    Thread.Sleep(1000);
                    textBox1.AppendText(".");
                    Thread.Sleep(1000);
                    textBox1.AppendText(".");
                    Thread.Sleep(1000);
                    textBox1.AppendText(".");
                    Thread.Sleep(1000);
                    textBox1.AppendText(".");
                    Thread.Sleep(1000);
                    Application.Exit();
                }
                progressBar1.PerformStep();
            }

            textBox1.AppendText("\r\n檢查完成!");
            timer2.Interval = 500;
            timer2.Start();
        }

        private void CheckEnd()
        {
            Logger.log(this.textBox1.Text);
            //this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
            this.Hide();
            ServerSelect ss = new ServerSelect();
            ss.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            CheckBegin();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            CheckEnd();
        }
    }
}
