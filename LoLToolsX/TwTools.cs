﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;

namespace LoLToolsX
{

    /// <summary>
    /// 台服工具
    /// </summary>


    public partial class TwTools : Form
    {

        public static string installPath = "";

        public TwTools()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)     //Form1_Load
        {
            CFGFile checkAutoUpdate = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
            if (checkAutoUpdate.GetValue("LoLToolsX", "AutoUpdate") == "true")
            {
                this.checkBox1.Checked = false;
                Thread updateThread = new Thread(CheckUpdate.checkUpdate);
                updateThread.Start();
            }
            else
            {
                this.checkBox1.Checked = true;
            }

            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            //寫入目前LoLToolsX版本到Log
            Logger.log("LoLToolsX版本: " + Application.ProductVersion, Logger.LogType.Info);
            //載入LoLToolsX Logo
            PictureBox1.ImageLocation = Directory.GetCurrentDirectory() + @"\logo.png";
            Logger.log("LoLToolsX Logo載入成功!", Logger.LogType.Info);

            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.TwPath(Directory.GetCurrentDirectory() + @"\config.ini");
            Logger.log("LoL目錄取得成功! " + installPath, Logger.LogType.Info);

           

            //檢查路徑是否存有 LoLTW 字串
            if (!installPath.Contains("LoLTW"))
            {

                if (MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    Logger.log("LoL手動選擇目錄! " , Logger.LogType.Info);
                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                        Logger.log("LoL目錄檢查成功! " + installPath, Logger.LogType.Info);
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.log("LoL目錄檢查失敗 " , Logger.LogType.Error);
                        Logger.log("強制關閉程式... " , Logger.LogType.Info);
                        Application.Exit();
                    }
                }
                else
                {
                    Logger.log("LoL目錄選擇取消 " , Logger.LogType.Error);
                    Logger.log("關閉程式... " + installPath, Logger.LogType.Info);
                    Application.Exit();
                }
            }
            else
            {
                CFGFile CFGFile = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
                CFGFile.SetValue("LoLPath", "TwPath", installPath);
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                PathLabel.Text = installPath;
                Logger.log("LoL目錄檢查成功! " , Logger.LogType.Info);
                Logger.log("LoL目錄寫入成功! " + installPath, Logger.LogType.Info);


            }

           
            

            //string test = Directory.GetCurrentDirectory();
            //MessageBox.Show(test);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //取得目前伺服器
            CheckProp cp = new CheckProp();
            cp.CheckPropFL(installPath);
            serverLocation.Text = cp.currentLoc;

            notifyIcon1.Visible = false;
            PathLabel.Text = installPath;

            //取得版本資訊
            toolsVersion.Text = Application.ProductVersion.ToString();
            LoLVersionLabel.Text = GetLoLVer();



            //在綫統計使用人數
            try
            {
                WebBrowser1.Navigate("http://lolnx.pixub.com/loltoolsx/stat.html");
                Logger.log("使用人數統計: http://lolnx.pixub.com/loltoolsx/stat.html " , Logger.LogType.Info);
            }
            catch (Exception e2)
            {
                Logger.log("使用人數統計失敗", Logger.LogType.Error);
                Logger.log(e2);
            }

