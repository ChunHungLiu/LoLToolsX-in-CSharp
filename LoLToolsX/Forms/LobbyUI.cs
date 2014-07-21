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
    /// <summary>
    /// ListBox1 放 FileName
    /// ListBox2 放 Path
    /// </summary>

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


        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox2.Items.Remove(listBox1.SelectedIndex);
        }


        private void LobbyUI_Load(object sender, EventArgs e)
        {
            this.listBox1.AllowDrop = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.Items != null)
            {
                try
                {
                    foreach (string i in listBox2.Items)
                    {
                        FileInfo fi = new FileInfo(i);
                        //MessageBox.Show(i,j);
                        if (i.Contains("landingPage"))
                            fi.CopyTo(modPath + @"\lp\themes\parchment\landingPageAssets.swf", true);
                        else if (i.Contains("buddypane"))
                            fi.CopyTo(modPath + @"\cht2\themes\parchment\buddyPanelAssets.swf", true);
                        else if (i.Contains("dockedchat"))
                            fi.CopyTo(modPath + @"\cht2\themes\parchment\dockedChatAssets.swf", true);
                        else if (i.Contains("chrome"))
                            fi.CopyTo(modPath + @"\chrome\themes\parchment\chromeAssets.swf", true);
                        else if (i.Contains("openstore"))
                            fi.CopyTo(assetsPath + @"\sounds\openstore.mp3", true);
                        else if (i.Contains("login.mp3"))
                        {
                            if (!Directory.Exists(assetsPath + @"\sounds\newSounds\"))
                            {
                                Directory.CreateDirectory(assetsPath + @"\sounds\newSounds\");
                            }
                            fi.CopyTo(assetsPath + @"\sounds\newSounds\login.mp3", true);
                        }
                        else if (i.Contains("playbutton"))
                            fi.CopyTo(assetsPath + @"\sounds\newSounds\playbutton.mp3", true);
                        else
                        {
                            MessageBox.Show("安裝跳過: " + i, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Logger.log("大廳UI安裝: " + i, Logger.LogType.Info);
                    }
                    MessageBox.Show("安裝成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("大廳UI安裝成功!", Logger.LogType.Info);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("安裝失敗:\r\n" + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("大廳UI安裝失敗 : ", Logger.LogType.Error);
                    Logger.log(ex, Logger.LogType.Error);
                }
            }
            else
            {
                MessageBox.Show("請先添加資源!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Wait wait = new Wait();
                wait.Show();
                wait.progressBar1.Value = 40;
                My.Computer.FileSystem.CopyDirectory(installPath + @"\Air", Application.StartupPath + @"\bak\Air", true);
                wait.progressBar1.Value = 100;
                wait.Dispose();
                Logger.log("大廳UI備份成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI備份失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI備份失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Wait wait = new Wait();
                wait.Show();
                wait.progressBar1.Value = 40;
                My.Computer.FileSystem.CopyDirectory(Application.StartupPath + @"\bak\Air", installPath + @"\Air", true);
                wait.progressBar1.Value = 100;
                wait.Dispose();
                Logger.log("大廳UI還原成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI還原成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                Logger.log("大廳UI還原失敗 : 沒有備份", Logger.LogType.Error);
                MessageBox.Show("大廳UI還原失敗 : 沒有備份", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI還原失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI還原失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + @"\bak\Air", Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                Logger.log("大廳UI刪除備份成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI刪除備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                Logger.log("大廳UI刪除備份失敗 : 沒有備份", Logger.LogType.Error);
                MessageBox.Show("大廳UI刪除備份失敗 : 沒有備份", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI刪除備份失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI刪除備份失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //string[] s_files = openFileDialog1.SafeFileNames;
                listBox1.Items.Add(openFileDialog1.SafeFileName);
                listBox2.Items.Add(openFileDialog1.FileName);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;
                filesPath = Directory.GetFiles(folderPath);
                foreach (string f in filesPath)
                {
                    listBox1.Items.Add(Path.GetFileName(f));    //只回傳FileName
                    listBox2.Items.Add(f);
                }

            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
            }
        }
    }
}