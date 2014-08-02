using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LoLToolsX
{
    public partial class SwitchType : Form
    {
        string installPath;
        string soundPath;
        Wait wait = new Wait();
        SwitchSound ss;

        public SwitchType(string _installPath,string _soundPath)
        {
            InitializeComponent();

            this.installPath = _installPath;
            this.soundPath = _soundPath;

            ss = new SwitchSound(this.installPath, this.soundPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("語音安裝需時\r\n安裝過程中程式會出現當機現象\r\n請勿關閉程式 並耐心等候安裝完成\r\n按確定開始進行安裝!");
            ss.QuickSwitch();
            this.Dispose();
  
        }

        private void SwitchType_Load(object sender, EventArgs e)
        {
            if (!soundPath.Contains("Sound"))
            {
                MessageBox.Show("請選擇正確的Sound資料夾", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("語音切換: Sound 資料夾選擇錯誤", Logger.LogType.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("語音安裝需時\r\n安裝過程中程式會出現當機現象\r\n請勿關閉程式 並耐心等候安裝完成\r\n按確定開始進行安裝!");
            ss.FullSwitch();
            this.Dispose();
        }
    }
}
