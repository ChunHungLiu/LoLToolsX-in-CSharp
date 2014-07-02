using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
                    File.Copy(Variable.hudPath, installPath + @"\Game\DATA\Menu\Textures\HUDAtlas.tga",true);
                    MessageBox.Show("UI安裝成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("UI安裝成功...");
                }
                catch (Exception e)
                {
                    MessageBox.Show("UI安裝失敗\r\n錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI安裝失敗...",Logger.LogType.Error);
                    Logger.log(e);
                }

            }
            else
            {
                MessageBox.Show("請選擇 '.tga' 檔案", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("HUDAtlas.tga 未選擇 : 安裝失敗",Logger.LogType.Error);
            }
        }
    }
}
