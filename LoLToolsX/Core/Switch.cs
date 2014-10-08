using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace LoLToolsX.Core
{

    /// <summary>
    /// 語言切換
    /// </summary>
    class SwitchLang
    {
        string installPath;
        string cd = Application.StartupPath;

        public SwitchLang(string _installPath)
        {
            installPath = _installPath;
        }

        //英文
        public void EngGame()
        {
            try
            {
                File.Copy(cd + @"\files\lang\eng\game\FontTypes.xml", installPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", true);
                File.Copy(cd + @"\files\lang\eng\game\GamePermanent.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                File.Copy(cd + @"\files\lang\eng\game\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                File.Copy(cd + @"\files\lang\eng\game\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
                MessageBox.Show("遊戲語言切換完成: 英文", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.log("遊戲語言切換完成: 英文", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("語言切換失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語言切換失敗!", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Error);
            }
        }
        public void EngLobby(int type)
        {
            string targetPath = "";

            //1是台服 2是美服
            if (type == 1)
            {
                targetPath = installPath + @"\Air";
            }
            else
            {
                targetPath = installPath;
            }


            try
            {
                File.Copy(cd + @"\files\lang\eng\lobby\locale.properties", targetPath + @"\locale.properties", true);
                File.Copy(cd + @"\files\lang\eng\lobby\fonts.swf", targetPath + @"\css\fonts.swf", true);
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
                File.Copy(cd + @"\files\lang\cht\game\FontTypes.xml", installPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", true);
                File.Copy(cd + @"\files\lang\cht\game\GamePermanent.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                File.Copy(cd + @"\files\lang\cht\game\GamePermanent_zh_TW.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", true);
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
        public void ChinLobby(int type)
        {
            string targetPath = "";

            //1是台服 2是美服
            if (type == 1)
            {
                targetPath = installPath + @"\Air";
            }
            else
            {
                targetPath = installPath;
            }

            try
            {
                File.Copy(cd + @"\files\lang\cht\lobby\locale.properties", targetPath + @"\locale.properties" , true);
                File.Copy(cd + @"\files\lang\cht\lobby\fonts.swf", targetPath + @"\css\fonts.swf", true);
                File.Copy(cd + @"\files\lang\cht\lobby\fonts_zh_TW.swf", targetPath + @"\css\fonts_zh_TW.swf", true);
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

        //韓文
        public void KRGame()
        {
            try
            {
                File.Copy(cd + @"\files\lang\kr\game\GamePermanent.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                File.Copy(cd + @"\files\lang\kr\game\GamePermanent_zh_TW.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", true);
                //File.Copy(cd + @"\files\lang\kr\game\GamePermanent_ko_KR.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_ko_KR.cfg", true);
                File.Copy(cd + @"\files\lang\kr\game\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                File.Copy(cd + @"\files\lang\kr\game\fontconfig_zh_TW.txt", installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                File.Copy(cd + @"\files\lang\kr\game\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
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
                File.Copy(cd + @"\files\lang\kr\lobby\locale.properties", installPath + @"\Air\locale.properties", true);
                File.Copy(cd + @"\files\lang\kr\lobby\fonts.swf", installPath + @"\Air\css\fonts.swf", true);
                File.Copy(cd + @"\files\lang\kr\lobby\fonts_zh_TW.swf", installPath + @"\Air\css\fonts_zh_TW.swf", true);
                File.Copy(cd + @"\files\lang\kr\lobby\fonts_ko_KR.swf", installPath + @"\Air\css\fonts_ko_KR.swf", true);
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

    }


    /// <summary>
    /// 伺服器切換
    /// </summary>
    class SwitchServer
    {

        public static void SwitchServerLoc(string installPath, string targetLoc)
        {
            string propPath = installPath + @"\Air\lol.properties";
            string localProp = Application.StartupPath + @"\files\server_prop\" + targetLoc;
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
            string localProp = Application.StartupPath + @"\files\server_prop\" + targetLoc;
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
        string installPath;
        string soundPath;
        TwTools tt = new TwTools();

        public SwitchSound(string installPath, string soundPath)
        {
            this.installPath = installPath;
            this.soundPath = soundPath;
        }

        //大廳語音切換
        public void SwitchLobby()
        {
            if (!soundPath.Contains("Sound"))
            {
                MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語音切換: Sound 資料夾選擇錯誤", Logger.LogType.Error);
                return;
            }
            try
            {
                Variable.switchingSound = true;
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\en_US\champions"), true);
                foreach (string newPath in Directory.GetFiles(soundPath + @"\champions", "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(soundPath + @"\champions", installPath + @"\Air\assets\sounds\zh_TW\champions"), true);
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
        public void QuickSwitch()
        {
        	#region Prevent Jump Game
        	if (Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_CN"))
        	{
        		Directory.Delete(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_CN");
        	}
        	if (Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\ko_KR"))
        	{
        		Directory.Delete(installPath + @"\Game\DATA\Sounds\Wwise\VO\ko_KR");
        	}
        	#endregion
        	
            #region ko_KR
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\ko_KR", installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
            }
            catch { }
            /*
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\ko_KR", installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US", true);
            }
            catch { }
            */
            #endregion

            #region zh_TW
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_TW", installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
            }
            catch { }
            /*
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_TW", installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US", true);
            }
            catch { }
            */
            #endregion

            #region zh_CN
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_CN", installPath + @"\Game\DATA\Sounds\Wwise\VO\zh_TW", true);
            }
            catch { }
            /*
            if (!Directory.Exists(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US"))
            {
                Directory.CreateDirectory(installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US");
            }
            try
            {
                My.Computer.FileSystem.CopyDirectory(soundPath + @"\zh_CN", installPath + @"\Game\DATA\Sounds\Wwise\VO\en_US", true);
            }
            catch { }
            */
            #endregion

            MessageBox.Show("安裝完成!");
        }

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
    }

}
