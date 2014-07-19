using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace LoLToolsX
{
    class Function
    {
        public static bool UploadLogs()
        {
                //Upload Log
                Logger.log("關閉程式...", Logger.LogType.Info);
                Random random = new Random();
                string rd = random.Next().ToString();
                string rdFile = Application.StartupPath + @"\Logs\Log" + rd + ".txt";
                File.Copy(Application.StartupPath + @"\Logs\Log.txt", rdFile);
                //File.Copy(Application.StartupPath + @"\Logs\Log.txt",Application.StartupPath + @"\Logs\Log" + rd + ".txt");
            try
            {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                byte[] result = Client.UploadFile("http://lolnx.pixub.com/loltoolsx/upload.php", "POST", rdFile);
                string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                //MessageBox.Show(s);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
