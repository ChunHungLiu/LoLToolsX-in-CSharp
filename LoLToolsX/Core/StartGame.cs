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
        bool gameStarted = false;

        //建立啟動完成事件
        private delegate void StartGameHandler();
        private event StartGameHandler StartGameFinish;

        LoLToolsX.TwTools twTools;
        LoLToolsX.NaTools naTools;

        public StartGame(TwTools _twTools, NaTools _naTools)
        {
            //nIcon.DoubleClick += new EventHandler(ni_DoubleClick);
            nIcon.Click += new EventHandler(ni_Click);

            StartGameFinish += new StartGameHandler(StartGame_StartGameFinish);
            this.twTools = _twTools;
            this.naTools = _naTools;
        }

        public void StartGarena()
        {
            if (!StartupCheck())
            {
                if (MessageBox.Show("LoL 已開啟，你確定要繼續開啟 LoL 嗎?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }
            }

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
                        gameStarted = true;
                        StartGameFinish();
                    }
                    else
                    {
                        gameStarted = false;
                    }
                }
            }
            catch
            {
                gameStarted = false;
            }

            //如果 Garena 開啟失敗
            if (!gameStarted)
            {
                //在 LoL 目錄向上尋找Garena路徑
                string p1 = Directory.GetParent(Variable.installPath).ToString();
                string p2 = Directory.GetParent(p1).ToString();
                string p3 = Directory.GetParent(p2).ToString();
                try
                {
                    Process.Start(p3 + @"\LoLTWLauncher.exe");
                    gameStarted = true;
                    StartGameFinish();
                }
                catch
                {
                    gameStarted = false;
                }
            }
            if (!gameStarted)
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
            System.Drawing.Icon _icon = Properties.Resources.lol;
            nIcon.Icon = _icon;
            nIcon.Visible = true;
            nIcon.ShowBalloonTip(3000, "", "遊戲啟動成功!", ToolTipIcon.None);

            if (Variable.curClient == "台服")
            {
                this.twTools.Dispose();
            }
            else
            {
                this.naTools.Dispose();
            }
        }

        private void ni_Click(object sender, EventArgs e)
        {
            if (Variable.curClient == "台服")
            {
                if (!this.twTools.IsDisposed)
                {
                    this.twTools.Show();
                }
                else
                {
                    new TwTools().Show();
                }

                nIcon.Visible = false;
            }
            else
            {
                if (!this.naTools.IsDisposed)
                {
                    this.naTools.Show();
                }
                else
                {
                    new NaTools().Show();
                }

                nIcon.Visible = false;
            }
        }

        bool StartupCheck()
        {
            Process[] procLol = Process.GetProcessesByName("lol");
            Process[] procClient = Process.GetProcessesByName("LoLClient");
            Process[] procGarena = Process.GetProcessesByName("GarenaMessenger");
            Process[] procLoLGame = Process.GetProcessesByName("League of Legends");

            if (procLol.Length >= 1 || procClient.Length >= 1 || procGarena.Length >= 1 || procLoLGame.Length >= 1)
                return false;
            else
                return true;
        }
    }
}
