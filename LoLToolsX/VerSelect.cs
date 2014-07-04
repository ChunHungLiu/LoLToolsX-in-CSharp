using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoLToolsX
{
    public partial class VerSelect : Form
    {
        public VerSelect()
        {
            InitializeComponent();
        }

        private void VerSelect_Load(object sender, EventArgs e)
        {

        }

        public string NaVer(string installPath)
        {
            return installPath;
        }
    }
}
