using System;
using System.Globalization;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Collections;
using System.Windows.Forms;

namespace LoLToolsX.Core
{
    public static class Logger
    {

        /// <summary>
        /// Logger
        /// </summary>

        public enum LogType
        {
            Error, Info, Crash, Exception, Game, Fml,
        }

        public static string file;


        public static void start()
        {
            if (!Directory.Exists(Application.StartupPath + @"\Logs"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Logs");
                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Logs");
                di.Refresh();
            }
                GC.Collect();
                file = System.IO.Path.Combine(Application.StartupPath + @"\Logs\" + "Log.txt");
                if (!File.Exists(Application.StartupPath + @"\Logs\" + "Log.txt"))
                    File.Create(Application.StartupPath + @"\Logs\" + "Log.txt");
                GC.Collect();
                FileStream fs = new FileStream(Application.StartupPath + @"\Logs\" + "Log.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("============================== LoLToolsX Log File ==============================");
                sw.Close();
            }
        
        private static string writeInfo(LogType type = LogType.Info)
        {
            switch (type)
            {
                case LogType.Error:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "錯誤: ");
                case LogType.Info:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息: ");
                case LogType.Crash:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "崩潰: ");
                case LogType.Exception:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "異常: ");
                case LogType.Game:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "遊戲: ");
                case LogType.Fml:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "FML : ");
                default:
                    return (DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息: ");
            }
        }

        //寫入記錄檔
        private static void write(string str, LogType type = LogType.Info)
        {
            GC.Collect();   //釋放資源 以免不能存取
            FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(writeInfo(type) + str);
            sw.Close();
            fs.Close();
        }

        static public void log(string str, LogType type = LogType.Info)
        {
            write(str, type);
        }
        
        // Log Exception
        static public void log(Exception ex, LogType type = LogType.Exception)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(ex.Source);
            message.AppendLine(ex.ToString());
            message.AppendLine(ex.Message);
            foreach (DictionaryEntry data in ex.Data)
            message.AppendLine(string.Format("Key:{0}\nValue:{1}", data.Key, data.Value));
            message.AppendLine(ex.StackTrace);
            var iex = ex;
            while (iex.InnerException != null)
            {
                message.AppendLine("------------------------");
                iex = iex.InnerException;
                message.AppendLine(iex.Source);
                message.AppendLine(iex.ToString());
                message.AppendLine(iex.Message);
                foreach (DictionaryEntry data in ex.Data)
                message.AppendLine(string.Format("Key:{0}\nValue:{1}", data.Key, data.Value));
                message.AppendLine(iex.StackTrace);
            }
            write(message.ToString(), type);
        }

        static public void info(string message)
        {
            Logger.log(message);
        }

        static public void error(string message)
        {
            Logger.log(message, LogType.Error);
        }

        static public void error(Exception ex)
        {
            Logger.log(ex);
        }
    }
}
