using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LoLToolsX
{
    /// <summary>
    /// 檢查 lol.properties
    /// </summary>

    class CheckProp
    {
        public string currentLoc;

        public string CheckPropFL(string installPath)
        {

            Path.propPath = installPath + @"\Air\lol.properties";
            FileStream fs = new FileStream(Path.propPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            try
            {
                
                    
                    string cs = sr.ReadToEnd();

                    //////////////////////////////////////

                    if (cs.Contains("host=prodtw.lol.garenanow.com"))
                    {
                        currentLoc = "台服";
                    }
                    else if (cs.Contains("host=prod.lol.garenanow.com"))
                    {
                        currentLoc = "新馬服(SEA)";
                    }
                    else if (cs.Contains("rod.pbe1.lol.riotgames.com"))
                    {
                        currentLoc = "PBE服";
                    }
                    else if (cs.Contains("prod.oc1.lol.riotgames.com"))
                    {
                        currentLoc = "大洋洲服";
                    }
                    else if (cs.Contains("host=prod.na1.lol.riotgames.com"))
                    {
                        currentLoc = "美服";
                    }
                    else if (cs.Contains("host=prod.kr.lol.riotgames.com"))
                    {
                        currentLoc = "韓服";
                    }
                    else if (cs.Contains("host=prod.eun1.lol.riotgames.com"))
                    {
                        currentLoc = "EUNE服";
                    }
                    else if (cs.Contains("host=prod.eu.lol.riotgames.com"))
                    {
                        currentLoc = "EUW服";
                    }
                    else
                    {
                        currentLoc = "未知";
                    }
                
                
                return currentLoc;
            }

            catch (Exception e)
            {
                return currentLoc;
            }

            finally
            {
                sr.Close();
                fs.Close();
            }

            

        }
    }
}
