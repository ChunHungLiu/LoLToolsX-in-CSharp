using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LoLToolsX.Core;

namespace LoLToolsX
{
    public partial class VerSelect : Form
    {
        string installPath = "";

        public VerSelect()
        {
            InitializeComponent();
        }

        private void VerSelect_Load(object sender, EventArgs e)
        {
            CFGFile CFGFile = new CFGFile(Application.StartupPath + @"\config.ini");

            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.NaPath(Application.StartupPath + @"\config.ini");
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

                        CFGFile.SetValue("LoLPath", "NaPath", "\"" + installPath + "\"");
                        CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                        Logger.log("LoL目錄檢查成功! ", Logger.LogType.Info);
                        Logger.log("LoL目錄寫入成功! " + installPath, Logger.LogType.Info);

                        string[] folder = Directory.GetDirectories(installPath + @"\RADS\projects\lol_air_client\releases");
                        foreach (string f in folder)
                        {
                            airBox.Items.Add(Path.GetFileName(f));
                        }

                        string[] folder2 = Directory.GetDirectories(installPath + @"\RADS\projects\lol_game_client\releases");
                        foreach (string f in folder2)
                        {
                            gameBox.Items.Add(Path.GetFileName(f));
                        }
                        Variable.n_installPath = this.installPath;
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
                CFGFile.SetValue("LoLPath", "NaPath", "\"" + installPath + "\"");
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                Logger.log("LoL目錄檢查成功! ", Logger.LogType.Info);
                Logger.log("LoL目錄寫入成功! " + installPath, Logger.LogType.Info);

                string[] folder = Directory.GetDirectories(installPath + @"\RADS\projects\lol_air_client\releases");
                foreach (string f in folder)
                {
                    airBox.Items.Add(Path.GetFileName(f));
                }

                string[] folder2 = Directory.GetDirectories(installPath + @"\RADS\projects\lol_game_client\releases");
                foreach (string f in folder2)
                {
                    gameBox.Items.Add(Path.GetFileName(f));
                }
                Variable.n_installPath = this.installPath;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (airBox.SelectedItem != null && gameBox.SelectedItem != null)
            {
                Variable.airVer = airBox.SelectedItem.ToString();
                Variable.gameVer = gameBox.SelectedItem.ToString();
                NaTools nt = new NaTools();
                nt.Show();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("請先選擇版本");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
