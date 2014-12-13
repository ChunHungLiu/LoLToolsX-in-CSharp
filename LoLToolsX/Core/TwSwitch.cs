using System;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX.Core
{

    /// <summary>
    /// 語言切換
    /// </summary>
    static class LangEdit
    {
        public static void EngGame(string twInstallPath)
        {
            CFGFile localeFile = new CFGFile(twInstallPath + @"\Game\DATA\CFG\Local.properties");
            try
            {
                localeFile.SetValue("General", "LanguageLocaleRegion", "en_US");
                File.Copy(Variable.CurrentDirectory + @"\files\lang\eng\game\FontTypes.xml", twInstallPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", true);
                File.Copy(twInstallPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", twInstallPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                GameLangEdit frmGLE = new GameLangEdit(Variable.installPath);
                frmGLE.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }

        public static void EngLobby(string twInstallPath)
        {
            try
            {
                File.WriteAllText(twInstallPath + "\\Air\\locale.properties", "locale=en_US",System.Text.Encoding.UTF8);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\eng\lobby\fonts.swf", twInstallPath + @"\Air\css\fonts.swf", true);
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
        public static void ChinGame(string twInstallPath)
        {
            CFGFile localeFile = new CFGFile(twInstallPath + @"\Game\DATA\CFG\Local.properties");
            try
            {
                localeFile.SetValue("General", "LanguageLocaleRegion", "zh_TW");
                File.Copy(Variable.CurrentDirectory + @"\files\lang\cht\game\FontTypes.xml", twInstallPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\cht\game\GamePermanent.cfg", twInstallPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                MessageBox.Show("遊戲語言切換完成: 中文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }

        public static void ChinLobby(string twInstallPath)
        {
            try
            {
                File.WriteAllText(twInstallPath + "\\Air\\locale.properties", "locale=zh_TW", System.Text.Encoding.UTF8);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\cht\lobby\fonts.swf", twInstallPath + @"\Air\css\fonts.swf", true);
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

        /*
        //韓文
        public void KRGame()
        {
            try
            {
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\game\GamePermanent.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\game\GamePermanent_zh_TW.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", true);
                //File.Copy(cd + @"\files\lang\kr\game\GamePermanent_ko_KR.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_ko_KR.cfg", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\game\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\game\fontconfig_zh_TW.txt", installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\game\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
                MessageBox.Show("遊戲語言切換完成: 韓文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("遊戲語言切換完成: 韓文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }
        public void KRLobby()
        {
            try
            {
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\lobby\locale.properties", installPath + @"\Air\locale.properties", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\lobby\fonts.swf", installPath + @"\Air\css\fonts.swf", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\lobby\fonts_zh_TW.swf", installPath + @"\Air\css\fonts_zh_TW.swf", true);
                File.Copy(Variable.CurrentDirectory + @"\files\lang\kr\lobby\fonts_ko_KR.swf", installPath + @"\Air\css\fonts_ko_KR.swf", true);
                MessageBox.Show("大廳語言切換完成: 韓文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("大廳語言切換完成: 韓文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }
        */
    }


    /// <summary>
    /// 伺服器切換
    /// </summary>
    class SwitchServer
    {

        public static void SwitchServerLoc(string installPath, string targetLoc)
        {
            string propPath = installPath + @"\Air\lol.properties";
            string localProp = Variable.CurrentDirectory + @"\files\server_prop\" + targetLoc;
            FileInfo fi = new FileInfo(localProp);
            try
            {
                fi.CopyTo(propPath, true);
                if (Variable.editpropMessageBox)
                {
                    MessageBox.Show("伺服器切換成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("伺服器切換成功: " + targetLoc, Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                if (Variable.editpropMessageBox)
                {
                    MessageBox.Show("伺服器切換失敗 \n\r 錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("伺服器切換失敗", Logger.LogType.Error);
                    Logger.log(e, Logger.LogType.Error);
                }
            }
        }

        public static void SwitchServerLocNa(string installPath, string targetLoc)
        {
            string propPath = installPath + @"\lol.properties";
            string localProp = Variable.CurrentDirectory + @"\files\server_prop\" + targetLoc;
            FileInfo fi = new FileInfo(localProp);
            try
            {
                fi.CopyTo(propPath, true);
                if (Variable.editpropMessageBox)
                {
                    MessageBox.Show("伺服器切換成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("伺服器切換成功: " + targetLoc, Logger.LogType.Info);
                }
            }
            catch (Exception e)
            {
                if (Variable.editpropMessageBox)
                {
                    MessageBox.Show("伺服器切換失敗 \n\r 錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("伺服器切換失敗", Logger.LogType.Error);
                    Logger.log(e, Logger.LogType.Error);
                }
            }
        }

        public static void localEdit(string installPath, string locale)
        {
            string localePath = installPath + @"\locale.properties";
            FileStream fs = new FileStream(localePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write("locale=" + locale);
                MessageBox.Show("伺服器切換成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("伺服器切換成功", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("伺服器切換失敗 \n\r 錯誤訊息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("伺服器切換失敗", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
            finally
            {
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }
    }


    /// <summary>
    /// 語音切換
    /// </summary>
    class SwitchSound
    {
        TwTools tt = new TwTools();

        //大廳語音切換
        public void SwitchLobby(string lobbySoundPath)
        {
            try
            {
                Variable.switchingSound = true;


                //把 Chanpions 轉做 champions
                if (Path.GetDirectoryName(lobbySoundPath) == "Champions")
                    Directory.Move(lobbySoundPath, Directory.GetParent(lobbySoundPath) + "\\champions");

                My.Computer.FileSystem.CopyDirectory(lobbySoundPath, Variable.installPath + @"\Air\assets\sounds\zh_TW\champions", true);
                My.Computer.FileSystem.CopyDirectory(lobbySoundPath, Variable.installPath + @"\Air\assets\sounds\en_US\champions", true);

                #region Old Method
                /*
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\en_US\champions"), true);
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\zh_TW\champions"), true);
                 * */
                #endregion

                MessageBox.Show("安裝完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("大廳語音安裝成功!", Logger.LogType.Info);
                Variable.switchingSound = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("安裝失敗\r\n錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語音切換: 大廳語音安裝失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
                Variable.switchingSound = false;
            }
        }
        public void SwitchGame(string gameSoundPath)
        {
            Variable.switchingSound = true;

            #region Prevent Game Broken
            if (Directory.Exists(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_CN"))
            {
                Directory.Delete(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_CN",true);
            }
            if (Directory.Exists(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\ko_KR"))
            {
                Directory.Delete(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\ko_KR", true);
            }
            if (Directory.Exists(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW"))
            {
                Directory.Delete(Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
            }
            #endregion

            try
            {
                My.Computer.FileSystem.CopyDirectory(gameSoundPath, Variable.installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
            }
            catch { Variable.switchingSound = false; MessageBox.Show("語音切換失敗", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            /*

            #region zh_TW
            if (Directory.Exists(soundPath + @"\zh_TW"))
            {
                try
                {
                    My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_TW", installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
                }
                catch { MessageBox.Show("語音切換失敗", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            #endregion

            #region zh_CN
            if (Directory.Exists(soundPath + @"\zh_CN"))
            {
                if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW"))
                {
                    Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW");
                }
                try
                {
                    My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_CN", installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
                }
                catch { }
            }
            #endregion
            */
            Variable.switchingSound = false;
            MessageBox.Show("安裝完成!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        /*
        public void FullSwitch()
        {

            string[] fsbFile = { "\\VOBank_zh_TW.fsb", 
                                       "\\VOBank_zh_CN.fsb", 
                                       "\\VOBank_en_US.fsb", 
                                       "\\VOBank_ko_KR.fsb", 
                                       };
            string[] fevFile = {
                                     "\\LoL_Audio_zh_TW.fev", 
                                       "\\LoL_Audio_zh_CN.fev", 
                                       "\\LoL_Audio_en_US.fev", 
                                       "\\LoL_Audio_ko_KR.fev" 
                                   };

            #region FSB
            foreach (string s in fsbFile)
            {
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s, installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_TW.fsb", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s, installPath + @"\Game\DATA\Sounds\FMOD\VOBank_zh_CN.fsb", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s, installPath + @"\Game\DATA\Sounds\FMOD\VOBank_en_US.fsb", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s, installPath + @"\Game\DATA\Sounds\FMOD\VOBank_ko_KR.fsb", true);
                }
                catch
                { }
            }

            #endregion

            #region FEV

            foreach (string s2 in fevFile)
            {
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s2, installPath + @"\Game\DATA\Sounds\FMOD\LoL_Audio_zh_TW.fev", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s2, installPath + @"\Game\DATA\Sounds\FMOD\LoL_Audio_zh_CN.fev", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s2, installPath + @"\Game\DATA\Sounds\FMOD\LoL_Audio_ko_KR.fev", true);
                }
                catch
                { }
                try
                {
                    Variable.switchingSound = true;
                    File.Copy(soundPath + s2, installPath + @"\Game\DATA\Sounds\FMOD\LoL_Audio_en_US.fev", true);
                }
                catch
                { }
            }
            #endregion

            QuickSwitch();
        }
         * */
    }
}
