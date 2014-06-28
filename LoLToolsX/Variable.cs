using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoLToolsX
{
    class Variable
    {
        //Path
        public static string v_installPath;
        public static string propPath;
        public static string hudPath;

        public static bool debug = false;
        public static bool haveUpdate = false;

        public static CFGFile lolConfig = new CFGFile(v_installPath);
    }
}
