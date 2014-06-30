using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LoLToolsX
{
    public partial class LobbyUI : Form
    {
        string installPath = "";
        string modPath = "";
        string assetsPath = "";
        string[] filesPath;

        public LobbyUI(string installPath)
        {
            InitializeComponent();
            this.installPath = installPath;
            this.modPath = installPath + @"\Air\mod";
            this.assetsPath = installPath + @"\Air\assets";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //string[] files = openFileDialog1.FileNames;
                string[] s_files = openFileDialog1.SafeFileNames;
                foreach (string f in s_files)
                {
                    listBox1.Items.Add(f);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;  
                filesPath = Directory.GetFiles(folderPath);
                foreach (string f in Directory.GetFiles(folderPath))
                {
                    listBox1.Items.Add(Path.GetFileName(f));    //只回傳FileName
                }
                
            }

        }

        private void LobbyUI_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items != null)
            {
                try
                {
                    foreach (string i in listBox1.Items)
                    {
                        FileInfo fi = new FileInfo(i);
                        MessageBox.Show(i);
                        if (i.Contains("landingPage"))
                            fi.CopyTo(modPath + @"\lp\themes\parchment\" + i ,true);
                        else if (i.Contains("buddypane") | i.Contains("dockedchat"))
                            fi.CopyTo(modPath + @"\cht2\themes\parchment\" + i , true);
                        else if (i.Contains("chrome"))
                            fi.CopyTo(modPath + @"\chrome\themes\parchment\" + i, true);
                        else if (i.Contains("openstore"))
                        {
                            fi.CopyTo(assetsPath + @"\sounds\" + i, true);
                        }
                        else if (i.Contains("login.mp3") | i.Contains("playbutton"))
                        {
                            if (!Directory.Exists(assetsPath + @"\sounds\newSounds\"))
                            {
                                Directory.CreateDirectory(assetsPath + @"\sounds\newSounds\");
                            }
                            fi.CopyTo(assetsPath + @"\sounds\newSounds\" + i, true);
                        }
                        else
                        {
                            MessageBox.Show("安裝跳過: " + i, "提示" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("安裝失敗:\r\n" + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("大廳UI安裝失敗 : ", Logger.LogType.Error);
                    Logger.log(ex, Logger.LogType.Error);
                }
             }
         }
    }
}
