using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LoLToolsX.Core
{
    class PropertiesWriter
    {
        Server server;

        public PropertiesWriter(Server _server)
        {
            this.server = _server;
        }

        public void Write()
        {
            using (StreamWriter writer = new StreamWriter(Variable.PropertiesFile,false,Encoding.UTF8))
            {
                foreach (string str in Utility.GetPropertiesFile(this.server))
                {
                    writer.WriteLine(str);
                }
            }
        }
    }
}
