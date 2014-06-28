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

        public PropEdit(string installpath, string websiteIn)
        {
            this.propPath = installpath + @"\Air\lol.properties";
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

    /// <summary>
    /// 定義 My 物件
    /// </summary>
    public static class My
    {
        static private Computer __Computer = new Computer();
        static private WindowsFormsApplicationBase __Application = new WindowsFormsApplicationBase();
        static private User __User = new User();

        public static ServerComputer Computer
        {
            get
            {
                return __Computer;
            }
        }

        public static WindowsFormsApplicationBase Application
        {
            get
            {
                return __Application;
            }
        }

        public static User User
        {
            get
            {
                return __User;
            }
        }
    }

}
