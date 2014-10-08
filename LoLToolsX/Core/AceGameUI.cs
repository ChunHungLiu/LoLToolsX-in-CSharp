using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SevenZip;

namespace LoLToolsX.Core
{
    class AceGameUI
    {
        private string installPath;

        public AceGameUI(string _installPath)
        {
            this.installPath = _installPath;
            if (!Directory.Exists(Application.StartupPath + "\\temp"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
        }
        public void Install(string zipPath)
        {
            try
            {
                SevenZipExtractor sze = new SevenZipExtractor(zipPath);
                sze.ExtractArchive(Application.StartupPath + "\\temp");
            }
            catch { MessageBox.Show("解壓失敗! 將會取消安裝程序", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            try
            {
                if (Directory.GetDirectories(Application.StartupPath + "\\temp").Length < 3)
                {
                    foreach (string d in Directory.GetDirectories(Application.StartupPath + "\\temp"))
                    {
                        if (File.Exists(d + "\\lol.properties"))
                        {
#if DEBUG
                            MessageBox.Show("lol.properties Exist!");
#else
#endif
                            try
                            {
                                File.Copy(d + "\\lol.properties", installPath + "\\Air\\lol.properties", true);
                                File.Delete(d + "\\lol.properties");
                            }
                            catch { continue; }
                        }
                        if (Directory.GetDirectories(d).Length >= 3)
                        {
                            foreach (string di in Directory.GetDirectories(d))
                            {
                                var dirName = new DirectoryInfo(di).Name;

                                if (Path.GetDirectoryName(di) == "sounds" || Path.GetDirectoryName(di) == "Sounds")
                                {
                                    My.Computer.FileSystem.CopyDirectory(di, String.Format(installPath + "\\Air\\assets\\sounds", dirName), true);
                                }
                                else
                                {
#if DEBUG
                                    MessageBox.Show(di);
#else
#endif
                                    My.Computer.FileSystem.CopyDirectory(di, String.Format(installPath + "\\Air\\mod\\{0}" ,dirName), true);
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("安裝成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("安裝失敗!\r\n" + ex.Message + "\r\n" + ex.Data, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); Logger.log("AceGameUI install failed : \r\n" + ex.Message + "\r\n" + ex.Data); }

            finally { CleanUp(); }
        }

        private void CleanUp()
        {
            GC.Collect();

            try
            {
                foreach (string dir in Directory.GetDirectories(Application.StartupPath + "\\temp"))
                {
#if DEBUG
                    MessageBox.Show(dir);
#else
#endif
                    Directory.Delete(dir,true);
                }
                foreach (string f in Directory.GetFiles(Application.StartupPath + "\\temp"))
                {
                    File.Delete(f);
                }
            }
            catch(Exception ex) { MessageBox.Show("緩存檔案刪除失敗!\r\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
    class FixFriend
    {
        private string installPath;

        public FixFriend(string _installPath)
        {
            this.installPath = _installPath;
        }

        public void StartFix()
        {
            try
            {
                foreach (string f in Directory.GetFiles((Application.StartupPath + "\\files\\fix-fd")))
                {
                    File.Copy(f, installPath + "\\Air\\mod\\cht2\\" + Path.GetFileName(f), true);
                }
                MessageBox.Show("修復成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { MessageBox.Show("修復失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
