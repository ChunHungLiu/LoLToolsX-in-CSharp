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
        string cd = Application.StartupPath;

        public ServerSelect()
        {
            InitializeComponent();
        }

        private void selectTW_Click(object sender, EventArgs e)
        {
            TwTools tt = new TwTools();
            tt.Show();
            this.Dispose();
        }

       
        private void ServerSelect_Load(object sender, EventArgs e)
        {
            //如果多過15個Log就全部刪
            string[] files = Directory.GetFiles(Application.StartupPath + @"\Logs");
            if (files.Length > 15)
            {
                foreach (string txt in files)
                {
                System.IO.File.Delete(txt);
                }
                File.Create(Application.StartupPath + @"\Logs\Log.txt");
            }
        }

        private void selectNA_Click(object sender, EventArgs e)
        {
            VerSelect vs = new VerSelect();
            vs.Show();
            this.Dispose();
        }
    }
}
