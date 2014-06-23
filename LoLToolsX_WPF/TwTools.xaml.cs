using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoLToolsX
{

    /// <summary>
    /// 台服工具
    /// </summary>

    public partial class TwTools : Window
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
                DialogResult dr = new DialogResult();
                dr = System.Windows.Forms.MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                PathLabel.Content = installPath;

            }
            



            //string test = Directory.GetCurrentDirectory();
            //MessageBox.Show(test);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //取得目前伺服器
            CheckProp cp = new CheckProp();
            cp.CheckPropFL(installPath);
            serverLocation.Content = cp.currentLoc;

            notifyIcon1.Visible = false;
            PathLabel.Content = installPath;

            //取得版本資訊
            toolsVersion.Content = Application.ProductVersion.ToString();
            LoLVersionLabel.Content = GetLoLVer();


            //在綫統計使用人數
            try
            {
                WebBrowser1.Navigate("http://lolnx.pixub.com/loltoolsx/stat.html");
            }
            catch
            { }

            //設定伺服器陣列

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
            FileStream fs = new FileStream(installPath + @"\Game\client.ver", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            FileStream fs2 = new FileStream(installPath + @"\lol.version", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs2);
            try
            {
                string gameVer = sr.ReadLine();
                string airVer = sr2.ReadLine();
                return gameVer + " - " + airVer;
                
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("無法取得LoL版本", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "未知";
            }
            finally
            {
                sr.Close();
                sr2.Close();
                fs.Close();
                fs2.Close();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolt.properties");
            serverLocation.Content = "台服";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lols.properties");
            serverLocation.Content = "SEA服";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loloce.properties");
            serverLocation.Content = "大洋洲服";
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loln.properties");
            serverLocation.Content = "美服";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lole.properties");
            serverLocation.Content = "EUW服";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolp.properties");
            serverLocation.Content = "PBE服";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolk.properties");
            serverLocation.Content = "韓服";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loleune.properties");
            serverLocation.Content = "EUNE服";
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

            if (serverLocation.Content == "台服")
            {
                    sg.StartGarena();
            }
            else if (serverLocation.Content == "SEA服")
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

    }
}


