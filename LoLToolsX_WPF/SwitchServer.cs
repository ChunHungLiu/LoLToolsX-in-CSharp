using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
{
    class SwitchServer
    {

        public static void SwitchServerLoc(string installPath,string targetLoc)
        {
           string propPath = installPath + @"\Air\lol.properties";
           string localProp = Directory.GetCurrentDirectory() + @"\files\server_prop\" + targetLoc;
           FileInfo fi = new FileInfo(localProp);
           try
           {
               fi.CopyTo(propPath, true);
               MessageBox.Show("伺服器切換成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           catch (Exception e)
           {
               MessageBox.Show("伺服器切換失敗 \n\r 錯誤訊息: " +e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
            
           
        }
    }
}
