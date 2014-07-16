﻿using System;
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

        }
    }


}
