using System.Runtime.InteropServices;
using System.Text;

namespace LoLToolsX.Core
{
    class CFGFile
    {
        public string ConfigPath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public CFGFile(string FilePath)
        {
            ConfigPath = FilePath;
        }
        public string GetValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", temp, 255, ConfigPath);
            return temp.ToString();
        }
        public void SetValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, ConfigPath);
        }
    }
}