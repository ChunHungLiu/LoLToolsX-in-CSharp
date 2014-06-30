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
            if (!String.IsNullOrEmpty(CFGFile.GetValue("Path", "TwPath")))
            {
                installPath = CFGFile.GetValue("Path", "TwPath");
                return installPath;
            }

            if (String.IsNullOrEmpty(installPath))
            {
                {
                    RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Garena\LoLTW", false);
                    String value = (String)myKey.GetValue("Path");

                    if (!String.IsNullOrEmpty(value))
                    {
                        installPath = value;
                        return installPath;
                    }
                }
            }


            if (String.IsNullOrEmpty(installPath))
            {
                {
                    RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Garena\LoLTW", false);
                    String value = (String)myKey.GetValue("Path");

                    if (!String.IsNullOrEmpty(value))
                    {
                        installPath = value;
                        return installPath;
                    }
                }
            }
              return "";
        }
    }
}
