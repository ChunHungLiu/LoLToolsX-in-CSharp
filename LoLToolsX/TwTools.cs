using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace LoLToolsX
{

    /// <summary>
    /// 台服工具
    /// </summary>

    public partial class TwTools : Form
    {
        //public static string currentLoc;
        public static string installPath = "";

        public TwTools()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //取得LoL路徑
            GetReg gr = new GetReg();
            installPath = gr.TwPath(Directory.GetCurrentDirectory() + @"\config.ini");

            //檢查路徑是否存有 LoLTW 字串
            if (!installPath.Contains("LoLTW"))
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("無法取得LoL目錄 請手動選擇LoLTW目錄", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    folderBrowserDialog1.ShowDialog();
                    if (folderBrowserDialog1.SelectedPath.Contains("LoLTW"))
                    {
                        installPath = folderBrowserDialog1.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("目錄選擇錯誤 按確定退出程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                else
                {
                    Application.Exit();
                }

            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            CheckProp cp = new CheckProp();
            cp.CheckPropFL(installPath);
            MessageBox.Show(cp.currentLoc);

        }



    }
}


