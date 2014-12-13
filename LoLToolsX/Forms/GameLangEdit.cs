using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace LoLToolsX
{
    public partial class GameLangEdit : Form
    {
        private string installPath;
        private string chtLangPath;
        private string engLangPath;

        public GameLangEdit(string _installPath)
        {
            InitializeComponent();
            this.installPath = _installPath;
            this.chtLangPath = this.installPath + @"\Game\DATA\Menu\fontconfig_zh_TW.txt";
            this.engLangPath = this.installPath + @"\Game\DATA\Menu\fontconfig_en_US.txt";
            this.Disposed += delegate {
                MessageBox.Show("英文遊戲語言修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void GameLangEdit_Load(object sender, EventArgs e)
        {
            Wait wait = new Wait();
            wait.label1.Text = "請稍後...";
            wait.Show();
            wait.Refresh();
            using (StreamReader engGameReader = new StreamReader(this.engLangPath,Encoding.UTF8))
            {
                tbENUS.Text = engGameReader.ReadToEnd();
                tbENUS.Select(0, 0);
            }

            using (StreamReader chtGameReader = new StreamReader(this.chtLangPath, Encoding.UTF8))
            {
                for (int i = 0; i < 6; i++)
                {
                    tbZHTW.AppendText(chtGameReader.ReadLine() + "\r\n");
                }
            }
            Clipboard.Clear();
            Clipboard.SetText(tbZHTW.Text);

            tbENUS.Select(0, 246);
            tbENUS.Paste(Clipboard.GetText());
            tbENUS.Select(0, 186);

            wait.progressBar1.Value = 50;
            wait.Refresh();

            try
            {
                tbFinal.Text = new WebClient().DownloadString("http://nitroxenon.com/loltoolsx/lang-prefer.txt");
                tbFinal.AppendText("\r\n\r\n以下省略...");
            }
            catch 
            {
                tbFinal.Text = "預覽獲取失敗!";
            }

            wait.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter engGameWriter = new StreamWriter(this.engLangPath,false,Encoding.UTF8))
            {
                engGameWriter.Write(tbENUS.Text);
            }
            this.Dispose();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
