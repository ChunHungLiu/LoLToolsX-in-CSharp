//using Paloma;
using Ionic.Zip;
using System;
using System.IO;
using System.Windows.Forms;
using ImageMagick;


namespace LoLToolsX.Core
{
    class CursorsEdit
    {
        #region "Variables"
        readonly string datasZip = Variable.installPath + "\\Game\\Datas.zip";
        readonly string zipHand1 = "DATA\\Images\\UI\\Cursors\\Hand1.tga";
        readonly string zipHand2 = "DATA\\Images\\UI\\Cursors\\Hand2.tga";
        readonly string cursorsPath = "DATA\\Images\\UI\\Cursors\\";

        Paloma.TargaImage defaultCursor1;
        Paloma.TargaImage defaultCursor2;

        Paloma.TargaImage currentCursor1;
        Paloma.TargaImage currentCursor2;

        Paloma.TargaImage cur1;
        Paloma.TargaImage cur2;

        PictureBox pBox1;
        PictureBox pBox2;
        
        bool cur1Resized = false;
        bool cur2Resized = false;
        #endregion

        public CursorsEdit(PictureBox _pBox1, PictureBox _pBox2)
        {
            this.pBox1 = _pBox1;
            this.pBox2 = _pBox2;

            if (!Directory.Exists(Variable.CurrentDirectory + "\\files\\cursors"))
            {
                Directory.CreateDirectory(Variable.CurrentDirectory + "\\files\\cursors");
            }

            if (!File.Exists(Variable.CurrentDirectory + "\\files\\cursors\\Hand1.tga"))
                new System.Net.WebClient().DownloadFile("http://nitroxenon.com/loltoolsx/Hand1.tga", Variable.CurrentDirectory + "\\files\\cursors\\Hand1.tga");

            if (!File.Exists(Variable.CurrentDirectory + "\\files\\cursors\\Hand2.tga"))
                new System.Net.WebClient().DownloadFile("http://nitroxenon.com/loltoolsx/Hand2.tga", Variable.CurrentDirectory + "\\files\\cursors\\Hand2.tga");
        }

