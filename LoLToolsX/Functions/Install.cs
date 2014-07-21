﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SevenZip;

namespace LoLToolsX
{
    class InstallUI
    {
        public static void GameUI(string installPath)
        {
            if (Path.GetExtension(Variable.hudPath) == ".tga")
            {
                try
                {
                    File.Copy(Variable.hudPath, installPath + @"\Game\DATA\Menu\Textures\HUDAtlas.tga", true);
                    MessageBox.Show("UI安裝成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("UI安裝成功...");
                }
                catch (Exception e)
                {
                    MessageBox.Show("UI安裝失敗\r\n錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI安裝失敗...", Logger.LogType.Error);
                    Logger.log(e);
                }

            }
            else
            {
                MessageBox.Show("請選擇 '.tga' 檔案", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("HUDAtlas.tga 未選擇 : 安裝失敗", Logger.LogType.Error);
            }
        }
    }
    class InstallSkin
    {
        public static void Skin(string installpath,string zipPath,string zipName)
        {
            SevenZipExtractor.SetLibraryPath(Application.StartupPath + @"\7z.dll");

            StreamReader sr = new StreamReader(installpath + @"\Game\ClientZips.txt");
            string temp = sr.ReadToEnd().ToString();
            sr.Close();

            if (!temp.Contains(zipName))
            {
                try
                {
                    string extractPath = installpath;
                    string parentPath = Directory.GetParent(extractPath).ToString();
                    SevenZipExtractor sze = new SevenZipExtractor(zipPath);
                    sze.ExtractArchive(parentPath);
                    FileStream fs = new FileStream(extractPath + @"\Game\ClientZips.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(zipName);
                    sw.Close();
                    fs.Close();

                    Variable.InstallSkinDone = true;
                    Logger.log("SKIN安裝成功!", Logger.LogType.Info);
                    MessageBox.Show("SKIN安裝成功!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SKIN安裝失敗 錯誤信息如下:\r\n" + ex.ToString());
                    return;
                }
            }
            else
            {
                MessageBox.Show("你所選擇的SKIN已安裝");
            }
        }
    }
}
