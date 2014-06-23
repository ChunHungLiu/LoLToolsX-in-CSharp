using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace LoLToolsX
{
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
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public void StartRiotL()
        {
            try
            {
                Process.Start(installPath + @"\lol.exe");
            }
            catch (Exception e)
            {
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
