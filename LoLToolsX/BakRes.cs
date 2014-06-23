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


        public void Lang(int Type)    //備份語言檔
        {

            string cd = Directory.GetCurrentDirectory();
            //備份
            if (Type == 1)
            {
                try
                {
                    File.Copy(installPath_m + @"\Game\DATA\Menu\fontconfig_en_US.txt",cd + @"\bak\lang\fontconfig_en_US.txt", true);
                    File.Copy(installPath_m + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", cd + @"\bak\lang\fontconfig_zh_TW.txt", true);
                    File.Copy(installPath_m + @"\Game\DATA\CFG\Locale.cfg",cd + @"\bak\lang\Locale.cfg", true);
                    File.Copy(installPath_m + @"\Air\css\fonts.swf", cd + @"\bak\lang\fonts.swf", true);
                    File.Copy(installPath_m + @"\Air\css\fonts_zh_TW.swf", cd + @"\bak\lang\fonts_zh_TW.swf", true);
                    File.Copy(installPath_m + @"\Air\locale.properties", cd + @"\bak\lang\locale.properties", true);
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
                    File.Copy(cd + @"\bak\lang\fontconfig_en_US.txt", installPath_m + @"\Game\DATA\Menu\fontconfig_en_US.txt", true);
                    File.Copy(cd + @"\bak\lang\fontconfig_zh_TW.txt", installPath_m + @"\Game\DATA\Menu\fontconfig_zh_TW.txt", true);
                    File.Copy(cd + @"\bak\lang\Locale.cfg", installPath_m + @"\Game\DATA\CFG\Locale.cfg", true);
                    File.Copy(cd + @"\bak\lang\fonts.swf", installPath_m + @"\Air\css\fonts.swf", true);
                    File.Copy(cd + @"\bak\lang\fonts_zh_TW.swf", installPath_m + @"\Air\css\fonts_zh_TW.swf", true);
                    File.Copy(cd + @"\bak\lang\locale.properties", installPath_m + @"\Air\locale.properties", true);
                    MessageBox.Show("還原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FileNotFoundException e2)
                {
                    MessageBox.Show("還原失敗 : 沒有備份 ", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (Exception e)
                {
                    MessageBox.Show("還原失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Type == 3)
            {
                try
                {
                    File.Delete(cd + @"\bak\lang\fontconfig_en_US.txt");
                    File.Delete(cd + @"\bak\lang\fontconfig_zh_TW.txt");
                    File.Delete(cd + @"\bak\lang\Locale.cfg");
                    File.Delete(cd + @"\bak\lang\fonts.swf");
                    File.Delete(cd + @"\bak\lang\fonts_zh_TW.swf");
                    File.Delete(cd + @"\bak\lang\locale.properties");
                    MessageBox.Show("備份刪除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("備份刪除失敗 \r\n 錯誤信息: " + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


    }
}
