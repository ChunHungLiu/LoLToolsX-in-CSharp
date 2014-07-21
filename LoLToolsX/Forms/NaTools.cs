using System;
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
    public partial class NaTools : Form
    {
        public static string installPath = "";
        public static string airVer = "";
        public static string gameVer = "";
        public static string airPath = "";
        public static string gamePath = "";
        TwTools tt = new TwTools();

        public NaTools()
        {
            InitializeComponent();
        }

        private void NaTools_Load(object sender, EventArgs e)
        {
            Variable.airPath = airPath;

            if (!Variable.allowBakRes)
            {
                button24.Enabled = false;
                button25.Enabled = false;
            }
            //Initialize Na Path
            installPath = Variable.n_installPath;
            airVer = Variable.airVer;
            gameVer = Variable.gameVer;
            airPath = installPath + @"\RADS\projects\lol_air_client\releases\" + airVer + @"\deploy";
            gamePath = installPath + @"\RADS\projects\lol_game_client\releases\" + gameVer + @"\deploy";

            if (Variable.allowUpdate)
            {
                CFGFile checkAutoUpdate = new CFGFile(Application.StartupPath + @"\config.ini");
                if (checkAutoUpdate.GetValue("LoLToolsX", "AutoUpdate") == "true")
                {
                    Variable.updating = true;
                    this.checkBox1.Checked = false;
                    Thread updateThread = new Thread(CheckUpdate.checkUpdate);
                    updateThread.Start();
                }
                else
                {
                    this.checkBox1.Checked = true;
                }
            }
            else
            {
                button23.Enabled = false;
            }

            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            //寫入目前LoLToolsX版本到Log
            Logger.log("LoLToolsX版本: " + Application.ProductVersion, Logger.LogType.Info);

            Logger.log("目前客戶端 : 美服", Logger.LogType.Info);
            //載入LoLToolsX Logo
            PictureBox1.ImageLocation = Application.StartupPath + @"\logo.png";
            Logger.log("LoLToolsX Logo載入成功!", Logger.LogType.Info);

            
            //取得目前伺服器;
            CheckProp cp = new CheckProp();
            cp.CheckPropNa(airPath);
            serverLocation.Text = cp.currentLoc;

            notifyIcon1.Visible = false;
            PathLabel.Text = installPath;

            //取得版本資訊
            toolsVersion.Text = Application.ProductVersion.ToString();



            //在綫統計使用人數
            try
            {
                WebBrowser1.Navigate("http://lolnx.pixub.com/loltoolsx/stat.html");
                Logger.log("使用人數統計: http://lolnx.pixub.com/loltoolsx/stat.html ", Logger.LogType.Info);
            }
            catch (Exception e2)
            {
                Logger.log("使用人數統計失敗", Logger.LogType.Error);
                Logger.log(e2);
            }

            //刪除物件
            cp = null;

            Variable.v_installPath = installPath;
            Variable.curClient = "美服";
        }

            
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.log("開啟NitroXenon的BLOG...", Logger.LogType.Info);
            Process.Start("http://lolnx.pixub.com");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "lolt.properties");
            serverLocation.Text = "台服";
            
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "lols.properties");
            serverLocation.Text = "SEA服";
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "loloce.properties");
            serverLocation.Text = "大洋洲服";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "loln.properties");
            serverLocation.Text = "美服";
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "lole.properties");
            serverLocation.Text = "EUW服";
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "lolp.properties");
            serverLocation.Text = "PBE服";
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "lolk.properties");
            serverLocation.Text = "韓服";
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLocNa(airPath, "loleune.properties");
            serverLocation.Text = "EUNE服";
            
        }

        private void backProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(1, 2);
        }

        private void restoneProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(2, 2);
        }

        private void delBakProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(3, 2);
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(1, 2);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(2, 2);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(3, 2);
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

                sg.naLauncher();
                //this.Hide();
                //this.ShowInTaskbar = false;
                //this.notifyIcon1.Visible = true;
                //this.notifyIcon1.ShowBalloonTip(5000, "", "遊戲啟動成功", ToolTipIcon.None);


            }
            this.Dispose();
        }

        private void lChin_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(airPath);
            sl.ChinLobby(2);
        }

        private void lEng_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(airPath);
            sl.EngLobby(2);
        }

        private void BakLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.NaLang(1);
        }

        private void ResLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.NaLang(2);
        }

        private void delLangBak_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.NaLang(3);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://lolnx.pixub.com/lol-tools-tw/lol-lobby-theme/");
        }

        private void checkServerStatus_Click(object sender, EventArgs e)
        {
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

        private void NaTools_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (Variable.allowUpdate)
            {
                if (!Variable.updating)
                {
                    Thread updateThread = new Thread(CheckUpdate.checkUpdate);
                    updateThread.Start();
                }
                else
                {
                    MessageBox.Show("正在檢查更新, 請稍候...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(airPath, websiteIn.Text,2);
            pe.LobbyLanding();
        }

        private void Button17_Click_1(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(1, 2);
        }

        private void Button18_Click_1(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(2, 2);
        }

        private void Button16_Click_1(object sender, EventArgs e)
        {
            BakRes br = new BakRes(airPath);
            br.Prop(3, 2);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(airPath, "http://landing.leagueoflegends.com/spectator_swf/landingPage.swf", 2);
            pe.LobbyLanding();
        }
    }
}
