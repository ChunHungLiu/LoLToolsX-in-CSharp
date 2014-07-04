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

        public NaTools()
        {
            InitializeComponent();
        }

        private void NaTools_Load(object sender, EventArgs e)
        {
            CFGFile checkAutoUpdate = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
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

            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            //寫入目前LoLToolsX版本到Log
            Logger.log("LoLToolsX版本: " + Application.ProductVersion, Logger.LogType.Info);

            Logger.log("目前客戶端 : 美服", Logger.LogType.Info);
            //載入LoLToolsX Logo
            PictureBox1.ImageLocation = Directory.GetCurrentDirectory() + @"\logo.png";
            Logger.log("LoLToolsX Logo載入成功!", Logger.LogType.Info);

            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.NaPath(Directory.GetCurrentDirectory() + @"\config.ini");
            Logger.log("LoL目錄取得成功! " + installPath, Logger.LogType.Info);



            //檢查路徑是否存有 LoLTW 字串
            if (!installPath.Contains("League of Legends"))
            {

                if (MessageBox.Show("無法取得LoL目錄 請手動選擇 League of Legends 目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    Logger.log("LoL手動選擇目錄! ", Logger.LogType.Info);
                    if (folderBrowserDialog1.SelectedPath.Contains("League of Legends"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                        Logger.log("LoL目錄檢查成功! " + installPath, Logger.LogType.Info);
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.log("LoL目錄檢查失敗 ", Logger.LogType.Error);
                        Logger.log("強制關閉程式... ", Logger.LogType.Info);
                        Application.Exit();
                    }
                }
                else
                {
                    Logger.log("LoL目錄選擇取消 ", Logger.LogType.Error);
                    Logger.log("關閉程式... " + installPath, Logger.LogType.Info);
                    Application.Exit();
                }
            }
            else
            {
                CFGFile CFGFile = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
                CFGFile.SetValue("LoLPath", "NaPath", installPath);
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                PathLabel.Text = installPath;
                Logger.log("LoL目錄檢查成功! ", Logger.LogType.Info);
                Logger.log("LoL目錄寫入成功! " + installPath, Logger.LogType.Info);


            }

            VerSelect vs = new VerSelect();
            installPath = vs.NaVer(installPath);


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
            //LoLVersionLabel.Text = GetLoLVer();



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
            gr = null;

            Variable.v_installPath = installPath;
        }
    }
}
