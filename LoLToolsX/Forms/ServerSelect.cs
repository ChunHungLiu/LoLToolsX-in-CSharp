using System;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
{
    public partial class ServerSelect : Form
    {
        string cd = Variable.CurrentDirectory;

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
            GC.Collect();
            //如果多過15個Log就全部刪
            string[] files = Directory.GetFiles(Variable.CurrentDirectory + @"\Logs");
            if (files.Length > 15)
            {
                foreach (string txt in files)
                {
                System.IO.File.Delete(txt);
                }
                File.Create(Variable.CurrentDirectory + @"\Logs\Log.txt");
            }
            if (!File.Exists(Variable.CurrentDirectory + "\\config.ini"))
            { 
                File.Create(Variable.CurrentDirectory + "\\config.ini");
            }
        }

        private void selectNA_Click(object sender, EventArgs e)
        {
            VerSelect vs = new VerSelect();
            vs.Show();
            this.Dispose();
        }

        private void ServerSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
