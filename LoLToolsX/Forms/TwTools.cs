using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using SevenZip;
using LoLToolsX.Core;
using LoLToolsX.Core.Update;

namespace LoLToolsX
{

    /// <summary>
    /// 台服工具
    /// </summary>


    public partial class TwTools : Form
    {     
        public static string installPath = "";

        public TwTools()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)     //Form1_Load
        {
            if (!Variable.allowBakRes)
            {
                button24.Enabled = false;
                button25.Enabled = false;
            }
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            //寫入目前LoLToolsX版本到Log
            Logger.log("LoLToolsX版本: " + Application.ProductVersion, Logger.LogType.Info);

            Logger.log("目前客戶端 : 台服", Logger.LogType.Info);
            //載入LoLToolsX Logo
            PictureBox1.ImageLocation = Application.StartupPath + @"\logo.png";
            Logger.log("LoLToolsX Logo載入成功!", Logger.LogType.Info);

            if (Variable.forceSelectPath)
            {
                goto SelectPath;
            }

            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.TwPath(Application.StartupPath + @"\config.ini");
            Logger.log("LoL目錄取得成功! " + installPath, Logger.LogType.Info);

SelectPath:
            CFGFile CFGFile = new CFGFile(Application.StartupPath + @"\config.ini");

            //檢查路徑是否存有 LoLTW 字串
            if (!installPath.Contains("LoLTW"))
            {
                if (MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    Logger.log("LoL手動選擇目錄! " , Logger.LogType.Info);
                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                        Logger.log("LoL目錄檢查成功! " + installPath, Logger.LogType.Info);
                        CFGFile.SetValue("LoLPath", "TwPath", "\"" + installPath + "\"");
                        CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.log("LoL目錄檢查失敗 " , Logger.LogType.Error);
                        Logger.log("強制關閉程式... " , Logger.LogType.Info);
                        Application.Exit();
                    }
                }
                else
                {
                    Logger.log("LoL目錄選擇取消 " , Logger.LogType.Error);
                    Logger.log("關閉程式... " + installPath, Logger.LogType.Info);
                    Application.Exit();
                }
            }
            else
            {
                CFGFile.SetValue("LoLPath", "TwPath", "\"" + installPath + "\"");
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                PathLabel.Text = installPath;
                Logger.log("LoL目錄檢查成功! " , Logger.LogType.Info);
                Logger.log("LoL目錄寫入成功! " + installPath, Logger.LogType.Info);
            }

            //CFGFile = null;

            if (Variable.allowUpdate)
            {
                //CFGFile checkAutoUpdate = new CFGFile(Application.StartupPath + @"\config.ini");
                if (CFGFile.GetValue("LoLToolsX", "AutoUpdate") == "true")
                {
                    Variable.updating = true;
                    this.checkBox1.Checked = false;
                    Thread updateThread = new Thread(CheckUpdate.checkUpdate);
                    //開始檢查更新
                    updateThread.Start();
                }
                else
                {
                    this.checkBox1.Checked = true;
                }
            }
            else
            {
                button23.Enabled = false;
            }

            //檢查語言檔更新
            //Thread langUpdate = new Thread(LangUpdate.CheckLangUpdate);
            //langUpdate.Start();

            //string test = Application.StartupPath;
            //MessageBox.Show(test);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //取得目前伺服器
            CheckProp cp = new CheckProp();
            cp.CheckPropFL(installPath);
            serverLocation.Text = cp.currentLoc;

            notifyIcon1.Visible = false;
            PathLabel.Text = installPath;

            //取得版本資訊
            toolsVersion.Text = Application.ProductVersion.ToString();
            LoLVersionLabel.Text = GetLoLVer();



            //在綫統計使用人數
            try
            {
                WebBrowser1.Navigate("http://nitroxenon.com/loltoolsx/stat.html");
            }
            catch (Exception e2)
            {
                Logger.log("使用人數統計失敗", Logger.LogType.Error);
                Logger.log(e2);
            }

            //刪除物件
            cp = null;
            gr = null;


            string[] skins = File.ReadAllLines(Application.StartupPath + @"\Skin.txt");
            foreach (string s in skins)
            {
                installedSkin.Items.Add(s);
            }

