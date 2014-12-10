using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace LoLToolsX.Core
{
    /// <summary>
    /// 遊戲啟動
    /// </summary>
    class StartGame
    {
        NotifyIcon ni = new NotifyIcon();
        string installPath;
        bool g_started = false;

        //建立啟動完成事件
        private delegate void StartGameHandler();
        private event StartGameHandler StartGameFinish;

        public StartGame(string installPath)
        {
            ni.DoubleClick += new EventHandler(ni_DoubleClick);
            StartGameFinish += new StartGameHandler(StartGame_StartGameFinish);
           this.installPath = installPath;
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
                            g_started = true;
                            StartGameFinish();
                        }
                        else
                        {
                            g_started = false;
                        }
                        }
                }
            }
            catch
            {
                g_started = false;
            }

            //如果開啟失敗
            if (!g_started)
            {
                //向上尋找Garena路徑
                string p1 = Directory.GetParent(installPath).ToString();
                string p2 = Directory.GetParent(p1).ToString();
                string p3 = Directory.GetParent(p2).ToString();
                try
                {
                    Process.Start(p3 + @"\LoLTWLauncher.exe");
                }
                catch
                {
                    g_started = false;
                }
            }
            if (!g_started)
            {
                if (Variable.curClient == "台服")
                {
                    TwTools tt = new TwTools();
                    tt.Show();
                }
                else
                {
                    NaTools nt = new NaTools();
                    nt.Show();
                }
                MessageBox.Show("遊戲啟動失敗!");
            }
        }

        public void StartRiotL()
        {
            try
            {
                Process.Start(installPath + @"\lol.exe");
                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                Logger.log(installPath + @"\lol.exe", Logger.LogType.Info);
                StartGameFinish();
            }
            catch (Exception e)
            {
                if (Variable.curClient == "台服")
                {
                    TwTools tt = new TwTools();
                    tt.Show();
                }
                else
                {
                    NaTools nt = new NaTools();
                    nt.Show();
                }
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("遊戲啟動失敗!" + e, Logger.LogType.Error);
            }
        }
        public void naLauncher()
        {
            try
            {
                Process.Start(installPath + @"\lol.launcher.exe");
                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                Logger.log(installPath + @"\lol.launcher.exe", Logger.LogType.Info);
                StartGameFinish();
            }
            catch (Exception e)
            {
                if (Variable.curClient == "台服")
                {
                    TwTools tt = new TwTools();
                    tt.Show();
                }
                else
                {
                    NaTools nt = new NaTools();
                    nt.Show();
                }
                MessageBox.Show("遊戲啟動失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("遊戲啟動失敗!" + e, Logger.LogType.Error);
            }
        }
        private void StartGame_StartGameFinish()
        {
            //自動最小化到工具列
            System.Drawing.Icon icon = new System.Drawing.Icon(Variable.CurrentDirectory + "\\lol.ico");
            ni.Icon = icon;
            ni.Visible = true;
            ni.ShowBalloonTip(3000,"","遊戲啟動成功!",ToolTipIcon.None);

            if (Variable.curClient == "台服")
            {
                TwTools tt = new TwTools();
                tt.Dispose();
            }
            else
            {
                NaTools nt = new NaTools();
                nt.Dispose();
            }
        }

        private void ni_DoubleClick(object sender,EventArgs e)
        {
            if (Variable.curClient == "台服")
            {
                    TwTools tt = new TwTools();
                    tt.Show();
                    ni.Visible = false;
            }
            else
            {
                NaTools nt = new NaTools();
                nt.Show();
                ni.Visible = false;
            }
        }
    }
}
