using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace LoLToolsX
{
    /// <summary>
    /// 遊戲啟動
    /// </summary>
    class StartGame
    {
        string installPath;
        bool g_started = false;
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
                    if (My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\im", "Path", null).ToString() != null)
                    {
                        ggcPath = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\im", "Path", null).ToString();
                            if (!String.IsNullOrEmpty(ggcPath))
                            {
                                Process.Start(ggcPath + @"\GarenaMessenger.exe");
                                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                                Logger.log(ggcPath + @"\GarenaMessenger.exe", Logger.LogType.Info);
                                MessageBox.Show("遊戲啟動成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                g_started = true;
                            }
                        }
                }
            }
            catch (Exception e)
            {
                g_started = false;
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("遊戲啟動失敗!" + e, Logger.LogType.Error);
            }

            if (!g_started)
            {
                string p1 = Directory.GetParent(installPath).ToString();
                string p2 = Directory.GetParent(p1).ToString();
                string p3 = Directory.GetParent(p2).ToString();
                Process.Start(p3 + @"\LoLTWLauncher.exe");
            }
        }

        public void StartRiotL()
        {
            try
            {
                Process.Start(installPath + @"\lol.exe");
                MessageBox.Show("遊戲啟動成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
