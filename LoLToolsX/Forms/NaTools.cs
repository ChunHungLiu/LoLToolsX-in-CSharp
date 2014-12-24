using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using LoLToolsX.Core;
using LoLToolsX.Core.Update;

namespace LoLToolsX
{
    public partial class NaTools : Form
    {
        ToolTip tip2 = null;
        ToolTip tip = null;
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

            if (!Variable.allowBakRes)
            {
                button24.Enabled = false;
                button25.Enabled = false;
            }
            //Initialize Na Path
            installPath = Variable.n_installPath;
            airVer = Variable.airVer;
            gameVer = Variable.gameVer;
            Variable.airPath = airPath = installPath + @"\RADS\projects\lol_air_client\releases\" + airVer + @"\deploy";
            gamePath = installPath + @"\RADS\projects\lol_game_client\releases\" + gameVer + @"\deploy";

            if (Variable.allowUpdate)
            {
                CFGFile checkAutoUpdate = new CFGFile(Variable.CurrentDirectory + @"\config.ini");
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
            PictureBox1.Image = Properties.Resources.logo;
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
                WebBrowser1.Navigate("http://nitroxenon.com/loltoolsx/stat.html");
            }
            catch (Exception e2)
            {
                Logger.log("使用人數統計失敗", Logger.LogType.Error);
                Logger.log(e2);
            }

            //刪除物件
            cp = null;

            //Variable.na_installPath = installPath;
            Variable.curClient = "美服";
        }

            
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.log("開啟NitroXenon的BLOG...", Logger.LogType.Info);
            Process.Start("http://nitroxenon.com");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "lolt.properties");
            }
            else
            {
                SwitchServer.localEdit(airPath, "zh_TW");
            }
            serverLocation.Text = "台服";
            
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "lols.properties");
            }
            else
            {
                MessageBox.Show("此伺服器不支緩安全切換方法");
            }
            serverLocation.Text = "SEA服";
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "loloce.properties");
            }
            else
            {
                MessageBox.Show("此伺服器不支緩安全切換方法");
            }
            serverLocation.Text = "大洋洲服";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "loln.properties");
            }
            else
            {
                SwitchServer.localEdit(airPath, "en_US");
            }
            serverLocation.Text = "美服";
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "lole.properties");
            }
            else
            {
                MessageBox.Show("此伺服器不支緩安全切換方法");
            }
            serverLocation.Text = "EUW服";
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "lolp.properties");
            }
            else
            {
                MessageBox.Show("此伺服器不支緩安全切換方法");
            }
            serverLocation.Text = "PBE服";
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "lolk.properties");
            }
            else
            {
                SwitchServer.localEdit(airPath, "ko_KR");
            }
            serverLocation.Text = "韓服";
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (oldSwitch.Checked)
            {
                SwitchServer.SwitchServerLocNa(airPath, "loleune.properties");
            }
            else
            {
                MessageBox.Show("此伺服器不支緩安全切換方法");
            }
            serverLocation.Text = "EUNE服";
            
        }

        private void backProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(1, 2);
        }

        private void restoneProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(2, 2);
        }

        private void delBakProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(3, 2);
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(1, 2);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(2, 2);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(3, 2);
        }

        private void startLoL_Click(object sender, EventArgs e)
        {
            this.Opacity = 50;

            StartGame sg = new StartGame(null,this);

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

        }

        private void lEng_Click(object sender, EventArgs e)
        {

        }

        private void BakLang_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.NaLang(1);
        }

        private void ResLang_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.NaLang(2);
        }

        private void delLangBak_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.NaLang(3);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nitroxenon.com/lol-tools-tw/lol-lobby-theme/");
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
                "216.52.241.254",
                "203.117.155.133",
                "www.leagueoflegends.co.kr",
                "95.172.70.254",
                "190.93.254.13",
                "lq.oc1.lol.riotgames.com",
                "66.150.148.64"
            };

            for (int i = 0; i <= 7; i++)
            {
                //if (StatusCheck.pingCheck(serverAry[i]))
                //{
                string tmp = Utility.PingCheck(serverAry[i]);
                if (tmp != "請求逾時" & tmp != "不明")
                {
                    labels[i].ForeColor = System.Drawing.Color.Green;
                }
                else if (tmp == "不明")
                {
                    labels[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    labels[i].ForeColor = System.Drawing.Color.Red;
                }
                labels[i].Text = tmp;
                Logger.log("Server Status Check:", Logger.LogType.Info);
                Logger.log(serverAry[i] + " : 正常", Logger.LogType.Info);
            }
            //else
            //{
            //labels[i].Text = "請求逾時";
            //labels[i].ForeColor = System.Drawing.Color.Red;
            // Logger.log("Server Status Check:", Logger.LogType.Info);
            //Logger.log(serverAry[i] + " : 請求逾時", Logger.LogType.Info);
            //}


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
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(1, 2);
        }

        private void Button18_Click_1(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(2, 2);
        }

        private void Button16_Click_1(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(airPath);
            br.Prop(3, 2);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(airPath, "http://landing.leagueoflegends.com/spectator_swf/landingPage.swf", 2);
            pe.LobbyLanding();
        }

        private void NaTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void chooseHUD_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Variable.hudPath = openFileDialog2.FileName;
                Logger.log("已選擇hudPath路徑: " + Variable.hudPath);
            }
        }

        private void installHUD_Click(object sender, EventArgs e)
        {
            InstallUI.NaGameUI(installPath);
        }

        private void installHUD_MouseEnter(object sender, EventArgs e)
        {
            if (tip == null)
            {
                tip = new ToolTip();
                tip.Show("目前美服UI安裝會出現短暫假死現象 並非程式當掉", installHUD);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(installPath);
            br.NaUi(1);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(installPath);
            br.NaUi(2);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(installPath);
            br.NaUi(3);
        }

        private void Button21_MouseEnter(object sender, EventArgs e)
        {
            if (tip2 == null)
            {
                tip2 = new ToolTip();
                tip2.Show("目前美服UI還原會出現短暫假死現象 並非程式當掉", Button21);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            MessageBox.Show("快捷:\r\n修改 lol.properties 檔案\r\n需要先啟動遊戲 再進行切換\r\n\r\n進階:\r\n修改 local.properties 檔案\r\n需要先切換再啟動遊戲\r\n\r\n如快捷方法無法登入 請使用進階方法進行切換\r\n\r\n!!注意!!使用進階方法切換韓服 語言和語音有機會變成韓文和韓語", "切換方法說明", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
