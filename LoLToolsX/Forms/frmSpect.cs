using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LoLToolsX.Forms
{
    public partial class frmSpect : Form
    {
        string lolPath;

        public frmSpect(string lolPath)
        {
            this.lolPath = lolPath;
            InitializeComponent();
        }

        private void frmSpect_Load(object sender, EventArgs e)
        {
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            //webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("content.php"))
            {
                //檢查LOL是否正在運行
                Process[] lolProc = Process.GetProcessesByName("League of Legends");
                if (lolProc.Length == 0)
                {
                    string[] array = this.webBrowser1.Document.Body.InnerHtml.ToString().Split('|');

                    //ProcessStartInfo startSpect = new ProcessStartInfo();
                    //startSpect.FileName = lolPath + "\\Game\\League of Legends.exe";
                    //startSpect.Arguments = "\"8390\" \"LoL.exe\" \"Air\\LOLClient.exe\" \"spectator " + array[0] + " " +array[1].Replace(" ", "+") +" " +array[2] +" " +array[3] +"\"";
                    //startSpect.Arguments = "\"8390\" \""  + lolPath + "LoL.exe\"" + " \"" +  Variable.tw_installPath + "Air\\LOLClient.exe\" \"spectator " + array[0] + " " +array[1].Replace(" ", "+") +" " +array[2] +" " +array[3] +"\"";
                    //startSpect.WorkingDirectory = Variable.tw_installPath;
                    //startSpect.UseShellExecute = true;
                    //Process.Start(startSpect);
                    //Process.Start(Variable.tw_installPath + "\\Game\\League of Legends.exe", "\"8390\" \"LoL.exe\" \"Air\\LOLClient.exe\" \"spectator " + array[0] + " " + array[1].Replace(" ", "+") + " " + array[2] + " " + array[3] + "\"");

                    Clipboard.SetText(string.Concat(new string[]
					{
						"\"",
						lolPath + "\\Game" ,
						"\\League of Legends.exe\" \"8390\" \"LoL.exe\" \"Air\\LOLClient.exe\" \"spectator ",
						array[0],
						" ",
						array[1].Replace(" ", "+"),
						" ",
						array[2],
						" ",
						array[3],
						"\""
					}));

                    //回到上一頁
                    webBrowser1.GoBack();
                    //MessageBox.Show(startSpect.Arguments);

                    Microsoft.VisualBasic.Interaction.Shell("rundll32.exe shell32.dll #61", Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, false, -1);

                    SendKeys.Send("^(v)");
                    SendKeys.Send("{ENTER}");
                }
                else
                {
                    webBrowser1.GoBack();
                    MessageBox.Show("英雄聯盟正在運行!");
                }
            }
        }
    }
}
