using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SevenZip;

namespace LoLToolsX.Core
{
    class InstallUI
    {
        public static void GameUI(string installPath)
        {
            //檢查是否正確的UI檔案
            if (Path.GetExtension(Variable.hudPath) == ".dds")
            {
                if (MessageBox.Show("此UI檔案為舊版格式，要確認轉換成新格式並安裝嗎?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string originalPath = Variable.hudPath;
                    try
                    {
                        File.Copy(originalPath, installPath + @"\Game\DATA\Menu\Textures\HUDAtlas.dds", true);
                        MessageBox.Show("UI安裝成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Logger.log("UI安裝成功...(dds)");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("UI安裝失敗\r\n錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.log("UI安裝失敗...(dds)", Logger.LogType.Error);
                        Logger.log(e);
                    }
                    return;
                }
                else
                { return; }
            }
            if (Path.GetExtension(Variable.hudPath) == ".dds")
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
                return;
            }
            else
            {
                MessageBox.Show("請選擇 '.tga' 或 '.dds' 檔案", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("檔案未選擇 : 安裝失敗", Logger.LogType.Error);
            }
        }

        public static void NaGameUI(string installPath)
        {
            //檢查是否正確的UI檔案
            if (Path.GetExtension(Variable.hudPath) == ".tga")
            {
                try
                {
                    //複製檔案到每個資料夾
                    string dir = Variable.n_installPath + @"\RADS\projects\lol_game_client\filearchives";
                    string[] folder = Directory.GetDirectories(dir);
                    foreach (string f in folder)
                    {
                        if (!Directory.Exists(f + "\\DATA"))
                        {
                            Directory.CreateDirectory(f + "\\DATA");
                        }
                        if (!Directory.Exists(f + "\\DATA\\Menu"))
                        {
                            Directory.CreateDirectory(f + "\\DATA\\Menu");
                        }
                        if (!Directory.Exists(f + "\\DATA\\Menu\\Textures"))
                        {
                            Directory.CreateDirectory(f + "\\DATA\\Menu\\Textures");
                        }
                        File.Copy(Variable.hudPath, f + "\\DATA\\Menu\\Textures\\HUDAtlas.tga",true);
                    }
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
            SevenZipExtractor.SetLibraryPath(Variable.CurrentDirectory + @"\7z.dll");

            StreamReader sr = new StreamReader(installpath + @"\Game\ClientZips.txt");
            string temp = sr.ReadToEnd().ToString();
            sr.Close();

            if (!temp.Contains(zipName))
            {
                try
                {
                    //解壓
                    string extractPath = installpath;
                    string parentPath = Directory.GetParent(extractPath).ToString();
                    SevenZipExtractor sze = new SevenZipExtractor(zipPath);
                    sze.ExtractArchive(parentPath);
                    //寫入 ClientZips.txt
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
                finally
                {
                    GC.Collect();
                }
            }
            else
            {
                MessageBox.Show("你所選擇的SKIN已安裝");
            }
        }
    }
}
