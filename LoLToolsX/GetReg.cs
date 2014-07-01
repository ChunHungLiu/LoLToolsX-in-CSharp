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

            /*
            if (String.IsNullOrEmpty(installPath))
            {
                {
                    RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Garena\LoLTW", false);
                    if (myKey.GetValue("Path") != null)
                    {
                        String value = (String)myKey.GetValue("Path");
                        if (!String.IsNullOrEmpty(value))
                        {
                            installPath = value;
                            return installPath;
                    }

                    }
                }
            }


            if (String.IsNullOrEmpty(installPath))
            {
                {
                    RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Garena\LoLTW",false);
                    if (myKey.GetValue("Path") != null)
                    {
                        String value = (String)myKey.GetValue("Path");
                        if (!String.IsNullOrEmpty(value))
                        {
                            installPath = value;
                            return installPath;
                        }

                    }
                }
            }
             */
              return "";
        }
    }
}
