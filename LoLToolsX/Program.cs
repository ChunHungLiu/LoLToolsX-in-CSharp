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
        static void Main(string[] args)  //加入 string[] args 來接受參數
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //如果參數是 "Debug" 則Debug模式開啟
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "Debug":
                        Debug.debug = true;
                        break;
                }
            }
            Application.Run(new ServerSelect());

        }
    }
}
