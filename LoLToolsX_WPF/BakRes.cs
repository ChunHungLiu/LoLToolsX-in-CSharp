using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LoLToolsX
{
    class BakRes
    {

        /// <summary>
        /// 檔案備份/還原
        /// </summary>

        protected string installPath_m;  //

        public BakRes(string installPath)
        {
            installPath_m = installPath;
        }


        public void Prop(int Type)     //備份伺服器設定檔 (lol.properties)
        {
            //備份
            if (Type == 1)
            {
                try
                {
                    FileInfo fi = new FileInfo(installPath_m + @"\Air\lol.properties");
                    fi.CopyTo(Directory.GetCurrentDirectory() + @"\bak\server_prop\lol.properties",true);
                    MessageBox.Show("備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //還原
            if (Type == 2)
            {
                try
                {
                    FileInfo fi = new FileInfo(Directory.GetCurrentDirectory() + @"\bak\server_prop\lol.properties");
                    fi.CopyTo(installPath_m + @"\Air\lol.properties", true);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FileNotFoundException e2)
                {
                    MessageBox.Show("還原失敗 : 沒有備份 " , "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (Exception e)
                {
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //刪除備份
            if (Type == 3)
            {
                try
                {
                    FileInfo fi = new FileInfo(Directory.GetCurrentDirectory() + @"\bak\server_prop\lol.properties");
                    fi.Delete();
                    MessageBox.Show("刪除備份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("刪除備份失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}
