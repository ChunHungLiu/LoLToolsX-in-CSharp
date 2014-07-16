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
            if (!File.Exists(installPath + @"\Air\lol.properties"))
            {
                MessageBox.Show("無法存取伺服器設定檔\r\n按'確定'關閉程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("無法存取伺服器設定檔", Logger.LogType.Error);
                Logger.log("強制關閉程式... Exit Code: " + Environment.ExitCode, Logger.LogType.Error);
                
                Environment.Exit(Environment.ExitCode);
            }

            else
            {
                Variable.propPath = installPath + @"\Air\lol.properties";
            }
   


            try
            {

                FileStream fs = new FileStream(Variable.propPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

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

                Logger.log("伺服器設定檔檢查成功! ", Logger.LogType.Info);
                Logger.log("目前伺服器: " + currentLoc , Logger.LogType.Info);
                return currentLoc;
            }

            catch (Exception e)
            {
                Logger.log("伺服器設定檔檢查失敗", Logger.LogType.Error);
                Logger.log(e,Logger.LogType.Exception);
                GC.Collect();
                return "";
            }

            finally
            {
                GC.Collect();
            }

            

        }

        public string CheckPropNa(string installPath)
        {
            if (!File.Exists(installPath + @"\lol.properties"))
            {
                MessageBox.Show("無法存取伺服器設定檔\r\n按'確定'關閉程式", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.log("無法存取伺服器設定檔", Logger.LogType.Error);
                Logger.log("強制關閉程式... Exit Code: " + Environment.ExitCode, Logger.LogType.Error);

                Environment.Exit(Environment.ExitCode);
            }

            else
            {
                Variable.propPath = installPath + @"\lol.properties";
            }



            try
            {

                FileStream fs = new FileStream(Variable.propPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

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

                Logger.log("伺服器設定檔檢查成功! ", Logger.LogType.Info);
                Logger.log("目前伺服器: " + currentLoc, Logger.LogType.Info);
                return currentLoc;
            }

            catch (Exception e)
            {
                Logger.log("伺服器設定檔檢查失敗", Logger.LogType.Error);
                Logger.log(e, Logger.LogType.Exception);
                GC.Collect();
                return "";
            }

            finally
            {
                GC.Collect();
            }



        }
    }
}
