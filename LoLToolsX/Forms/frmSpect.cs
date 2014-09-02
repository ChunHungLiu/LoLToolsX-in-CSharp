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
        public frmSpect()
        {
            InitializeComponent();
        }

        private void frmSpect_Load(object sender, EventArgs e)
        {
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
        }

        
        private void webBrowser1_Navigated(object sender, EventArgs e)
        {
            //MessageBox.Show(webBrowser1.Url.ToString());
            if (webBrowser1.Url.ToString() == "http://lol-clearcode.rhcloud.com//index.php" || webBrowser1.Url.ToString() == "http://lol-clearcode.rhcloud.com/index.php/home/guide")
            {
                webBrowser1.Navigate("http://nitroxenon.com/loltoolsx/spect2.html");
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Contains("content.php"))
            {
                string[] array = this.webBrowser1.Document.Body.InnerHtml.ToString().Split('|');

                Clipboard.SetText(string.Concat(new string[]
					{
						"\"",
						Variable.tw_installPath + "\\Game\\League of Legends.exe" ,
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
            }
            Microsoft.VisualBasic.Interaction.Shell("rundll32.exe shell32.dll #61", Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, false, -1);
            SendKeys.Send("^(v)");
            SendKeys.Send("{ENTER}");

            webBrowser1.GoBack();
        }
       
    }
}
