using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// wpfLog.xaml 的互動邏輯
    /// </summary>
    public partial class wpfLog : Window  //要Window 否則不能 Show()
    {
        public wpfLog()
        {
            InitializeComponent();
        }

        public void WriteLine(string Log, Logger.LogType Type = Logger.LogType.Info)
        {
            switch (Type)
            {
                case Logger.LogType.Error:
                    Log = DateTime.Now.ToString() + "錯誤:" + Log;
                    break;
                case Logger.LogType.Info:
                    Log = DateTime.Now.ToString() + "信息:" + Log;
                    break;
                case Logger.LogType.Crash:
                    Log = DateTime.Now.ToString() + "崩潰:" + Log;
                    break;
                case Logger.LogType.Exception:
                    Log = DateTime.Now.ToString() + "異常:" + Log;
                    break;
                case Logger.LogType.Game:
                    Log = DateTime.Now.ToString() + "遊戲:" + Log;
                    break;
                case Logger.LogType.Fml:
                    Log = DateTime.Now.ToString() + "FML :" + Log;
                    break;
                default:
                    Log = DateTime.Now.ToString() + "信息:" + Log;
                    break;
            }
            Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate { listBox1.Items.Add(Log); listBox1.ScrollIntoView(listBox1.Items[listBox1.Items.Count - 1]); }));
        }

    }
}
