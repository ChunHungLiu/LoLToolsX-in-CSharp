using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace LoLToolsX.Functions.Update
{
    public partial class UpdateForm : Form
    {
        string version;
        //string info;
        List<string> info = new List<string>();
        bool updating = false;

        public UpdateForm(string _version,List<string> _info)
        {
            InitializeComponent();
            this.version = _version;
            this.info = _info;
            //this.info = _info;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            Variable.haveUpdate = true;
            curVer.Text = ProductVersion;
            lastestVer.Text = version;
            foreach (string s in info)
            {
                textBox1.AppendText(s + "\r\n");
            }
            //取消全選
            textBox1.Select(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string downloadPath = "https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/LoLToolsX" + version + "/LoLToolsX" + version + ".exe";

            //建立事件 進度條用
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            try
            {
                if (!updating)
                {
                    updating = true;
                    label3.Text = "0";
                    wc.DownloadFileAsync(new Uri(downloadPath), Application.StartupPath + @"\download\" + @"LoLToolsX.exe");
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
            updating = false;
            Variable.updating = false;
            MessageBox.Show("更新下載完成! 按確定安裝更新");
            Logger.log("更新下載成功!");
            Logger.log("啟動Updater.exe進行gengx");
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = "Updater.exe";
            pi.WorkingDirectory = Application.StartupPath;
            Process.Start(pi);
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
    }
}
