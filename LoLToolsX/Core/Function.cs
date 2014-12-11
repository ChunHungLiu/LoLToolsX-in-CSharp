using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;


namespace LoLToolsX.Core
{
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

    class HotKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,

            int id,
            KeyModifiers fsModifiers,

            Keys vk

            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,

            int id

            );

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
    }
}
