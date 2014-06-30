using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
    
{
    /// <summary>
    /// 語言切換
    /// </summary>
    class SwitchLang
    {
        string installPath;
        string cd = Directory.GetCurrentDirectory();

        public SwitchLang(string ip)
        {
            installPath = ip;
        }

        //英文
        public void EngGame()
        {
            try
            {
                File.Copy(cd + @"\files\lang\eng\game\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                File.Copy(cd + @"\files\lang\eng\game\fontconfig_zh_TW.txt", installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                File.Copy(cd + @"\files\lang\eng\game\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
                MessageBox.Show("遊戲語言切換完成: 英文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("遊戲語言切換完成: 英文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " +e , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }
        public void EngLobby()
        {
            try
            { 
             File.Copy(cd + @"\files\lang\eng\lobby\locale.properties", installPath + @"\Air\locale.properties",true);
             File.Copy(cd + @"\files\lang\eng\lobby\fonts.swf", installPath + @"\Air\css\fonts.swf",true);
             MessageBox.Show("大廳語言切換完成: 英文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
             Logger.log("大廳語言切換完成: 英文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }

        }


        //中文
        public void ChinGame()
        {
            try
            {
                File.Copy(cd + @"\files\lang\cht\game\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                File.Copy(cd + @"\files\lang\cht\game\fontconfig_zh_TW.txt", installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                File.Copy(cd + @"\files\lang\cht\game\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
                MessageBox.Show("遊戲語言切換完成: 中文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("遊戲語言切換完成: 中文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }
        public void ChinLobby()
        {
            try
            {
                File.Copy(cd + @"\files\lang\cht\lobby\locale.properties", installPath + @"\Air\locale.properties", true);
                File.Copy(cd + @"\files\lang\cht\lobby\fonts.swf", installPath + @"\Air\css\fonts.swf",true);
                File.Copy(cd + @"\files\lang\cht\lobby\fonts_zh_TW.swf", installPath + @"\Air\css\fonts_zh_TW.swf",true);
                MessageBox.Show("大廳語言切換完成: 中文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("大廳語言切換完成: 中文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }

        }

    }
}
