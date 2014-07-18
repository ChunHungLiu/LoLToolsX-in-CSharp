using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLToolsX
{
    struct Variable
    {
        //Path
        public static string v_installPath;
        public static string propPath;
        public static string hudPath;
        public static string n_installPath;

        //Na Ver
        public static string airVer = "";
        public static string gameVer = "";

        //bool
        public static bool debug = false;
        public static bool haveUpdate = false;
        public static bool switchingSound = false;
        public static bool updating = false;
        public static bool InstallSkinDone = false;
        public static bool allowBakRes = true;
        public static bool allowUpdate = true;

        public static CFGFile lolConfig = new CFGFile(v_installPath);

    }
}
