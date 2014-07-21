using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.ApplicationServices;

namespace LoLToolsX
{
    class PropEdit
    {
        string propPath;
        string website;
        string editedWebSite;

        public PropEdit(string installpath, string websiteIn,int client)
        {
            Variable.editpropMessageBox = false;

            //1是台服 2是美服
            if (client == 1)
            {
                this.propPath = installpath + @"\Air\lol.properties";
            }
            else
            {
                this.propPath = installpath + @"\lol.properties";
            }
            this.website = websiteIn;

            /*
            #region 更新舊版檔案

            //檢測是否曾使用舊版LobbyLanding修改
            StreamReader sr = new StreamReader(this.propPath,Encoding.Default);
            string str = sr.ReadToEnd();
            sr.Close();
            if (str.Contains("#lobbyLandingURL"))
            {
                File.Delete(propPath);
                if (this.propPath == installpath + @"\Air\lol.properties")
                {
                    SwitchServer.SwitchServerLoc(installpath, Variable.curLoc);
                }
                else
                {
                    SwitchServer.SwitchServerLocNa(Variable.airPath, Variable.curLoc);
                }
            }

            #endregion
             */
        }

        public void LobbyLanding()
        {
            if (website.Contains("http://") | (website.Contains("https://")))
            {
                try
                {
                    editedWebSite = website.Replace("http://", "");
                }
                catch { }

                try
                {
                    editedWebSite = website.Replace("https://", "");
                }
                catch { }

                Logger.log("LobbyLanding : 去掉 http/https", Logger.LogType.Info);

                LobbyLandingEdit();
            }
            else
            {
                editedWebSite = "http://" + website;
                LobbyLandingEdit();
            }
        }

        public void LobbyLandingEdit()
        {
            
            FileStream fs = new FileStream(propPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string fileContent = sr.ReadToEnd().Replace("lobbyLandingURL", "#lobbyLandingURL"); ;
            sr.Close();
            fs.Close();
            File.WriteAllText(propPath, fileContent + "\r\n" + "lobbyLandingURL=" + editedWebSite);


            MessageBox.Show("修改完成!\r\n" + website, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Logger.log("LobbyLanding 修改成功 : " + website, Logger.LogType.Info);
            

            /*
            PropertiesFile prop = new PropertiesFile(propPath);
            prop.set("lobbyLandingURL", editedWebSite);
            prop.Save();

            MessageBox.Show("修改完成!\r\n" + website, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Logger.log("LobbyLanding 修改成功 : " + website, Logger.LogType.Info);
             */

             Variable.editpropMessageBox = true;

        }
    }


}
