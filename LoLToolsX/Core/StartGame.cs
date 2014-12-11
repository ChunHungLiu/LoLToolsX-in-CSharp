using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX.Core
{
    /// <summary>
    /// 遊戲啟動
    /// </summary>
    class StartGame
    {
        NotifyIcon nIcon = new NotifyIcon();
        bool garenaStarted = false;

        //建立啟動完成事件
        private delegate void StartGameHandler();
        private event StartGameHandler StartGameFinish;

        LoLToolsX.TwTools twTools;
        LoLToolsX.NaTools naTools;

        public StartGame(TwTools _twTools, NaTools _naTools)
        {
            nIcon.DoubleClick += new EventHandler(ni_DoubleClick);
            StartGameFinish += new StartGameHandler(StartGame_StartGameFinish);
            this.twTools = _twTools;
            this.naTools = _naTools;
        }

        public void StartGarena()
        {
            try
            {
                if (My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\im", "Path", null).ToString() != null)
                {
                    string ggcPath = My.Computer.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Garena\im", "Path", null).ToString();
                    if (!String.IsNullOrEmpty(ggcPath))
                    {
                        Process.Start(ggcPath + @"\GarenaMessenger.exe");
                        Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                        Logger.log(ggcPath + @"\GarenaMessenger.exe", Logger.LogType.Info);
                        garenaStarted = true;
                        StartGameFinish();
                    }
                    else
                    {
                        garenaStarted = false;
                    }
                }
            }
            catch
            {
                garenaStarted = false;
            }

            //如果 Garena 開啟失敗
            if (!garenaStarted)
            {
                //在 LoL 目錄向上尋找Garena路徑
                string p1 = Directory.GetParent(Variable.installPath).ToString();
                string p2 = Directory.GetParent(p1).ToString();
                string p3 = Directory.GetParent(p2).ToString();
                try
                {
                    Process.Start(p3 + @"\LoLTWLauncher.exe");
                    garenaStarted = true;
                    StartGameFinish();
                }
                catch
                {
                    garenaStarted = false;
                }
            }
            if (!garenaStarted)
            {
                if (Variable.curClient == "台服")
                {
                    if (this.twTools != null)
                        twTools.Show();
                }
                else
                {
                    if (this.naTools != null)
                        twTools.Show();
                }
                MessageBox.Show("遊戲啟動失敗!");
            }
        }

        public void StartRiotL()
        {
            try
            {
                Process.Start(Variable.installPath + @"\lol.exe");
                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                Logger.log(Variable.installPath + @"\lol.exe", Logger.LogType.Info);
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
                Process.Start(Variable.installPath + @"\lol.launcher.exe");
                Logger.log("遊戲啟動成功!", Logger.LogType.Info);
                Logger.log(Variable.installPath + @"\lol.launcher.exe", Logger.LogType.Info);
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
            nIcon.Icon = icon;
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(3000, "", "遊戲啟動成功!", ToolTipIcon.None);

            if (Variable.curClient == "台服")
            {
                twTools.Dispose();
            }
            else
            {
                NaTools nt = new NaTools();
                nt.Dispose();
            }
        }

        private void ni_DoubleClick(object sender, EventArgs e)
        {
            if (Variable.curClient == "台服")
            {
                TwTools tt = new TwTools();
                tt.Show();
                nIcon.Visible = false;
            }
            else
            {
                NaTools nt = new NaTools();
                nt.Show();
                nIcon.Visible = false;
            }
        }
    }
}
