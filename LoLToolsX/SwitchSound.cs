using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
{

    /// <summary>
    /// 語音切換
    /// </summary>

    class SwitchSound
    {
        string installPath;
        string soundPath;
        TwTools tt = new TwTools();

        public SwitchSound(string installPath,string soundPath)
        {
            this.installPath = installPath;
            this.soundPath = soundPath;
        }

        //大廳語音切換
        public void SwitchLobby()
        {
            if (!soundPath.Contains("Sound"))
            {
                MessageBox.Show("請選擇正確的Sound資料夾","錯誤",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            try
            {
                tt.pbLobby.Value = 0;
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\en_US\champions"), true);
                tt.pbLobby.Value = 50;
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\zh_TW\champions"), true);
                tt.pbLobby.Value = 100;
                MessageBox.Show("安裝完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tt.pbLobby.Value = 0;

            }
            catch (Exception e)
            {
                tt.pbLobby.Value = 0;
                MessageBox.Show("安裝失敗\r\n錯誤信息: " +e , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //遊戲語音切換
        public void SwitchGame()
        {
            if (!soundPath.Contains("Sound"))
            {
                MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //KR
            try
            {
                tt.pbGame.Value = 0;
                File.Copy(soundPath + @"\VOBank_ko_KR.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_ko_KR.fsb", true);
                tt.pbGame.Value = 35;
                File.Copy(soundPath + @"\VOBank_ko_KR.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_TW.fsb", true);
                File.Copy(soundPath + @"\VOBank_ko_KR.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_CN.fsb", true);
                tt.pbGame.Value = 65;
                File.Copy(soundPath + @"\VOBank_ko_KR.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_en_US.fsb", true);
                tt.pbGame.Value = 100;
                MessageBox.Show("安裝完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tt.pbGame.Value = 0;
            }
                /*
            catch (System.IO.FileNotFoundException)
            {
                tt.pbGame.Value = 0;
                MessageBox.Show("安裝失敗\r\n找不到語音檔案 " , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                 */
            catch 
            {
               
            }


            try
            {
                tt.pbGame.Value = 0;
                File.Copy(soundPath + @"\VOBank_zh_TW.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_ko_KR.fsb", true);
                tt.pbGame.Value = 35;
                File.Copy(soundPath + @"\VOBank_zh_TW.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_TW.fsb", true);
                File.Copy(soundPath + @"\VOBank_zh_TW.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_CN.fsb", true);
                tt.pbGame.Value = 65;
                File.Copy(soundPath + @"\VOBank_zh_TW.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_en_US.fsb", true);
                tt.pbGame.Value = 100;
                MessageBox.Show("安裝完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tt.pbGame.Value = 0;
            }
            /*
            catch (System.IO.FileNotFoundException)
            {
                tt.pbGame.Value = 0;
                MessageBox.Show("安裝失敗\r\n找不到語音檔案 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             */
            catch 
            {
               
            }

            try
            {
                tt.pbGame.Value = 0;
                File.Copy(soundPath + @"\VOBank_zh_CN.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_ko_KR.fsb", true);
                tt.pbGame.Value = 35;
                File.Copy(soundPath + @"\VOBank_zh_CN.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_TW.fsb", true);
                File.Copy(soundPath + @"\VOBank_zh_CN.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_CN.fsb", true);
                tt.pbGame.Value = 65;
                File.Copy(soundPath + @"\VOBank_zh_CN.fsb", installPath + @"\Game\DATA\Sounds\FMOD\VOBank_en_US.fsb", true);
                tt.pbGame.Value = 100;
                MessageBox.Show("安裝完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tt.pbGame.Value = 0;
            }
                /*
            catch (System.IO.FileNotFoundException)
            {
                tt.pbGame.Value = 0;
                MessageBox.Show("安裝失敗\r\n找不到語音檔案 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                 */
            catch
            {
               
            }

        }

    }
}
