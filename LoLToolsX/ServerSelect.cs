﻿using System;
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
            GC.Collect();
        }

        private void ServerSelect_Load(object sender, EventArgs e)
        {
            //開始記錄
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Logs"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs");
            Logger.start();
        }
    }
}
