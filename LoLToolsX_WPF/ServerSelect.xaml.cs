using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoLToolsX
{
    public partial class ServerSelect : Window
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
    }
}