            //刪除物件
            cp = null;
            gr = null;

        }

        
        private void TwTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Thread sendThread = new Thread(new ThreadStart(send));
            this.Hide();
            UploadLogs();

        }
        /*
        private void send()
        {
            
        }
        */
        private void UploadLogs()
        {
            //Upload Log
            try
            {
                    Logger.log("關閉程式...", Logger.LogType.Info);
                    Random random = new Random();
                    string rd = random.Next().ToString();
                    string rdFile = Directory.GetCurrentDirectory() + @"\Logs\Log" + rd + ".txt";
                    File.Copy(Directory.GetCurrentDirectory() + @"\Logs\Log.txt", rdFile);
                    //File.Copy(Directory.GetCurrentDirectory() + @"\Logs\Log.txt",Directory.GetCurrentDirectory() + @"\Logs\Log" + rd + ".txt");

                    System.Net.WebClient Client = new System.Net.WebClient();
                    Client.Headers.Add("Content-Type", "binary/octet-stream");
                    byte[] result = Client.UploadFile("http://lolnx.pixub.com/loltoolsx/upload.php", "POST", rdFile);
                    string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);                  

                    Environment.Exit(Environment.ExitCode);

                }
                catch
                {
                    Environment.Exit(Environment.ExitCode);
                }              
        }

        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.log("開啟NitroXenon的BLOG...", Logger.LogType.Info);
            Process.Start("http://lolnx.pixub.com");
        }


        public static string GetLoLVer()
        {
            //取得LoL版本
            //FileStream fs = new FileStream(installPath + @"\Game\client.ver", FileMode.Open);
            //StreamReader sr = new StreamReader(fs);
            FileStream fs2 = new FileStream(installPath + @"\lol.version", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs2);
            try
            {
                //string gameVer = sr.ReadLine();
                string airVer = sr2.ReadLine();
                Logger.log("Air版本: " + airVer, Logger.LogType.Info);
                return airVer;
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show("無法取得LoL版本", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("無法取得Air版本: " , Logger.LogType.Error);
                Logger.log(e);
                return "未知";
            }
            finally
            {
                //sr.Close();
                sr2.Close();
                //fs.Close();
                fs2.Close();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolt.properties");
            serverLocation.Text = "台服";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lols.properties");
            serverLocation.Text = "SEA服";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loloce.properties");
            serverLocation.Text = "大洋洲服";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loln.properties");
            serverLocation.Text = "美服";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lole.properties");
            serverLocation.Text = "EUW服";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolp.properties");
            serverLocation.Text = "PBE服";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolk.properties");
            serverLocation.Text = "韓服";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loleune.properties");
            serverLocation.Text = "EUNE服";
        }

        private void backProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(1);

        }

        private void restoneProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(2);
        }

        private void delBakProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(3);
        }

        private void startLoL_Click(object sender, EventArgs e)
        {
            this.Opacity = 50;

            StartGame sg = new StartGame(installPath);

            if (serverLocation.Text == "台服")
            {
                    sg.StartGarena();
            }
            else if (serverLocation.Text == "SEA服")
            {
              
                    sg.StartGarena();
                    //this.Hide();
                    //this.ShowInTaskbar = false;
                    //this.notifyIcon1.Visible = true;
                    //this.notifyIcon1.ShowBalloonTip(5000, "", "遊戲啟動成功", ToolTipIcon.None);
                
            }
            else
            {
               
                    sg.StartRiotL();
                    //this.Hide();
                    //this.ShowInTaskbar = false;
                    //this.notifyIcon1.Visible = true;
                    //this.notifyIcon1.ShowBalloonTip(5000, "", "遊戲啟動成功", ToolTipIcon.None);
                
                
            }
            
        }

        private void lChin_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.ChinLobby();

        }

        private void lEng_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.EngLobby();
        }

        private void gChin_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.ChinGame();
        }

        private void gEng_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.EngGame();
        }

        private void BakLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(1);
        }

        private void ResLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(2);
        }

        private void delLangBak_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(3);
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://lolnx.pixub.com/sound-pack");
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://lolnx.pixub.com/lol-tools-tw/lol-lobby-theme/");
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.Contains("Sound"))
                {
                    Logger.log("Sound資料夾選擇成功: " +folderBrowserDialog1.SelectedPath, Logger.LogType.Error);
                    tbPath.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Sound資料夾選擇錯誤", Logger.LogType.Error);
                }
            }
        }

        private void installSound_Click(object sender, EventArgs e)
        {
            SwitchSound ss = new SwitchSound(installPath, tbPath.Text);
            ss.SwitchLobby();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            SwitchSound ss = new SwitchSound(installPath, tbPath.Text);
            ss.SwitchGame();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(1);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(2);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(3);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo StartInfo = new ProcessStartInfo();
                StartInfo.UseShellExecute = true;
                StartInfo.WorkingDirectory = System.Environment.CurrentDirectory;
                StartInfo.Verb = "runas";
                StartInfo.FileName = "BakResConsole";
                StartInfo.Arguments = "Backup" + " " + installPath;
                Process.Start(StartInfo);
                Logger.log("LoLToolsX 備份/還原中心 啟動成功!", Logger.LogType.Info);
            }
            catch (Exception e3)
            {
                Logger.log("LoLToolsX 備份/還原中心 啟動失敗", Logger.LogType.Error);
                Logger.log(e3);
            }


        }

        private void Button15_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(installPath,websiteIn.Text);
            pe.LobbyLanding();
        }

        private void chooseHUD_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Path.hudPath = openFileDialog2.FileName;
                Logger.log("已選擇hudPath路徑: " + Path.hudPath);
            }
            
        }

        private void installHUD_Click(object sender, EventArgs e)
        {
            InstallUI.GameUI(installPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CFGFile ini = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
            if (checkBox1.Checked == true)
            ini.SetValue("LoLToolsX", "AutoUpdate", "false");
            else
            ini.SetValue("LoLToolsX", "AutoUpdate", "true");
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            Thread updateThread = new Thread(CheckUpdate.checkUpdate);
            updateThread.Start();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(1);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(2);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(3);
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            ChatEdit ce = new ChatEdit();
            ce.Show();
        }

        private void TwTools_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void checkServerStatus_Click(object sender, EventArgs e)
        {
            /*  伺服器檢查 (失敗)
            TwTools tt = new TwTools();
            string result = Check();
            if (result.Length > 1)
            {
                switch (result)
                {
                    case ("台服"):
                        tt.Label001.Text = "請求逾時";
                        break;
                    case ("美服"):
                        tt.Label002.Text = "請求逾時";
                        break;
                    case ("SEA服"):
                        tt.Label003.Text = "請求逾時";
                        break;
                    case ("韓服"):
                        tt.Label004.Text = "請求逾時";
                        break;
                    case ("EUW服"):
                        tt.Label005.Text = "請求逾時";
                        break;
                    case ("EUNE服"):
                        tt.Label006.Text = "請求逾時";
                        break;
                    case ("大洋洲服"):
                        tt.Label007.Text = "請求逾時";
                        break;
                    case ("PBE服"):
                        tt.Label008.Text = "請求逾時";
                        break;
                }

            }
             */

            Label[] labels = {           this.Label001,
                                         this.Label002,
                                         this.Label003,
                                         this.Label004,
                                         this.Label005,
                                         this.Label006,
                                         this.Label007,
                                         this.Label008};

            string[] serverAry = 
            {   "lol.garena.com",
                "landing.leagueoflegends.com",
                "lol.garena.com",
                "www.leagueoflegends.co.kr",
                "lq.eu.lol.riotgames.com",
                "lq.eun1.lol.riotgames.com",
                "lq.oc1.lol.riotgames.com",
                "d2q6fdmnncz9b0.cloudfront.net"
            };

            for (int i = 0; i <= 7; i++)
            {
                if (StatusCheck.pingCheck(serverAry[i]))
                {
                    labels[i].Text = "正常";
                    labels[i].ForeColor = System.Drawing.Color.Green;
                    Logger.log("Server Status Check:", Logger.LogType.Info);
                    Logger.log(serverAry[i] + " : 正常", Logger.LogType.Info);
                }
                else
                {
                    labels[i].Text = "請求逾時";
                    labels[i].ForeColor = System.Drawing.Color.Red;
                    Logger.log("Server Status Check:", Logger.LogType.Info);
                    Logger.log(serverAry[i] + " : 請求逾時", Logger.LogType.Info);
                }
            }

        }

        /* 伺服器狀態檢查函式 (失敗)
        public static string Check()
         {
             TwTools tt2 = new TwTools();
             tt2.Label001.Text = "dguj";
             try
             {
                 string[] serverAry = 
            {   "lol.garena.com",
                "landing.leagueoflegends.com",
                "lol.garena.com",
                "www.leagueoflegends.co.kr",
                "lq.eu.lol.riotgames.com",
                "lq.eun1.lol.riotgames.com",
                "lq.oc1.lol.riotgames.com",
                "d2q6fdmnncz9b0.cloudfront.net"
            };

                 string[] serverNameAry = 
            {   "台服",
                "美服",
                "SEA服",
                "韓服",
                "EUW服",
                "EUNE服",
                "大洋洲服",
                "PBE服"
            };

                 Logger.log("伺服器狀態查詢");

                 for (int i = 0; i < 7; i++)
                 {
                     Ping ping = new Ping();
                     PingReply pingReply = ping.Send(serverAry[i]);

                     if (pingReply.Status == IPStatus.TimedOut)
                     {
                         return serverNameAry[i];
                     }
                     else
                     {
                         
                         //宣告Label陣列
                         TwTools tt = new TwTools();
                         Label[] labels = {   tt.Label001,
                                         tt.Label002,
                                         tt.Label003,
                                         tt.Label004,
                                         tt.Label005,
                                         tt.Label006,
                                         tt.Label007,
                                         tt.Label008};

                         for (int i2 = 0; i2 < 7; i2++)
                         {
                             
                             MessageBox.Show("fsghd");
                             labels[i2].Text = "正常";
                             labels[i2].ForeColor = System.Drawing.Color.Green;
                         }
                     }
                 }
                 return "";
             }
             catch (Exception e)
             {
                 Logger.log("伺服器檢查失敗!", Logger.LogType.Error);
                 Logger.log(e, Logger.LogType.Error);
                 return "";
             }
            
        }
         */
    }
}


