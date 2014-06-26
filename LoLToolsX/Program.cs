using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LoLToolsX
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerSelect());
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)  //CrushHandler
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                Logger.log(string.Format("程式發生未處理的錯誤！\n\n{0}{1}", ex.Message, ex.StackTrace), Logger.LogType.Crash);
                MessageBox.Show(string.Format("程式發生未處理的錯誤, 按確定關閉程式！\n\n{0}{1}", ex.Message, ex.StackTrace), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
