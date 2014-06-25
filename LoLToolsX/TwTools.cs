using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            //除錯模式
            //Debug.debug = true;
            //wpfLog wpLog = new wpfLog();
            //wpLog.Show();

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
            Logger.log("關閉程式...", Logger.LogType.Info);
            Environment.Exit(Environment.ExitCode);
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

    }
}