        public bool Load(string cur1Path,string cur2Path)
        {
            if (String.IsNullOrEmpty(cur1Path) && String.IsNullOrEmpty(cur2Path))
            {
                MessageBox.Show("請先選擇鼠標TGA或CUR檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                if (!String.IsNullOrEmpty(cur1Path))
                {
                    cur1 = new Paloma.TargaImage(cur1Path);
                }
                if (!String.IsNullOrEmpty(cur2Path))
                {
                    cur2 = new Paloma.TargaImage(cur2Path);
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("鼠標載入錯誤!\r\n錯誤訊息:\r\n\r\n" + e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void SaveDefault()
        {
            Save(Variable.CurrentDirectory + "\\files\\cursors\\Hand1.tga", Variable.CurrentDirectory + "\\files\\cursors\\Hand2.tga");
        }

        public bool Save(string cursor1, string cursor2)
        {
            if (String.IsNullOrEmpty(cursor1) && String.IsNullOrEmpty(cursor2))
            {
                MessageBox.Show("請先選擇鼠標TGA或CUR檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (cur1Resized)
            {
            	pBox1.Image.Save(cursor1,System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            
            
            using (ZipFile zFile = new ZipFile(datasZip))
            {
                try
                {
                    if (!String.IsNullOrEmpty(cursor1))
                    {
                        File.Copy(cursor1, Variable.CurrentDirectory + "\\temp\\Hand1.tga", true);
                        try
                        {
                            zFile.RemoveEntry(zipHand1);
                        }
                        catch {}
                        zFile.AddFile(Variable.CurrentDirectory + "\\temp\\Hand1.tga", cursorsPath);
                    }
                    if (!String.IsNullOrEmpty(cursor2))
                    {
                        File.Copy(cursor2, Variable.CurrentDirectory + "\\temp\\Hand2.tga", true);
                        try
                        {
                            zFile.RemoveEntry(zipHand2);
                        }
                        catch {}
                        zFile.AddFile(Variable.CurrentDirectory + "\\temp\\Hand2.tga", cursorsPath);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("鼠標修改失敗!\r\n錯誤訊息:\r\n\r\n" + e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    return false; 
                }

                zFile.Save(Variable.installPath + "\\Game\\_Datas.zip");
            }

            File.Copy(Variable.installPath + "\\Game\\_Datas.zip", datasZip,true);
            File.Delete(Variable.installPath + "\\Game\\_Datas.zip");

            MessageBox.Show("鼠標修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        public bool Show()
        {
            try
            {
                if (cur1 != null)
                {
                    pBox1.Image = cur1.Image;
                    /*
                    if (cur1.Header.Height != 48 || cur1.Header.Width != 48)
                    {
                    	pBox1.Image = (System.Drawing.Bitmap)Utility.ResizeImage(cur1.Image,48,48);
                    	cur1Resized = true;
                    }
                    */
                }
                if (cur2 != null)
                {
                    pBox2.Image = cur2.Image;
                    /*
                    if (cur2.Header.Height != 48 || cur2.Header.Width != 48)
                    {
                    	pBox2.Image = (System.Drawing.Bitmap)Utility.ResizeImage(cur2.Image,48,48);
                    	cur2Resized = true;
                    }
                    */
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("鼠標預覽失敗! 錯誤訊息:\r\n\r\n" + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool LoadDefault()
        {
            try
            {
                defaultCursor1 = new Paloma.TargaImage(Variable.CurrentDirectory + "\\files\\cursors\\Hand1.tga");
                defaultCursor2 = new Paloma.TargaImage(Variable.CurrentDirectory + "\\files\\cursors\\Hand2.tga");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("鼠標載入錯誤!\r\n錯誤訊息:\r\n\r\n" + e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ShowDefault()
        {
            try
            {
                pBox1.Image = defaultCursor1.Image;
                pBox2.Image = defaultCursor2.Image;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("鼠標預覽失敗! 錯誤訊息:\r\n\r\n" + e, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool GetCurrentCursors()
        {
            try
            {
                using (ZipFile zipF = new ZipFile(datasZip))
                {
#if DEBUG
                    MessageBox.Show(datasZip);
#else
#endif
                    foreach (ZipEntry e in zipF)
                    {
                    	//e.FileName = e.FileName.Replace('/','\\');
                        //MessageBox.Show(e.FileName);
                        if (e.FileName.Replace('/','\\') == zipHand1)
                        {
                            e.Extract(Variable.CurrentDirectory + "\\temp\\Hand1", ExtractExistingFileAction.OverwriteSilently);
                        }
                        if (e.FileName.Replace('/','\\') == zipHand2)
                        {
                            e.Extract(Variable.CurrentDirectory + "\\temp\\Hand2", ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("壓縮檔讀取失敗! 錯誤訊息:\r\n\r\n" + e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log(e);
                return false;
            }
            return true;
        }

        public bool ShowCurrentCursors()
        {
            try
            {
                //MessageBox.Show(Variable.CurrentDirectory + "\\temp\\Hand1\\" + zipHand1);
                currentCursor1 = new Paloma.TargaImage(Variable.CurrentDirectory + "\\temp\\Hand1\\" + zipHand1);
                currentCursor2 = new Paloma.TargaImage(Variable.CurrentDirectory + "\\temp\\Hand2\\" + zipHand2);

                pBox1.Image = currentCursor1.Image;
                pBox2.Image = currentCursor2.Image;
            }
            catch (Exception e)
            {
                MessageBox.Show("鼠標讀取失敗! 錯誤訊息:\r\n\r\n" + e.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log(e);
                return false;
            }
            //finally
            //{
            //Directory.Delete(Variable.CurrentDirectory + "\\temp");
            //}

            return true;
        }

        public void Backup()
        {
            if (GetCurrentCursors())
            {
                try
                {
                    if (!Directory.Exists(Variable.CurrentDirectory + "\\bak\\cursors"))
                        Directory.CreateDirectory(Variable.CurrentDirectory + "\\bak\\cursors");

                    File.Copy(Variable.CurrentDirectory + "\\temp\\Hand1\\" + zipHand1, Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga", true);
                    File.Copy(Variable.CurrentDirectory + "\\temp\\Hand2\\" + zipHand2, Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga", true);
                }
                catch (Exception e)
                {
                    if (File.Exists(Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga"))
                        File.Delete(Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga");
                    if (File.Exists(Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga"))
                        File.Delete(Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga");

                    MessageBox.Show("備份失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log(e);
                    return;
                }
                MessageBox.Show("備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Restore()
        {
            if (!Save(Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga", Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga"))
            {
                MessageBox.Show("還原失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("還原成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DeleteBackup()
        {
            try
            {
                if (File.Exists(Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga"))
                {
                    File.Delete(Variable.CurrentDirectory + "\\bak\\cursors\\Hand1.tga");
                }
                if (File.Exists(Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga"))
                {
                    File.Delete(Variable.CurrentDirectory + "\\bak\\cursors\\Hand2.tga");
                }
                MessageBox.Show("刪除備份成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("刪除備份失敗!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}