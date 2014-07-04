using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace LoLToolsX
{
    class GetReg
    {

        /// <summary>
        /// 從登錄檔取得LoL路徑
        /// </summary>

        public static string installPath = "";  //LoL安裝路徑

        public string TwPath(string iniPath)   //取得台服LoL路徑
        {                
            CFGFile CFGFile = new CFGFile(iniPath);

            //檢查 config.ini
            if (!String.IsNullOrEmpty(CFGFile.GetValue("LoLPath", "TwPath")))
            {
                installPath = CFGFile.GetValue("LoLPath", "TwPath");
                return installPath;
            }

            if (My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\LoLTW", "Path",null) != null)
            {
                string value = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\LoLTW", "Path",null).ToString();
                if (value.Contains("LoLTW"))
                {
                    installPath = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\LoLTW", "Path", null).ToString();
                    return installPath;
                }
            }

            if (My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Garena\LoLTW", "Path", null) != null)
            {
                string value = My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Garena\LoLTW", "Path", null).ToString();
                if (value.Contains("LoLTW"))
                {
                    installPath = My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Garena\LoLTW", "Path", null).ToString();
                    return installPath;
                }
            }
              return "";
        }

        public string NaPath(string iniPath)
        {

            CFGFile CFGFile = new CFGFile(iniPath);

            //檢查 config.ini
            if (!String.IsNullOrEmpty(CFGFile.GetValue("LoLPath", "NaPath")))
            {
                installPath = CFGFile.GetValue("LoLPath", "NaPath");
                return installPath;
            }

            if (My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Riot Games\League of Legends", "Path", null) != null)
            {
                string value = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Riot Games\League of Legends", "Path", null).ToString();
                if (value.Contains("League of Legends"))
                {
                    installPath = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Riot Games\League of Legends", "Path", null).ToString();
                    return installPath;
                }
            }

            if (My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Riot Games\League of Legends", "Path", null) != null)
            {
                string value = My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Riot Games\League of Legends", "Path", null).ToString();
                if (value.Contains("League of Legends"))
                {
                    installPath = My.Computer.Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Riot Games\League of Legends", "Path", null).ToString();
                    return installPath;
                }
            }
            return "";
        }
    }
}
