using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using LoLToolsX.Core;

namespace LoLToolsX
{
    public partial class FileCheck : Form
    {
        //檢查完畢事件
        private delegate void CheckFinishHandler();
        private event CheckFinishHandler CheckFinish;
        WebClient client = new WebClient();

        public FileCheck()
        {
            InitializeComponent();

            CheckFinish += delegate
            {
                Logger.log(this.textBox1.Text);

                //釋放資源
                timer1 = null;
                client = null;
                this.Hide();
                //var thread = new Thread(() => Application.Run(new ServerSelect()));
                //thread.Start();
                ServerSelect ss = new ServerSelect();
                ss.Show();
            };

            timer1.Interval = 500;
            timer1.Tick += delegate {
                timer1.Stop();
                CheckBegin();
            };
        }

        private void FileCheck_Load(object sender, EventArgs e)
        {
            timer1.Start();     
        }

        private void CheckBegin()
        {
            // Logger 開始記錄
            if (!Directory.Exists(Variable.CurrentDirectory + @"\Logs"))
                Directory.CreateDirectory(Variable.CurrentDirectory + @"\Logs");

            Logger.start();

            Logger.log("LoLToolsX 啟動路徑 : " + Variable.CurrentDirectory);

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
                                  //"\\files\\lang\\kr",
                              };

            string[] files = {
                                  "\\Skin.txt",
                                  "\\LoLBakRes.exe",
                                  "\\Updater.exe",
                                  "\\Logs\\Log.txt",
                                  "\\SevenZipSharp.dll",
                                  "\\7z.dll",
                                  "\\LICENSE.txt",
                                  "\\Disclaimer.txt",
                                  "\\Privacy.txt",
                                  "\\files\\fix-fd\\dependencies.properties",
                                  "\\files\\fix-fd\\info.riotmod",
                                  "\\files\\fix-fd\\mod_cht2.dat"
                             };

            progressBar1.Minimum = 0;
            progressBar1.Maximum = folder.Length + files.Length;
            progressBar1.Step = 1;

            textBox1.AppendText("檔案檢查開始!");

            foreach (string str in files)
            {
                string path = Variable.CurrentDirectory + str;
                textBox1.AppendText(String.Format("\r\n檢查 {0}", path));
                if (!File.Exists(path))
                {
                    textBox1.AppendText(String.Format("\r\n找不到 {0} 檔案", path));
                    switch (path)
                    {
                        case "\\Skin.txt":
                            File.Create(Variable.CurrentDirectory + "\\Skin.txt");
                            break;
                        case "\\Logs\\Log.txt":
                            File.Create(Variable.CurrentDirectory + "\\Logs\\Log.txt");
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
                                client.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/SevenZipSharp/SevenZipSharp.dll", Variable.CurrentDirectory + "\\SevenZipSharp.dll");
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
                            try
                            {
                                client.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/7z/7z.dll", Variable.CurrentDirectory + "\\7z.dll");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                        case "\\LICENSE.txt":
                            textBox1.AppendText("\r\n程式授權文件遺失 正在下載...");
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/LICENSE.txt", Variable.CurrentDirectory + "\\LICENSE.txt");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                        case "\\重要聲明.txt":
                            textBox1.AppendText("\r\n程式聲明文件遺失 正在下載...");
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/Disclaimer.txt", Variable.CurrentDirectory + "\\重要聲明.txt");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                        case "\\隱私權聲明.txt":
                            textBox1.AppendText("\r\n程式隱私權聲明文件遺失 正在下載...");
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/Privacy.txt", Variable.CurrentDirectory + "\\隱私權聲明.txt");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch
                            {
                                textBox1.AppendText("\r\n下載失敗");
                                continue;
                            }
                        case "\\files\\fix-fd\\dependencies.properties":
                            if (!Directory.Exists(Variable.CurrentDirectory + "\\files\\fix-fd"))
                            {
                                Directory.CreateDirectory(Variable.CurrentDirectory + "\\files\\fix-fd");
                            }
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/fix-fd/dependencies.properties", Variable.CurrentDirectory + "\\files\\fix-fd\\dependencies.properties");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch { textBox1.AppendText("\r\n下載失敗"); continue; }
                        case "\\files\\fix-fd\\info.riotmod":
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/fix-fd/info.riotmod", Variable.CurrentDirectory + "\\files\\fix-fd\\info.riotmod");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch { textBox1.AppendText("\r\n下載失敗"); continue; }
                        case "\\files\\fix-fd\\mod_cht2.dat":
                            try
                            {
                                client.DownloadFile("http://nitroxenon.com/loltoolsx/files/fix-fd/mod_cht2.dat", Variable.CurrentDirectory + "\\files\\fix-fd\\mod_cht2.dat");
                                textBox1.AppendText("\r\n下載完成");
                                break;
                            }
                            catch { textBox1.AppendText("\r\n下載失敗"); continue; }
                    }

                }
                progressBar1.PerformStep();
            }

            foreach (string f in folder)
            {
                string path = Variable.CurrentDirectory + f;
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
                    try
                    {
                        client.DownloadFile("https://dl.dropboxusercontent.com/u/7084520/LoLToolsX/KR_lang/kr.zip", Variable.CurrentDirectory + "\\download\\kr.zip");
                       
                    }
                    catch
                    {
                        MessageBox.Show("韓文語言檔案下載失敗 將關閉韓文語言切換功能");
                        Thread.Sleep(3000);
                        continue;
                    }

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

            folder = null;
            files = null;

            CheckFinish();   //觸發檢查完畢事件
        }
    }
}