            //Variable.tw_installPath = installPath;
            Variable.curClient = "台服";
        }

        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.log("開啟NitroXenon的BLOG...", Logger.LogType.Info);
            Process.Start("http://nitroxenon.com");
        }

        public static string GetLoLVer()
        {
            //取得LoL版本
            FileStream fs2 = new FileStream(installPath + @"\lol.version", FileMode.Open);
            StreamReader sr2 = new StreamReader(fs2);
            try
            {
                //string gameVer = sr.ReadLine();
                string airVer = sr2.ReadLine();
                Logger.log("Air版本: " + airVer, Logger.LogType.Info);
                return airVer;
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show("無法取得LoL版本", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("無法取得Air版本: " , Logger.LogType.Error);
                Logger.log(e);
                return "未知";
            }
            finally
            {
                //sr.Close();
                sr2.Close();
                //fs.Close();
                fs2.Close();
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolt.properties");
            serverLocation.Text = "台服";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lols.properties");
            serverLocation.Text = "SEA服";
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loloce.properties");
            serverLocation.Text = "大洋洲服";
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loln.properties");
            serverLocation.Text = "美服";
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lole.properties");
            serverLocation.Text = "EUW服";
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolp.properties");
            serverLocation.Text = "PBE服";
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "lolk.properties");
            serverLocation.Text = "韓服";
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SwitchServer.SwitchServerLoc(installPath, "loleune.properties");
            serverLocation.Text = "EUNE服";
            
        }

        private void backProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(1,1);

        }

        private void restoneProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(2,1);
        }

        private void delBakProp_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(3,1);
        }

        private void startLoL_Click(object sender, EventArgs e)
        {
            this.Opacity = 50;

            StartGame sg = new StartGame(installPath);

            if (serverLocation.Text == "台服")
            {
                    sg.StartGarena();
            }
            else if (serverLocation.Text == "SEA服")
            {
              
                    sg.StartGarena();
                    //this.Hide();
                    //this.ShowInTaskbar = false;
                    //this.notifyIcon1.Visible = true;
                    //this.notifyIcon1.ShowBalloonTip(5000, "", "遊戲啟動成功", ToolTipIcon.None);
                
            }
            else
            {
               
                    sg.StartRiotL();
                    //this.Hide();
                    //this.ShowInTaskbar = false;
                    //this.notifyIcon1.Visible = true;
                    //this.notifyIcon1.ShowBalloonTip(5000, "", "遊戲啟動成功", ToolTipIcon.None);
                
                
            }
            this.Dispose();
        }

        private void lChin_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.ChinLobby(1);

        }

        private void lEng_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.EngLobby(1);
        }

        private void gChin_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.ChinGame();
        }

        private void gEng_Click(object sender, EventArgs e)
        {
            SwitchLang sl = new SwitchLang(installPath);
            sl.EngGame();
        }

        private void BakLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(1);
        }

        private void ResLang_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(2);
        }

        private void delLangBak_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Lang(3);
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nitroxenon.com/sound-pack");
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nitroxenon.com/lol-tools-tw/lol-lobby-theme/");
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.Contains("Sound"))
                {
                    Logger.log("Sound資料夾選擇成功: " +folderBrowserDialog1.SelectedPath, Logger.LogType.Info);
                    tbPath.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Sound資料夾選擇錯誤", Logger.LogType.Error);
                }
            }
        }


        private void Button11_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPath.Text))
            {
                if (!Variable.switchingSound)
                {
                    SwitchType st = new SwitchType(installPath,tbPath.Text);
                    st.ShowDialog();
                }
                else
                {
                    MessageBox.Show("語音安裝進行中... 請稍後~");
                }
            }
            else
            {
                MessageBox.Show("請先選擇Sound資料夾", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(1);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(2);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Sound(3);
        }


        private void Button15_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(installPath,websiteIn.Text,1);
            pe.LobbyLanding();
        }

        private void chooseHUD_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Variable.hudPath = openFileDialog2.FileName;
                Logger.log("已選擇hudPath路徑: " + Variable.hudPath);
            }
            
        }

        private void installHUD_Click(object sender, EventArgs e)
        {
            InstallUI.GameUI(installPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CFGFile ini = new CFGFile(Application.StartupPath + @"\config.ini");
            if (checkBox1.Checked == true)
            ini.SetValue("LoLToolsX", "AutoUpdate", "false");
            else
            ini.SetValue("LoLToolsX", "AutoUpdate", "true");
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            if (Variable.allowUpdate)
            {
                if (!Variable.updating)
                {
                    Thread updateThread = new Thread(CheckUpdate.checkUpdate);
                    updateThread.Start();
                }
                else
                {
                    MessageBox.Show("正在檢查更新, 請稍候...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(1);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(2);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.UI(3);
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            ChatEdit ce = new ChatEdit(installPath);
            ce.Show();
        }

        private void TwTools_Click(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void checkServerStatus_Click(object sender, EventArgs e)
        {
            Label[] labels = {           this.Label001,
                                         this.Label002,
                                         this.Label003,
                                         this.Label004,
                                         this.Label005,
                                         this.Label006,
                                         this.Label007,
                                         this.Label008};

            string[] serverAry = 
            {   "lol.garena.com",
                "216.52.241.254",
                "203.117.155.133",
                "www.leagueoflegends.co.kr",
                "95.172.70.254",
                "190.93.254.13",
                "lq.oc1.lol.riotgames.com",
                "66.150.148.64"
            };

            for (int i = 0; i <= 7; i++)
            {
                //if (StatusCheck.pingCheck(serverAry[i]))
                //{
                string tmp = StatusCheck.pingCheck(serverAry[i]);
                if (tmp != "請求逾時" & tmp != "不明")
                {
                    labels[i].ForeColor = System.Drawing.Color.Green;
                }
                else if (tmp == "不明")
                {
                    labels[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    labels[i].ForeColor = System.Drawing.Color.Red;
                }
                labels[i].Text = tmp;
                Logger.log("Server Status Check:", Logger.LogType.Info);
                Logger.log(serverAry[i] + " : 正常", Logger.LogType.Info);
            }
            //else
            //{
            //labels[i].Text = "請求逾時";
            //labels[i].ForeColor = System.Drawing.Color.Red;
            // Logger.log("Server Status Check:", Logger.LogType.Info);
            //Logger.log(serverAry[i] + " : 請求逾時", Logger.LogType.Info);
            //}


        }

        private void button25_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.LoL(1);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.LoL(2);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            LobbyUI lui = new LobbyUI(installPath);
            lui.ShowDialog();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(1,1);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(2,1);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            BakRes br = new BakRes(installPath);
            br.Prop(3,1);
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            LobbyUI lui = new LobbyUI(installPath);
            lui.Show();
        }

        private void installSound_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPath.Text))
            {
                if (!Variable.switchingSound)
                {
                    SwitchSound ss = new SwitchSound(installPath, tbPath.Text);
                    ss.SwitchLobby();
                }
                else
                {
                    MessageBox.Show("語音安裝進行中... 請稍後~");
                }
            }
            else
            {
                MessageBox.Show("請先選擇Sound資料夾", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LibDownload()
        {
                WebClient wc = new WebClient();
                Wait wait = new Wait();
                try
                {
                    wait.Show();
                    wait.progressBar1.Value = 50;
                    wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/SevenZipSharp/SevenZipSharp.dll", Application.StartupPath + @"\SevenZipSharp.dll");
                    wait.progressBar1.Value = 70;
                    wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/7z/7z.dll", Application.StartupPath + @"\7z.dll");
                    wait.progressBar1.Value = 100;
                    MessageBox.Show("下載完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("SevenZipSharp.dll 下載完成!", Logger.LogType.Info);
                    wait.Close();
                }
                catch
                {
                    wait.Dispose();
                    MessageBox.Show("下載Skin安裝所需類別庫失敗, 無法進行Skin安裝。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    wc.Dispose();
                }
        }

        private void lKR_Click(object sender, EventArgs e)
        {
            SwitchLang swLang = new SwitchLang(installPath);
            swLang.KRLobby();
        }

        private void gKR_Click(object sender, EventArgs e)
        {
            SwitchLang swLang = new SwitchLang(installPath);
            swLang.KRGame();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (skinSelector.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(skinSelector.FileName))
                {
                    skinPathBox.Items.Add(skinSelector.FileName);
                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (skinPathBox.Items.Count != 0)
            {
                skinPathBox.Items.Remove(skinPathBox.SelectedItem);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\SevenZipSharp.dll") || !File.Exists(Application.StartupPath + @"\7z.dll"))
            {
                Logger.log("找不到Skin安裝所需的類別庫", Logger.LogType.Info);
                if (MessageBox.Show("找不到Skin安裝所需的類別庫, 按確定下載Skin安裝用類別庫。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Logger.log("正在下載Skin安裝所需的類別庫...", Logger.LogType.Info);
                    Thread downloadThread = new Thread(new ThreadStart(LibDownload));
                    downloadThread.Start();
                }
            }
            else
            {
                if (skinPathBox.SelectedItem != null)
                {
                    if (Path.GetExtension(skinPathBox.SelectedItem.ToString()) == ".zip")
                    {
                        try
                        {
                            string str = File.ReadAllText(Application.StartupPath + @"\Skin.txt");



                            File.WriteAllText(Application.StartupPath + @"\Skin.txt", str, Encoding.Default);

                            InstallSkin.Skin(installPath, skinPathBox.SelectedItem.ToString(), Path.GetFileName(skinPathBox.SelectedItem.ToString()));
                            FileStream fs = new FileStream(Application.StartupPath + @"\Skin.txt", FileMode.Append, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            sw.WriteLine(Path.GetFileName(skinPathBox.SelectedItem.ToString()));
                            sw.Close();
                            fs.Close();

                            if (Variable.InstallSkinDone)
                            {
                                installedSkin.Items.Add(Path.GetFileName(skinPathBox.SelectedItem.ToString()));
                                skinPathBox.Items.Remove(skinPathBox.SelectedItem);
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.log("SKIN安裝失敗: \r\n" + ex, Logger.LogType.Info);
                            MessageBox.Show("SKIN安裝失敗，錯誤訊息:\r\n" + ex);
                        }
                        finally
                        {
                            Variable.InstallSkinDone = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("請選擇正確的SKIN檔案");
                    }
                }
                else
                {
                    MessageBox.Show("請先選擇欲安裝之SKIN");
                }
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (installedSkin.SelectedItem != null)
            {
                try
                {
                    string str = File.ReadAllText(Application.StartupPath + @"\Skin.txt");
                    
                    
                    
                    File.WriteAllText(Application.StartupPath + @"\Skin.txt", str, Encoding.Default);

                    string temp = File.ReadAllText(Application.StartupPath + @"\Skin.txt");
                    string newTemp = temp.Replace(installedSkin.SelectedItem.ToString() + "\r\n" , "");
                    File.WriteAllText(Application.StartupPath + @"\Skin.txt", newTemp);

                    string temp2 = File.ReadAllText(installPath + @"\Game\ClientZips.txt");
                    string newTemp2 = temp2.Replace(installedSkin.SelectedItem.ToString() + "\r\n", "");
                    File.WriteAllText(installPath + @"\Game\ClientZips.txt", newTemp2);
                    installedSkin.Items.Remove(installedSkin.SelectedItem);

                    
                    
                    
                    
                    

                    Logger.log("SKIN刪除成功!", Logger.LogType.Info);
                    MessageBox.Show("SKIN刪除成功!");
                }
                catch (Exception ex)
                {
                    Logger.log("SKIN刪除失敗: \r\n" + ex, Logger.LogType.Info);
                    MessageBox.Show("SKIN刪除失敗! 錯誤信息:\r\n" + ex);
                }
            }
            else
                MessageBox.Show("請先選擇欲刪除的SKIN");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://forum.gamer.com.tw/C.php?bsn=17532&snA=235775&tnum=1285");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(installPath, "http://lobby.lol.tw/", 1);
            pe.LobbyLanding();
        }

        private void TwTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            LoLToolsX.Forms.CheckLangUpdate check = new Forms.CheckLangUpdate();
            check.ShowDialog();
        }

        private void tabPage11_Enter(object sender, EventArgs e)
        {

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\lolspectx.txt"))
            {
                if (MessageBox.Show("已推出新版 LoLSpectX 觀戰工具 請問要下載嗎?", "新工具發佈", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Wait wait = new Wait();
                    wait.progressBar1.Visible = false;
                    wait.label1.Visible = true;
                    wait.TopMost = true;
                    wait.Show();
                    wait.Refresh();
                    wait.Update();

                    WebClient wc = new WebClient();
                    wc.DownloadFile("https://github.com/NitroXenon/LoLSpectX/releases/download/v0.1.6-beta/LoLSpectX0.1.6-beta.zip", Application.StartupPath + "\\download\\LoLSpectX0.1.1-beta.zip");
                    SevenZipExtractor sze = new SevenZipExtractor(Application.StartupPath + "\\download\\LoLSpectX0.1.1-beta.zip");
                    sze.ExtractArchive(Application.StartupPath + "\\LoLSpectX\\");
                    wc.Dispose();
                    sze.Dispose();
                    wait.Dispose();

                    File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\lolspectx.txt").Close();

                    MessageBox.Show("下載完成! 按確定開啟 LoLSpectX 觀戰工具 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (!Directory.Exists(Application.StartupPath + @"\LoLSpectX"))
            {
                if (MessageBox.Show("已推出 LoLSpectX 觀戰工具 請問要下載嗎?", "新工具發佈", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Wait wait = new Wait();
                    wait.progressBar1.Visible = false;
                    wait.label1.Visible = true;
                    wait.TopMost = true;
                    wait.Show();
                    wait.Refresh();
                    wait.Update();

                    WebClient wc = new WebClient();
                    wc.DownloadFile("https://github.com/NitroXenon/LoLSpectX/releases/download/v0.1.6-beta/LoLSpectX0.1.6-beta.zip", Application.StartupPath + "\\download\\LoLSpectX0.1.1-beta.zip");
                    SevenZipExtractor sze = new SevenZipExtractor(Application.StartupPath + "\\download\\LoLSpectX0.1.1-beta.zip");
                    sze.ExtractArchive(Application.StartupPath + "\\LoLSpectX\\");
                    wc.Dispose();
                    sze.Dispose();
                    wait.Dispose();

                    File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\lolspectx.txt").Close();

                    MessageBox.Show("下載完成! 按確定開啟 LoLSpectX 觀戰工具 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    
                
            }
            tabControl1.SelectedTab = tabPage1;
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = Application.StartupPath + "\\LoLSpectX\\LoLSpectX";
                start.FileName = Application.StartupPath + "\\LoLSpectX\\LoLSpectX\\LoLSpectX.exe";
                start.Arguments = installPath;
                Process.Start(start);
        }

        private void spect_Disposed(object sender, EventArgs e)
        {
            Variable.frmShown = false;
        }

        private void spect_Shown(object sender, EventArgs e)
        {
            Variable.frmShown = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button37_Click(object sender, EventArgs e)
        {
            AceGameUI ui = new AceGameUI(installPath);
            ui.Install(txtZipPath.Text);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            FixFriend ff = new FixFriend(installPath);
            ff.StartFix();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                Wait wait = new Wait();
                wait.Show();
                wait.progressBar1.Value = 40;
                My.Computer.FileSystem.CopyDirectory(installPath + @"\Air", Application.StartupPath + @"\bak\Air", true);
                wait.progressBar1.Value = 100;
                wait.Dispose();
                Logger.log("大廳UI備份成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI備份失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI備份失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                Wait wait = new Wait();
                wait.Show();
                wait.progressBar1.Value = 40;
                My.Computer.FileSystem.CopyDirectory(Application.StartupPath + @"\bak\Air", installPath + @"\Air", true);
                wait.progressBar1.Value = 100;
                wait.Dispose();
                Logger.log("大廳UI還原成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI還原成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                Logger.log("大廳UI還原失敗 : 沒有備份", Logger.LogType.Error);
                MessageBox.Show("大廳UI還原失敗 : 沒有備份", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI還原失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI還原失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                My.Computer.FileSystem.DeleteDirectory(Application.StartupPath + @"\bak\Air", Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                Logger.log("大廳UI刪除備份成功!", Logger.LogType.Info);
                MessageBox.Show("大廳UI刪除備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                Logger.log("大廳UI刪除備份失敗 : 沒有備份", Logger.LogType.Error);
                MessageBox.Show("大廳UI刪除備份失敗 : 沒有備份", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.log("大廳UI刪除備份失敗 : ", Logger.LogType.Error);
                Logger.log(ex, Logger.LogType.Error);
                MessageBox.Show("大廳UI刪除備份失敗 : " + ex, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                txtZipPath.Text = f.SelectedPath;
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://forum.gamer.com.tw/C.php?bsn=17532&snA=430322&tnum=420");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                //註冊熱鍵 : F8關閉LoL
                HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.None, Keys.F8); 
            }
            else
            {
                //取消熱鍵 : F8關閉LoL
                HotKey.UnregisterHotKey(Handle, 101); 
            }
        }
        protected override void WndProc(ref Message m)
        {
            //HotKey Event
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 101:    //F8 
                            //Close LOL
                            Process[] lolProc = Process.GetProcessesByName("League of Legends");
                            if (lolProc.Length >= 1)
                            {
                                foreach (Process p in lolProc)
                                {
                                    p.Kill();
                                }
                            }
                            else
                            {
                                MessageBox.Show("LoL 沒有運行~");
                            }
                            break;

                    }
                    break;
            }

            base.WndProc(ref m);
        }
    }
}


