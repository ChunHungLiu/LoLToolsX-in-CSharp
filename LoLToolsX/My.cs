using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;

namespace LoLToolsX
{
    /// <summary>
    /// 定義 My 物件
    /// </summary>
    public static class My
    {
        static private Computer __Computer = new Computer();
        static private WindowsFormsApplicationBase __Application = new WindowsFormsApplicationBase();
        static private User __User = new User();

        public static ServerComputer Computer
        {
            get
            {
                return __Computer;
            }
        }

        public static WindowsFormsApplicationBase Application
        {
            get
            {
                return __Application;
            }
        }

        public static User User
        {
            get
            {
                return __User;
            }
        }
    }
}
