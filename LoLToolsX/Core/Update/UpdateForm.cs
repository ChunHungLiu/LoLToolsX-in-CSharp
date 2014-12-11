using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace LoLToolsX.Core.Update
{
    public partial class UpdateForm : Form
    {
        string version;
        //string info;
        List<string> info = new List<string>();

        public UpdateForm(string _version,List<string> _info)
        {
            InitializeComponent();
            this.version = _version;     //最新版本
            this.info = _info;           //更新資訊
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            curVer.Text = ProductVersion;
            lastestVer.Text = version;
            foreach (string s in info)
            {
                //寫入更新資訊到TextBox
                textBox1.AppendText(s + "\r\n");
            }
            //取消全選
            textBox1.Select(0, 0);
        }

        /// <summary>
        /// 下載更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //路徑 = github + 版本號碼 + /LoLToolsX + 版本號碼 + .exe
            string downloadPath = "https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/LoLToolsX" + version + "/LoLToolsX" + version + ".exe";

            //建立事件 進度條用
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            try
            {
                if (!Variable.updating)
                {
                    Variable.updating = true;
                    label3.Text = "0";
                    //開始下載更新
                    wc.DownloadFileAsync(new Uri(downloadPath), Variable.CurrentDirectory + @"\download\" + @"LoLToolsX.exe");
                }
            }
            catch
            {
                Variable.haveUpdate = false;
                Variable.updating = false;
                MessageBox.Show("下載更新失敗!");
                return;
            }
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            int value = int.Parse(Math.Truncate(percentage).ToString());
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            label3.Text = value.ToString();
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Variable.updating = false;
            MessageBox.Show("更新下載完成! 按確定安裝更新");
            Logger.log("更新下載成功!");
            Logger.log("啟動Updater.exe進行gengx");

            Process.Start(new ProcessStartInfo() {
                FileName = "Updater.exe",
                WorkingDirectory = Variable.CurrentDirectory
            });

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //取消更新
            this.Dispose();
            Variable.haveUpdate = false;
            Variable.updating = false;
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Variable.haveUpdate = false;
            Variable.updating = false;
        }
    }
}
