using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LoLToolsX
{
    public partial class ServerSelect : Form
    {
        public ServerSelect()
        {
            InitializeComponent();
        }

        private void selectTW_Click(object sender, EventArgs e)
        {
            TwTools tt = new TwTools();
            tt.Show();
            this.Hide();
        }

        private void ServerSelect_Load(object sender, EventArgs e)
        {
            //如果多過15個Log就全部刪除
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Logs");
            if (files.Length > 15)
            {
                foreach (string txt in files)
                {
                System.IO.File.Delete(txt);
                }
                File.Create(Directory.GetCurrentDirectory() + @"\Logs\Log.txt");
            }

            // Logger 開始記錄
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Logs"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs");
            Logger.start();
        }

        private void selectNA_Click(object sender, EventArgs e)
        {
            VerSelect vs = new VerSelect();
            vs.Show();
            this.Hide();
        }
    }
}
