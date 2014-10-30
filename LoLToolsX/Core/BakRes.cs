using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace LoLToolsX.Core
{
    class BakRes
    {
        Wait wait = new Wait();

        /// <summary>
        /// 檔案備份/還原
        /// </summary>

        private string installPath;  

        public BakRes(string installPath)  //Constructor
        {
            this.installPath = installPath;

            //如沒有備份資料夾就建立
            if (!Directory.Exists(Application.StartupPath + @"\bak\sound\FMOD\"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\bak\sound\FMOD\");
            }
            if (!Directory.Exists(Application.StartupPath + Application.StartupPath + "\\bak\\sound\\zh_TW"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\bak\\sound\\zh_TW");
            }
            if (!Directory.Exists(Application.StartupPath + "\\bak\\sound\\zh_CN"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\bak\\sound\\zh_CN");
            }
            if (!Directory.Exists(Application.StartupPath + "\\bak\\sound\\en_US"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\bak\\sound\\en_US");
            }
            if (!Directory.Exists(Application.StartupPath + "\\bak\\sound\\ko_KR"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\bak\\sound\\ko_KR");
            }
        }

        public void Prop(int Type,int client)     //備份伺服器設定檔 (lol.properties)
        {
            string propPath = "";
            string bakPath = "";
            string localePath = "";
            string localeBakPath = "";

            //Client : 1是台服 2是美服
            if (client == 1)
            {
                //台服檔案路徑
                propPath = installPath + @"\Air\lol.properties";
                bakPath = Application.StartupPath + @"\bak\server_prop\lol.properties";
                localePath = installPath + @"\Air\locale.properties";
                localeBakPath = Application.StartupPath + @"\bak\server_prop\locale.properties";
            }
            else
            {
                //美服檔案路徑
                propPath = installPath + @"\lol.properties";
                bakPath = Application.StartupPath + @"\bak\na_server_prop\lol.properties";
                localePath = installPath + @"\locale.properties";
                localeBakPath = Application.StartupPath + @"\bak\na_server_prop\locale.properties";
            }

            //備份
            if (Type == 1)
            {
                try
                {
                    try
                    {
                        File.Copy(localePath, localeBakPath);
                    }
                    catch { }
                    FileInfo fi = new FileInfo(propPath);
                    fi.CopyTo(bakPath,true);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("伺服器設定檔 備份成功!", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("伺服器設定檔 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);

                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    try
                    {
                        File.Copy(localeBakPath,localePath);
                    }
                    catch { }
                    FileInfo fi = new FileInfo(bakPath);
                    fi.CopyTo(propPath, true);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("伺服器設定檔 還原成功!", Logger.LogType.Info);
                }
                catch (FileNotFoundException e2)
                {
                    MessageBox.Show("還原失敗 : 沒有備份 " , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("伺服器設定檔 還原失敗 : 沒有備份檔案", Logger.LogType.Error);
                    Logger.log(e2);
                }

                catch (Exception e)
                {
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("伺服器設定檔 還原失敗!", Logger.LogType.Error);
                    Logger.log(e);
                }
            }

            //刪除備份
            if (Type == 3)
            {
                try
                {
                    try
                    {
                        File.Delete(localeBakPath);
                    }
                    catch { }
                    FileInfo fi = new FileInfo(bakPath);
                    fi.Delete();
                    Logger.log("伺服器設定檔 備份刪除成功!", Logger.LogType.Info);
                    MessageBox.Show("刪除備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("伺服器設定檔 備份刪除失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("刪除備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public void NaLang(int Type)   //備份語言檔(美服)
        {
            string cd = Application.StartupPath;
            //備份
            if (Type == 1)
            {
                try
                {
                    File.Copy(installPath + @"\css\fonts.swf", cd + @"\bak\na_lang\fonts.swf", true);
                    File.Copy(installPath + @"\css\fonts_zh_TW.swf", cd + @"\bak\na_lang\fonts_zh_TW.swf", true);
                    try
                    {
                        File.Copy(installPath + @"\css\fonts_ko_KR.swf", cd + @"\bak\na_lang\fonts_ko_KR.swf", true);
                    }
                    catch { }
                    try
                    {
                        File.Copy(installPath + @"\css\fonts_en_US.swf", cd + @"\bak\na_lang\fonts_en_US.swf", true);
                    }
                    catch { }
                    File.Copy(installPath + @"\locale.properties", cd + @"\bak\na_lang\locale.properties", true);
                    Logger.log("語言檔 備份成功!", Logger.LogType.Info);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("語言檔 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //還原
            if (Type == 2)
            {
                try
                {
                    File.Copy(cd + @"\bak\na_lang\fonts.swf", installPath + @"\css\fonts.swf", true);
                }
                catch { }
                try
                {
                    File.Copy(cd + @"\bak\na_lang\fonts_zh_TW.swf", installPath + @"\css\fonts_zh_TW.swf", true);
                }
                catch { }
                try
                {
                    File.Copy(cd + @"\bak\na_lang\fonts_en_US.swf", installPath + @"\css\fonts_en_US.swf", true);
                }
                catch { }
                try
                {
                    File.Copy(cd + @"\bak\na_lang\fonts_ko_KR.swf", installPath + @"\css\fonts_ko_KR.swf", true);
                }
                catch { }
                File.Copy(cd + @"\bak\na_lang\locale.properties", installPath + @"\locale.properties", true);
                Logger.log("語言檔 還原成功!", Logger.LogType.Info);
                MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //刪除備份
            if (Type == 3)
            {
                if (Type == 3)
                {
                    try
                    {
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\Locale.cfg");
                        }
                        catch { }
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\fonts.swf");
                        }
                        catch { }
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\fonts_zh_TW.swf");
                        }
                        catch { }
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\fonts_en_US.swf");
                        }
                        catch { }
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\fonts_ko_KR.swf");
                        }
                        catch { }
                        try
                        {
                            File.Delete(cd + @"\bak\na_lang\locale.properties");
                        }
                        catch { }
                        Logger.log("語言檔 備份刪除成功! ", Logger.LogType.Info);
                        MessageBox.Show("備份刪除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception e)
                    {
                        Logger.log(e);
                        MessageBox.Show("備份刪除失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void Lang(int Type)     //備份語言檔
        {

            string cd = Application.StartupPath;
            //備份
            if (Type == 1)
            {

                try
                {
                    try
                    {
                        File.Copy(installPath + @"\Game\DATA\CFG\defaults\GamePermanent_ko_KR.cfg", cd + @"\bak\lang\GamePermanent_ko_KR.cfg", true);
                    }
                    catch { }
                    File.Copy(installPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", cd + @"\bak\lang\FontTypes.xml", true);
                    File.Copy(installPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", cd + @"\bak\lang\GamePermanent_zh_TW.cfg", true);
                    File.Copy(installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", cd + @"\bak\lang\GamePermanent.cfg", true);
                    File.Copy(installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt",cd + @"\bak\lang\fontconfig_en_US.txt", true);
                    File.Copy(installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", cd + @"\bak\lang\fontconfig_zh_TW.txt", true);
                    File.Copy(installPath + @"\Game\DATA\CFG\Locale.cfg",cd + @"\bak\lang\Locale.cfg", true);
                    File.Copy(installPath + @"\Air\css\fonts.swf", cd + @"\bak\lang\fonts.swf", true);
                    File.Copy(installPath + @"\Air\css\fonts_zh_TW.swf", cd + @"\bak\lang\fonts_zh_TW.swf", true);
                    try
                    {
                        File.Copy(installPath + @"\Air\css\fonts_ko_KR.swf", cd + @"\bak\lang\fonts_ko_KR.swf", true);
                    }
                    catch { }
                    File.Copy(installPath + @"\Air\locale.properties", cd + @"\bak\lang\locale.properties", true);
                    Logger.log("語言檔 備份成功!", Logger.LogType.Info);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("語言檔 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    try
                    {
                        File.Copy(cd + @"\bak\lang\GamePermanent_ko_KR.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_ko_KR.cfg", true);
                    }
                    catch
                    { }
                    try
                    {
                        File.Copy(cd + @"\bak\lang\FontTypes.xml", installPath + @"\Game\DATA\CFG\defaults\FontTypes.xml", true);
                    }
                    catch { }
                    try
                    {
                        File.Copy(cd + @"\bak\lang\GamePermanent_zh_TW.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent_zh_TW.cfg", true);
                    }
                    catch { }
                    try
                    {
                        File.Copy(cd + @"\bak\lang\GamePermanent.cfg", installPath + @"\Game\DATA\CFG\defaults\GamePermanent.cfg", true);
                    }
                    catch { }
                    try
                    {
                        File.Copy(cd + @"\bak\lang\fontconfig_en_US.txt", installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                    }
                    catch {}
                    try
                    {
                        File.Copy(cd + @"\bak\lang\fontconfig_zh_TW.txt", installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                    }
                    catch {}
                    try
                    {
                        File.Copy(cd + @"\bak\lang\Locale.cfg", installPath + @"\Game\DATA\CFG\Locale.cfg", true);
                    }
                    catch {}
                    try
                    {
                        File.Copy(cd + @"\bak\lang\fonts.swf", installPath + @"\Air\css\fonts.swf", true);
                    }
                    catch {}
                    try
                    {
                        File.Copy(cd + @"\bak\lang\fonts_zh_TW.swf", installPath + @"\Air\css\fonts_zh_TW.swf", true);
                    }
                    catch { }
                    try
                    {
                        File.Copy(cd + @"\bak\lang\fonts_ko_KR.swf", installPath + @"\Air\css\fonts_ko_KR.swf", true);
                    }
                    catch { }
                    File.Copy(cd + @"\bak\lang\locale.properties", installPath + @"\Air\locale.properties", true);
                    Logger.log("語言檔 還原成功!", Logger.LogType.Info);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
                catch (Exception e)
                {
                    Logger.log("語言檔 還原失敗", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //刪除備份
            if (Type == 3)
            {
                try
                {
                    try
                    {
                        File.Delete(cd + @"\bak\lang\GamePermanent_ko_KR.cfg");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\FontTypes.xml");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\GamePermanent_zh_TW.cfg");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\GamePermanent.cfg");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\fontconfig_en_US.txt");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\fontconfig_zh_TW.txt");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\Locale.cfg");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\fonts.swf");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\fonts_zh_TW.swf");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\fonts_ko_KR.swf");
                    }
                    catch { }
                    try
                    {
                        File.Delete(cd + @"\bak\lang\locale.properties");
                    }
                    catch { }
                    Logger.log("語言檔 備份刪除成功! ", Logger.LogType.Info);
                    MessageBox.Show("備份刪除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log(e);
                    MessageBox.Show("備份刪除失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        
        public void Sound(int Type)    //語音  
        {
            
            //需要備份的檔案
            //string[] fsbFile = { "VOBank_zh_TW.fsb", "VOBank_zh_CN.fsb", "VOBank_en_US.fsb", "VOBank_ko_KR.fsb", "LoL_Audio_zh_TW.fev", "LoL_Audio_zh_CN.fev", "LoL_Audio_en_US.fev", "LoL_Audio_ko_KR.fev" };

            if (Type == 1)  //備份
            {
                wait.Show();
                wait.progressBar1.Value = 0;    

                //foreach (string file in fsbFile)
                //{
                    //try
                    //{
                        //File.Copy(installPath + @"\Game\DATA\Sounds\FMOD\" + file, Application.StartupPath + @"\bak\sound\FMOD\" + file, true);
                        //wait.progressBar1.Value = wait.progressBar1.Value + 5;
                    //}
                    //catch { }
                //}
                try
                {
                    My.Computer.FileSystem.CopyDirectory(installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\zh_TW", Application.StartupPath + "\\bak\\sound\\zh_TW", true);
                }
                catch
                {
 
                }
                try
                {
                    My.Computer.FileSystem.CopyDirectory(installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\en_US", Application.StartupPath + "\\bak\\sound\\en_US", true);
                }
                catch
                {

                }

                try
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\bak\sound\air");
                    foreach (string newPath in Directory.GetFiles(installPath + @"\Air\assets\sounds\en_US\champions", "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(installPath + @"\Air\assets\sounds\en_US\champions", Application.StartupPath + @"\bak\sound\air"), true);
                    wait.progressBar1.Value = 60;
                    foreach (string newPath in Directory.GetFiles(installPath + @"\Air\assets\sounds\zh_TW\champions", "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(installPath + @"\Air\assets\sounds\zh_TW\champions", Application.StartupPath + @"\bak\sound\air"), true);
                    wait.progressBar1.Value = 100;
                    wait.progressBar1.Value = 0;
                    wait.Close();
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("語音檔 備份成功!", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    wait.Close();
                    Logger.log("語音檔 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            if (Type == 2) //還原
            {
                wait.Show();
                wait.progressBar1.Value = 0;

                //foreach (string file in fsbFile)
               //{
                    //try
                    //{
                        //File.Copy(Application.StartupPath + @"\bak\sound\FMOD\" + file, installPath + @"\Game\DATA\Sounds\FMOD\" + file, true);
                        //wait.progressBar1.Value = wait.progressBar1.Value + 15;
                    //}
                    //catch { }
                //}
                string[] folder = {
                                      "ko_KR",
                                      "zh_CN"
                                  };
                foreach (string f in folder)
                {
                    if (Directory.Exists(installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\" + f))
                    {
                        Directory.Delete(installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\" + f);
                    }

                }
                try
                {
                    My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\\bak\\sound\\zh_TW", installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\zh_TW", true);
                }
                catch
                {

                }
                try
                {
                    My.Computer.FileSystem.CopyDirectory(Application.StartupPath + "\\bak\\sound\\en_US", installPath + "\\Game\\DATA\\Sounds\\Wwise\\VO\\en_US", true);
                }
                catch
                {

                }
                try
                {
                    foreach (string newPath in Directory.GetFiles(Application.StartupPath + @"\bak\sound\air", "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(Application.StartupPath + @"\bak\sound\air",installPath + @"\Air\assets\sounds\en_US\champions"), true);
                    wait.progressBar1.Value = 50;
                    foreach (string newPath in Directory.GetFiles(Application.StartupPath + @"\bak\sound\air", "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(Application.StartupPath + @"\bak\sound\air", installPath + @"\Air\assets\sounds\zh_TW\champions"), true);
                    wait.progressBar1.Value = 100;
                    wait.progressBar1.Value = 0;
                    wait.Close();
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("語音檔 還原成功!", Logger.LogType.Info);
                }
                catch (System.IO.DirectoryNotFoundException err)
                {
                    wait.Close();
                    Logger.log("語音檔 還原失敗 : 沒有還原檔可供還原", Logger.LogType.Error);
                    Logger.log(err);
                    MessageBox.Show("沒有還原檔可供還原", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
                catch (Exception e)
                {
                    wait.Close();
                    Logger.log("語音檔 還原失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }

            if (Type == 3)  //刪除備份
            {
                //foreach (string files in fsbFile)
                //{
                    //try
                    //{
                        //File.Delete(Application.StartupPath + @"\bak\sound\FMOD\" + files);
                    //}
                    //catch { }
                //}
                try
                {
                    Directory.Delete(Application.StartupPath + @"\bak\sound\zh_TW", true);
                }
                catch
                { }
                try
                {
                    Directory.Delete(Application.StartupPath + @"\bak\sound\zh_CN", true);
                }
                catch
                { }
                try
                {
                    Directory.Delete(Application.StartupPath + @"\bak\sound\en_US", true);
                }
                catch
                { }
                try
                {
                    Directory.Delete(Application.StartupPath + @"\bak\sound\ko_KR", true);
                }
                catch
                { }
                try
                {
                    Directory.Delete(Application.StartupPath + @"\bak\sound\Air", true);
                    Logger.log("語音檔 刪除備份成功!", Logger.LogType.Error);
                    MessageBox.Show("備份刪除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                { }
            }

        }

        public void UI(int Type)       //UI備份
        {
            //備份
            if (Type == 1)
            {
                try
                {
                    FileInfo fi = new FileInfo(installPath + @"\Game\DATA\Menu\Textures\HUDAtlas.tga");
                    fi.CopyTo(Application.StartupPath + @"\bak\UI\game\HUDAtlas.tga", true);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("UI檔案 備份成功!", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI檔案 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);

                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\bak\UI\game\HUDAtlas.tga");
                    fi.CopyTo(installPath + @"\Game\DATA\Menu\Textures\HUDAtlas.tga", true);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("UI檔案 還原成功!", Logger.LogType.Info);
                }
                catch (FileNotFoundException e2)
                {
                    MessageBox.Show("還原失敗 : 沒有備份 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI檔案 還原失敗 : 沒有備份檔案", Logger.LogType.Error);
                    Logger.log(e2);
                }

                catch (Exception e)
                {
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI檔案 還原失敗!", Logger.LogType.Error);
                    Logger.log(e);
                }
            }

            //刪除備份
            if (Type == 3)
            {
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\bak\UI\game\HUDAtlas.tga");
                    fi.Delete();
                    Logger.log("UI檔案 備份刪除成功!", Logger.LogType.Info);
                    MessageBox.Show("刪除備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("UI檔案 備份刪除失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("刪除備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public void NaUi(int Type)       //美服UI備份
        {
            if (!Directory.Exists(Application.StartupPath + @"\bak\UI\na\game"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\bak\UI\na\game");
            }

            //備份
            if (Type == 1)
            {
                try
                {
                    string dir = Variable.n_installPath + @"\RADS\projects\lol_game_client\filearchives";
                    string[] folder = Directory.GetDirectories(dir);
                    //尋找UI檔案
                    foreach (string f in folder)
                    {
                        if (Directory.Exists(f + @"\DATA\Menu\Textures"))
                        {
                            if (File.Exists(f + @"\DATA\Menu\Textures\HUDAtlas.tga"))
                            {
                                File.Copy(f + @"\DATA\Menu\Textures\HUDAtlas.tga", Application.StartupPath + @"\bak\UI\na\game\HUDAtlas.tga", true);
                                Logger.log("UI檔案 備份成功!", Logger.LogType.Info);
                                MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //如已備份UI 就無需重複備份
                                return;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI檔案 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);

                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    string dir = Variable.n_installPath + @"\RADS\projects\lol_game_client\filearchives";
                    string[] folder = Directory.GetDirectories(dir);
                    foreach (string f in folder)
                    {
                        if (!Directory.Exists(f + "\\DATA"))
                        {
                            Directory.CreateDirectory(f + "\\DATA");
                        }
                        if (!Directory.Exists(f + "\\DATA\\Menu"))
                        {
                            Directory.CreateDirectory(f + "\\DATA\\Menu");

                        }
                        if (!Directory.Exists(f + "\\DATA\\Menu\\Textures"))
                        {
                            Directory.CreateDirectory(f + "\\DATA\\Menu\\Textures");
                        }
                        File.Copy(Application.StartupPath + @"\bak\UI\na\game\HUDAtlas.tga", f + "\\DATA\\Menu\\Textures\\HUDAtlas.tga", true);
                    }
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("UI檔案 還原成功!", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("UI檔案 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);

                }
            }

            //刪除備份
            if (Type == 3)
            {
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\bak\UI\na\game\HUDAtlas.tga");
                    fi.Delete();
                    Logger.log("UI檔案 備份刪除成功!", Logger.LogType.Info);
                    MessageBox.Show("刪除備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("UI檔案 備份刪除失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("刪除備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public void Chat(int Type)     //備份 Chat.ini
        {
            //備份
            if (Type == 1)
            {
                try
                {
                    FileInfo fi = new FileInfo(installPath + @"\Game\DATA\Menu\HUD\defaults\Chat.ini");
                    fi.CopyTo(Application.StartupPath + @"\bak\Chat\Chat.ini", true);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("Chat.ini 備份成功!", Logger.LogType.Info);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Chai.ini 備份失敗!", Logger.LogType.Error);
                    Logger.log(e);

                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\bak\Chat\Chat.ini");
                    fi.CopyTo(installPath + @"\Game\DATA\Menu\HUD\defaults\Chat.ini", true);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.log("Chat.ini 還原成功!", Logger.LogType.Info);
                }
                catch (FileNotFoundException e2)
                {
                    MessageBox.Show("還原失敗 : 沒有備份 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Chat.ini 還原失敗 : 沒有備份檔案", Logger.LogType.Error);
                    Logger.log(e2);
                }

                catch (Exception e)
                {
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("Chat.ini 還原失敗!", Logger.LogType.Error);
                    Logger.log(e);
                }
            }

            //刪除備份
            if (Type == 3)
            {
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\bak\Chat\Chat.ini");
                    fi.Delete();
                    Logger.log("Chat.ini 備份刪除成功!", Logger.LogType.Info);
                    MessageBox.Show("刪除備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    Logger.log("Chat.ini 備份刪除失敗!", Logger.LogType.Error);
                    Logger.log(e);
                    MessageBox.Show("刪除備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public void LoL(int Type)   //一鍵備份
        {
            if (Type == 1)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        Arguments = "Backup " + installPath,
                        Verb = "runas",
                        WorkingDirectory = Application.StartupPath,
                        FileName = "LoLBakRes.exe"
                    });
                    Logger.log("LoL一鍵備份 開始!", Logger.LogType.Info);
                }
                catch
                {
 
                }
            }

                if (Type == 2)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo { 
                            Arguments = "Restore " + installPath,
                            Verb = "runas",
                            WorkingDirectory = Application.StartupPath,
                            FileName = "LoLBakRes.exe"
                        });
                        Logger.log("LoL一鍵還原 開始!", Logger.LogType.Info);
                    }
                    catch
                    {
 
                    }
                }
        }
    }
}
