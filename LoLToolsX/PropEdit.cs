using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
{
    class PropEdit
    {
        string propPath;
        string website;
        string editedWebSite;

        public PropEdit(string installpath,string websiteIn)
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
            FileStream fs = new FileStream(propPath, FileMode.Open, FileAccess.ReadWrite,FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string fileContent = sr.ReadToEnd().Replace("lobbyLandingURL", "#lobbyLandingURL");;
            sr.Close();
            fs.Close();
            File.WriteAllText(propPath,fileContent + "\r\n" + "lobbyLandingURL=" + editedWebSite);
     

            MessageBox.Show("修改完成!\r\n" + website, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
