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
    public partial class ChatEdit : Form
    {
        public ChatEdit()
        {
            InitializeComponent();
        }

        private void ChatEdit_Load(object sender, EventArgs e)
        {
            CFGFile ini = new CFGFile(Directory.GetCurrentDirectory() + @"\config.ini");
        R.Text = ini.GetValue("ChatColour", "R");
        B.Text = ini.GetValue("ChatColour", "B");
        G.Text = ini.GetValue("ChatColour", "G");

        R2.Text = ini.GetValue("ChatColour", "R2");
        B2.Text = ini.GetValue("ChatColour", "B2");
        G2.Text = ini.GetValue("ChatColour", "G2");

        R3.Text = ini.GetValue("ChatColour", "R3");
        B3.Text = ini.GetValue("ChatColour", "B3");
        G3.Text = ini.GetValue("ChatColour", "G3");

        R4.Text = ini.GetValue("ChatColour", "R4");
        B4.Text = ini.GetValue("ChatColour", "B4");
        G4.Text = ini.GetValue("ChatColour", "G4");

        R5.Text = ini.GetValue("ChatColour", "R5");
        B5.Text = ini.GetValue("ChatColour", "B5");
        G5.Text = ini.GetValue("ChatColour", "G5");

        RS.Text = ini.GetValue("ChatColour", "RS");
        BS.Text = ini.GetValue("ChatColour", "BS");
        GS.Text = ini.GetValue("ChatColour", "GS");

        FontSize1.Text = ini.GetValue("ChatFontSize", "FontSize");
        }
    }
}
