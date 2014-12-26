using ImageMagick;
using LoLToolsX.Core;
using LoLToolsX.Core.Update;
using SevenZip;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LoLToolsX
{

    /// <summary>
    /// 台服工具
    /// </summary>


    public partial class TwTools : Form
    {     
        CFGFile CFGFile = new CFGFile(Variable.CurrentDirectory + "\\config.ini");
        CursorsEdit cursorEdit;

        public TwTools()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化
            this.Initialize();
            //檢查 LoL 路徑
            this.CheckPath();

            //目前可以檢查更新?
            if (Variable.allowUpdate)
            {
                //允許自動檢查更新
                if (CFGFile.GetValue("LoLToolsX", "AutoUpdate") == "true")
                {
                    Variable.updating = true;  //正在更新
                    this.checkBox1.Checked = false;
                    Thread updateThread = new Thread(new ThreadStart(CheckUpdate.checkUpdate));
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

            //取得目前伺服器
            CheckProp checkProp = new CheckProp();
            serverLocation.Text = checkProp.CheckPropFL(Variable.installPath);

            string[] skins = File.ReadAllLines(Variable.CurrentDirectory + @"\Skin.txt");
            foreach (string s in skins)
            {
                installedSkin.Items.Add(s);
            }

            this.FinalizeStartup();
        }

        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.log("開啟NitroXenon的BLOG...", Logger.LogType.Info);
            Process.Start("http://nitroxenon.com");
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.TW);
            writer.Write();
            serverLocation.Text = "台服";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.SEA);
            writer.Write();
            serverLocation.Text = "SEA服";
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.OCE);
            writer.Write();
            serverLocation.Text = "大洋洲服";
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.NA);
            writer.Write();
            serverLocation.Text = "美服";
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.EUW);
            writer.Write();
            serverLocation.Text = "EUW服";
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.PBE);
            writer.Write();
            serverLocation.Text = "PBE服";
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.KR);
            writer.Write();
            serverLocation.Text = "韓服";
            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            PropertiesWriter writer = new PropertiesWriter(Server.EUNE);
            writer.Write();
            serverLocation.Text = "EUNE服";
            
        }

        private void backProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(1,1);
        }

        private void restoneProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(2,1);
        }

        private void delBakProp_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(3,1);
        }

        private void startLoL_Click(object sender, EventArgs e)
        {
            this.Opacity = 50;

            StartGame startGame = new StartGame(this,null);

            if (serverLocation.Text == "台服")
            {
                startGame.StartGarena();
            }
            else if (serverLocation.Text == "SEA服")
            {
                startGame.StartGarena();
            }
            else
            {
                startGame.StartRiotL();
            }
        }

        private void lChin_Click(object sender, EventArgs e)
        {
            LangEdit.ChinLobby(Variable.installPath);

        }

        private void lEng_Click(object sender, EventArgs e)
        {
            LangEdit.EngLobby(Variable.installPath);
        }

        private void gChin_Click(object sender, EventArgs e)
        {
            LangEdit.ChinGame(Variable.installPath);
        }

        private void gEng_Click(object sender, EventArgs e)
        {
            LangEdit.EngGame(Variable.installPath);
        }

        private void BakLang_Click(object sender, EventArgs e)
        {
            LangBakRes.Backup(Variable.installPath);
        }

        private void ResLang_Click(object sender, EventArgs e)
        {
            LangBakRes.Restore(Variable.installPath);
        }

        private void delLangBak_Click(object sender, EventArgs e)
        {
            LangBakRes.Delete();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://forum.gamer.com.tw/C.php?bsn=17532&snA=398091&tnum=267");
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nitroxenon.com/lol-tools-tw/lol-lobby-theme/");
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.ToLower().Contains("champions"))
                {
                    Logger.log("champions 資料夾選擇成功: " +folderBrowserDialog1.SelectedPath, Logger.LogType.Info);
                    tbLobbySound.Text = folderBrowserDialog1.SelectedPath;
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
            if (!String.IsNullOrEmpty(tbGameSound.Text))
            {
                if (!Variable.switchingSound)
                {
                    SwitchSound switchSound = new SwitchSound();
                    switchSound.SwitchGame(tbGameSound.Text);
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
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Sound(1);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Sound(2);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Sound(3);
        }


        private void Button15_Click(object sender, EventArgs e)
        {
            PropEdit pe = new PropEdit(Variable.installPath,websiteIn.Text,1);
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
            InstallUI.GameUI(Variable.installPath);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CFGFile ini = new CFGFile(Variable.CurrentDirectory + @"\config.ini");
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
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.UI(1);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.UI(2);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.UI(3);
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            ChatEdit ce = new ChatEdit(Variable.installPath);
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
                string tmp = Utility.PingCheck(serverAry[i]);
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
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.LoL(1);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.LoL(2);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            LobbyUI lui = new LobbyUI(Variable.installPath);
            lui.ShowDialog();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(1,1);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(2,1);
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            TwBakRes br = new TwBakRes(Variable.installPath);
            br.Prop(3,1);
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            LobbyUI lui = new LobbyUI(Variable.installPath);
            lui.Show();
        }

        private void installSound_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLobbySound.Text))
            {
                if (!Variable.switchingSound)
                {
                    SwitchSound switchSound = new SwitchSound();
                    switchSound.SwitchLobby(tbLobbySound.Text);
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
                    wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/SevenZipSharp/SevenZipSharp.dll", Variable.CurrentDirectory + @"\SevenZipSharp.dll");
                    wait.progressBar1.Value = 70;
                    wc.DownloadFile("https://github.com/NitroXenon/LoLToolsX-in-CSharp/releases/download/7z/7z.dll", Variable.CurrentDirectory + @"\7z.dll");
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
            
        }

        private void gKR_Click(object sender, EventArgs e)
        {
            
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
            if (!File.Exists(Variable.CurrentDirectory + @"\SevenZipSharp.dll") || !File.Exists(Variable.CurrentDirectory + @"\7z.dll"))
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
                            string str = File.ReadAllText(Variable.CurrentDirectory + @"\Skin.txt");



                            File.WriteAllText(Variable.CurrentDirectory + @"\Skin.txt", str, Encoding.Default);

                            InstallSkin.Skin(Variable.installPath, skinPathBox.SelectedItem.ToString(), Path.GetFileName(skinPathBox.SelectedItem.ToString()));
                            FileStream fs = new FileStream(Variable.CurrentDirectory + @"\Skin.txt", FileMode.Append, FileAccess.Write);
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
                    string str = File.ReadAllText(Variable.CurrentDirectory + @"\Skin.txt");
                    
                    
                    
                    File.WriteAllText(Variable.CurrentDirectory + @"\Skin.txt", str, Encoding.Default);

                    string temp = File.ReadAllText(Variable.CurrentDirectory + @"\Skin.txt");
                    string newTemp = temp.Replace(installedSkin.SelectedItem.ToString() + "\r\n" , "");
                    File.WriteAllText(Variable.CurrentDirectory + @"\Skin.txt", newTemp);

                    string temp2 = File.ReadAllText(Variable.installPath + @"\Game\ClientZips.txt");
                    string newTemp2 = temp2.Replace(installedSkin.SelectedItem.ToString() + "\r\n", "");
                    File.WriteAllText(Variable.installPath + @"\Game\ClientZips.txt", newTemp2);
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
            PropEdit pe = new PropEdit(Variable.installPath, "http://lobby.lol.tw/", 1);
            pe.LobbyLanding();
        }

        private void TwTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.Delete(Variable.CurrentDirectory + "\\temp",true);
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
                    wc.DownloadFile("https://github.com/NitroXenon/LoLSpectX/releases/download/v0.1.6-beta/LoLSpectX0.1.6-beta.zip", Variable.CurrentDirectory + "\\download\\LoLSpectX0.1.1-beta.zip");
                    SevenZipExtractor sze = new SevenZipExtractor(Variable.CurrentDirectory + "\\download\\LoLSpectX0.1.1-beta.zip");
                    sze.ExtractArchive(Variable.CurrentDirectory + "\\LoLSpectX\\");
                    wc.Dispose();
                    sze.Dispose();
                    wait.Dispose();

                    File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\lolspectx.txt").Close();

                    MessageBox.Show("下載完成! 按確定開啟 LoLSpectX 觀戰工具 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (!Directory.Exists(Variable.CurrentDirectory + @"\LoLSpectX"))
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
                    wc.DownloadFile("https://github.com/NitroXenon/LoLSpectX/releases/download/v0.1.6-beta/LoLSpectX0.1.6-beta.zip", Variable.CurrentDirectory + "\\download\\LoLSpectX0.1.1-beta.zip");
                    SevenZipExtractor sze = new SevenZipExtractor(Variable.CurrentDirectory + "\\download\\LoLSpectX0.1.1-beta.zip");
                    sze.ExtractArchive(Variable.CurrentDirectory + "\\LoLSpectX\\");
                    wc.Dispose();
                    sze.Dispose();
                    wait.Dispose();

                    File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\lolspectx.txt").Close();

                    MessageBox.Show("下載完成! 按確定開啟 LoLSpectX 觀戰工具 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    
                
            }
            tabControl1.SelectedTab = tabPage1;
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = Variable.CurrentDirectory + "\\LoLSpectX\\LoLSpectX";
                start.FileName = Variable.CurrentDirectory + "\\LoLSpectX\\LoLSpectX\\LoLSpectX.exe";
                start.Arguments = Variable.installPath;
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
            AceGameUI ui = new AceGameUI(Variable.installPath);
            ui.Install(txtZipPath.Text);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            FixFriend ff = new FixFriend(Variable.installPath);
            ff.StartFix();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                Wait wait = new Wait();
                wait.Show();
                wait.progressBar1.Value = 40;
                My.Computer.FileSystem.CopyDirectory(Variable.installPath + @"\Air", Variable.CurrentDirectory + @"\bak\Air", true);
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
                My.Computer.FileSystem.CopyDirectory(Variable.CurrentDirectory + @"\bak\Air", Variable.installPath + @"\Air", true);
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
                My.Computer.FileSystem.DeleteDirectory(Variable.CurrentDirectory + @"\bak\Air", Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
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

        private void Initialize()
        {
            //備份主控台沒有遺失
            if (!Variable.allowBakRes)
            {
                button24.Enabled = false;
                button25.Enabled = false;
            }

            //寫入目前LoLToolsX版本到Log
            Logger.log("LoLToolsX版本: " + Application.ProductVersion, Logger.LogType.Info);

            Logger.log("目前客戶端 : 台服", Logger.LogType.Info);

            //載入LoLToolsX Logo
            PictureBox1.Image = Properties.Resources.logo;
            Logger.log("LoLToolsX Logo載入成功!", Logger.LogType.Info);

            try
            {
                Directory.CreateDirectory(Variable.CurrentDirectory + "\\temp");
            }
            catch { }
        }

        private void CheckPath()
        {
            if (!Variable.forceSelectPath)
            {
                //取得LoL路徑
                GetReg getReg = new GetReg();
                Variable.installPath = getReg.TwPath(Variable.CurrentDirectory + "\\config.ini");
                Logger.log("LoL目錄取得成功! " + Variable.installPath, Logger.LogType.Info);
            }

            //檢查路徑是否存有 LoLTW 字串
            if (!Variable.installPath.Contains("LoLTW"))
            {
                if (MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    Logger.log("LoL手動選擇目錄! ", Logger.LogType.Info);

                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        Variable.installPath = folderBrowserDialog1.SelectedPath;
                        Logger.log("LoL目錄檢查成功! " + Variable.installPath, Logger.LogType.Info);
                        CFGFile.SetValue("LoLPath", "TwPath", "\"" + Variable.installPath + "\"");
                        CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.log("LoL目錄檢查失敗 ", Logger.LogType.Error);
                        Logger.log("強制關閉程式... ", Logger.LogType.Info);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Logger.log("LoL目錄選擇取消 ", Logger.LogType.Error);
                    Logger.log("關閉程式... " + Variable.installPath, Logger.LogType.Info);
                    Environment.Exit(0);
                }
            }
            else
            {
                CFGFile.SetValue("LoLPath", "TwPath", "\"" + Variable.installPath + "\"");
                CFGFile.SetValue("LoLToolsX", "Version", Application.ProductVersion.ToString());
                PathLabel.Text = Variable.installPath;
                Logger.log("LoL目錄檢查成功! ", Logger.LogType.Info);
                Logger.log("LoL目錄寫入成功! " + Variable.installPath, Logger.LogType.Info);
            }
        }

        private void FinalizeStartup()
        {
            notifyIcon1.Visible = false;
            PathLabel.Text = Variable.installPath;

            //取得版本資訊
            toolsVersion.Text = Application.ProductVersion.ToString();
            LoLVersionLabel.Text = Utility.GetLoLVer();



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

            Variable.curClient = "台服";

            cursorEdit = new CursorsEdit(pBoxCursor1, pBoxCursor2);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath.Contains("zh_TW") || folderBrowserDialog1.SelectedPath.Contains("ko_KR"))
                {
                    Logger.log("遊戲語音資料夾選擇成功: " + folderBrowserDialog1.SelectedPath, Logger.LogType.Info);
                    tbGameSound.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Sound資料夾選擇錯誤", Logger.LogType.Error);
                }
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("目前中文遊戲語言修改功能有 BUG\r\n\r\n請勿使用中文遊戲語言修改\r\n\r\n並請在切換成英文遊戲語言前進行備份 謝謝", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCurrentCursors_Click(object sender, EventArgs e)
        {
            cursorEdit.GetCurrentCursors();
            cursorEdit.ShowCurrentCursors();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog() {
                Title = "請選擇 .tga/.cur 鼠標檔案" ,
                Filter = "鼠標檔案|*.tga;*.cur"
            };

            if (DialogResult.OK == fileDialog.ShowDialog())
            {
                if (Path.GetExtension(fileDialog.FileName) == ".cur")
                {
                    using (MagickImage image = new MagickImage(fileDialog.FileName))
                    {
                        try
                        {
                            image.Format = MagickFormat.Tga;
                            string targetPath = Path.GetDirectoryName(fileDialog.FileName) + "\\" + Path.GetFileName(fileDialog.FileName) + ".tga";
                            image.Write(targetPath);
                            tbCursor1Path.Text = targetPath;
                            MessageBox.Show("CUR ---> TGA 轉檔成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("CUR ---> TGA 轉檔失敗!\r\n無法使用此鼠標檔案!\r\n錯誤訊息\r\n\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Logger.log(ex);
                        }
                    }
                }
                else
                {
                    tbCursor1Path.Text = fileDialog.FileName;
                }
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "請選擇 .tga 鼠標檔案",
                Filter = "鼠標檔案|*.tga;*.cur"
            };

            if (DialogResult.OK == fileDialog.ShowDialog())
            {
                if (Path.GetExtension(fileDialog.FileName) == ".cur")
                {
                    using (MagickImage image = new MagickImage(fileDialog.FileName))
                    {
                        try
                        {
                            image.Format = MagickFormat.Tga;
                            string targetPath = Path.GetDirectoryName(fileDialog.FileName) + "\\" + Path.GetFileName(fileDialog.FileName) + ".tga";
                            image.Write(targetPath);
                            tbCursor1Path.Text = targetPath;
                            MessageBox.Show("CUR ---> TGA 轉檔成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("CUR ---> TGA 轉檔失敗!\r\n無法使用此鼠標檔案!\r\n錯誤訊息\r\n\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Logger.log(ex);
                        }
                    }
                }
                else
                {
                    tbCursor1Path.Text = fileDialog.FileName;
                }
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            cursorEdit.Load(tbCursor1Path.Text, tbCursor2Path.Text);
            cursorEdit.Show();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            cursorEdit.SaveDefault();
        }

        private void btnCursorBackup_Click(object sender, EventArgs e)
        {
            cursorEdit.Backup();
        }

        private void btnCursorRestore_Click(object sender, EventArgs e)
        {
            cursorEdit.Restore();
        }

        private void btnCursorDelete_Click(object sender, EventArgs e)
        {
            cursorEdit.DeleteBackup();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            cursorEdit.Save(tbCursor1Path.Text, tbCursor2Path.Text);
        }
    }
}


