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
            //載入LoLToolsX Logo
            PictureBox1.ImageLocation = Directory.GetCurrentDirectory() + @"\logo.png";

            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.TwPath(Directory.GetCurrentDirectory() + @"\config.ini");
           

            //檢查路徑是否存有 LoLTW 字串
            if (!installPath.Contains("LoLTW"))
            {

                if (MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                CFGFile CFGFile = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
                CFGFile.SetValue("LoLPath", "TwPath", installPath);
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                PathLabel.Text = installPath;

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
            }
            catch
            { }

            //刪除物件
            cp = null;
            gr = null;

        }

        
        private void TwTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
                return airVer;
                
            }
            catch
            {
                MessageBox.Show("無法取得LoL版本", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    tbPath.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ProcessStartInfo StartInfo = new ProcessStartInfo();
            StartInfo.WorkingDirectory = System.Environment.CurrentDirectory;
            StartInfo.Verb = "runas";
            StartInfo.FileName = "BakResConsole";
            StartInfo.Arguments = "Backup" + " " + installPath;
            Process.Start(StartInfo);

        }

        private void Button15_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(installPath,websiteIn.Text);
            pe.LobbyLanding();
        }

    }
}


