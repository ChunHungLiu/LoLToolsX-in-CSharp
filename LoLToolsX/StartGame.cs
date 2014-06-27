using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace LoLToolsX
{
    /// <summary>
    /// 遊戲啟動
    /// </summary>
    class StartGame
    {
        string installPath;

        public StartGame(string ip)
        {
           installPath = ip;
        }

        public void StartGarena()
        {
            try
            {
                string ggcPath = "";
                if (String.IsNullOrEmpty(ggcPath))
                {
                    RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Garena\im", false);
                    ggcPath = (String)myKey.GetValue("Path");

                    if (!String.IsNullOrEmpty(ggcPath))
                    {
                        Process.Start(ggcPath + @"\GarenaMessenger.exe");
                        Logger.log("遊戲啟動成功!" , Logger.LogType.Info);
                        Logger.log(ggcPath + @"\GarenaMessenger.exe", Logger.LogType.Info);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("遊戲啟動失敗!" + e, Logger.LogType.Error);
            }

            
        }

        public void StartRiotL()
        {
            try
            {
                Process.Start(installPath + @"\lol.exe");
                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                Logger.log(installPath + @"\lol.exe", Logger.LogType.Info);
            }
            catch (Exception e)
            {
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("遊戲啟動失敗!" + e, Logger.LogType.Error);
            }
        }
    }
}
