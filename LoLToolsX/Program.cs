using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using LoLToolsX.Core;

namespace LoLToolsX
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);   //If crush, call CrushHandler

            //只允許單一執行個體
            SingelApp myapp = new SingelApp();
            myapp.Run(args);

            if (args.Length > 0)
            {
                if (args[0] == "-M")
                {
                    //強制手動選擇路徑
                    Variable.forceSelectPath = true;
                }
            }
            //清理下載資料夾
            foreach (string s in Directory.GetFiles(Variable.CurrentDirectory + "\\download"))
            {
                File.Delete(s);
            }
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)  //CrushHandler
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                CrushForm cf = new CrushForm(ex.Message,ex.StackTrace);
                cf.ShowDialog();
                Logger.log(string.Format("程式發生未處理的錯誤！\n\n{0}{1}", ex.Message, ex.StackTrace), Logger.LogType.Crash);
            }
            finally
            {
                Logger.log("關閉程式...", Logger.LogType.Info);
                Application.Exit();
            }
        }
    }
    /// <summary>
    /// 單一執行個體。
    /// </summary>

    internal class SingelApp : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        public SingelApp()
        {
            this.IsSingleInstance = true;
            this.EnableVisualStyles = true;
            this.ShutdownStyle = Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses;
        }

        protected override void OnCreateMainForm()
        {
            this.MainForm = new FileCheck();
        }
    }
}