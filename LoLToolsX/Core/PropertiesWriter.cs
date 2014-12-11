using System.IO;
using System.Text;

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
            try
            {
                using (StreamWriter writer = new StreamWriter(Variable.PropertiesFile, false, Encoding.UTF8))
                {
                    foreach (string str in Utility.GetPropertiesFile(this.server))
                    {
                        writer.WriteLine(str);
                    }
                }
                System.Windows.Forms.MessageBox.Show("lol.properties 修改成功!", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("lol.properties 修改失敗!", "錯誤", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
