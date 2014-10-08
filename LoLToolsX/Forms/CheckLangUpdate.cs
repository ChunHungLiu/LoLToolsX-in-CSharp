using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using SevenZip;
using System.Xml;
using System.Xml.Linq;
using LoLToolsX.Core;

namespace LoLToolsX.Forms
{
    public partial class CheckLangUpdate : Form
    {
        //CFGFile ini = new CFGFile(Application.StartupPath + @"\config.ini");
        //MD5CryptoServiceProvider CheckLang = new MD5CryptoServiceProvider();
        string newVer;

        public CheckLangUpdate()
        {
            InitializeComponent();
        }

        private void CheckLangUpdate_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            this.Enabled = false;
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/u/7084520/LoLToolsX/lang/" + newVer + ".zip"), Application.StartupPath + @"\download\pack.zip");

        }
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            int value = int.Parse(Math.Truncate(percentage).ToString());
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            label1.Text = "更新正在安裝更新... 請稍候...";
            File.Create(Application.StartupPath + "\\files\\lang\\eng\\game\\" + newVer + ".txt").Close();
            SevenZipExtractor sz = new SevenZipExtractor(Application.StartupPath + @"\download\pack.zip");
            sz.ExtractArchive(Application.StartupPath + @"\files\lang");
            File.Delete(Application.StartupPath + @"\download\pack.zip");

            if (MessageBox.Show("更新完成") == DialogResult.OK)
            {
                this.Dispose();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nitroxenon.com/loltoolsx/lang/update.txt");
            WebResponse response = (WebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            newVer = sr.ReadToEnd();

            if (!File.Exists(Application.StartupPath + "\\files\\lang\\eng\\game\\" + newVer + ".txt"))
            {
                //有更新
                button1.Enabled = true;
                label1.Text = "發現可用更新 按更新按鈕下載更新";
            }
            else
            {
                button1.Enabled = false;
                label1.Text = "沒有可用更新!";
            }

            sr.Close();
            stream.Close();
            response.Close();

            #region "Old method"
            /*
            //獲取本機檔案MD5 (TW)
            FileStream tw_fs = new FileStream(Application.StartupPath + @"\files\lang\cht\game\fontconfig_en_US.txt", FileMode.Open, FileAccess.Read);
            byte[] output = CheckLang.ComputeHash(tw_fs);
            tw_fs.Close();
            tw_final = BitConverter.ToString(output);
            tw_final = tw_final.Replace("-", "");
            ini.SetValue("MD5", "TW",tw_final);

            //NA
            FileStream na_fs = new FileStream(Application.StartupPath + @"\files\lang\eng\game\fontconfig_en_US.txt", FileMode.Open, FileAccess.Read);
            byte[] output2 = CheckLang.ComputeHash(na_fs);
            na_fs.Close();
            na_final = BitConverter.ToString(output2);
            na_final = na_final.Replace("-", "");
            ini.SetValue("MD5", "NA",na_final);

            //在綫獲取最新版MD5
            try
            {
                string md5sum = "";
                WebClient client = new WebClient();
                //string tmp = client.DownloadString("http://nitroxenon.com/loltoolsx/MD5/md5.xml");
                client.DownloadFile("http://nitroxenon.com/loltoolsx/MD5/md5.xml",Application.StartupPath + "\\download\\md5.xml");
                XDocument doc = XDocument.Load(Application.StartupPath + "\\download\\md5.xml");
                var tmp = doc.Descendants("MD5");
                foreach (var s in tmp)
                {
                    md5sum = s.Value;
                }
                
                //client.DownloadFile("http://nitroxenon.com/loltoolsx/MD5/md5.txt", Application.StartupPath + @"\download\md5.txt");
                //string tmp = File.ReadAllText(Application.StartupPath + @"\download\md5.txt");

                //對比本地檔案和最新檔案的MD5
                if (na_final != md5sum)
                {
                    //有更新
                    button1.Enabled = true;
                    label1.Text = "發現可用更新 按更新按鈕下載更新";
                }
                else
                {
                    //沒有更新
                    label1.Text = "沒有可用更新";
                }
            }
            catch
            {
                label1.Text = "檢查更新失敗!";
            }
             * */
            #endregion
        }
    }
}
