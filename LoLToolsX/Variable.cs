﻿namespace LoLToolsX
{
    struct Variable
    {
        #region 屬性
        public static string PropertiesFile
        {
            get
            {
                return Variable.installPath + "\\Air\\lol.properties";
            }
        }

        public static string UiPath
        {
            get
            {
                return Variable.installPath + "\\Game\\DATA\\Menu\\Textures\\HUDAtlas.dds";
            }
        }

        public static string CurrentDirectory
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath;
            }
        }
        #endregion

        #region 字串

        //public static string tw_installPath;  //台服LOL路徑
        //public static string na_installPath;  //美服LOL路徑
        public static string installPath;
        public static string propPath;
        public static string hudPath;
        public static string n_installPath;
        public static string airPath;   //美服Air路徑
        public static string curClient;

        #endregion

        #region 版本(美服)

        public static string airVer = "";
        public static string gameVer = "";

        #endregion

        #region 布林值

        public static bool haveUpdate = false;
        public static bool switchingSound = false;
        public static bool updating = false;
        public static bool InstallSkinDone = false;
        public static bool allowBakRes = true;
        public static bool allowUpdate = true;
        public static bool editpropMessageBox = true;
        public static bool forceSelectPath = false;
        public static bool frmShown = false;

        #endregion

        #region 物件

        //public static CFGFile lolConfig = new CFGFile(tw_installPath);

        #endregion

        #region 伺服器名稱 ---> lol.properties檔案名稱
        /*
        

        private static string _curLoc;

        public static string curLoc
        {
            get
            {
                switch (_curLoc)
                {
                    case "台服":
                        _curLoc = "lolt.properties";
                        break;
                    case "美服" :
                        _curLoc = "loln.properties";
                        break;
                    case "SEA服":
                        _curLoc = "lols.properties";
                        break;
                    case "大洋洲服":
                        _curLoc = "loloce.properties";
                        break;
                    case "韓服":
                        _curLoc = "lolk.properties";
                        break;
                    case "EUW服":
                        _curLoc = "lole.properties";
                        break;
                    case "EUNE服":
                        _curLoc = "loleune.properties";
                        break;
                    case "PBE服":
                        _curLoc = "lolp.properties";
                        break;
                }
                return _curLoc;
            }
            set
            {
                _curLoc = value;
            }
        }

         */
        #endregion
    }
}
