using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace LoLToolsX.Core
{
    static class Utility
    {
        public static string[] GetPropertiesFile(Server _server)
        {
            switch (_server)
            {
                case Server.TW:
                    return new[] { 
                        "host=prodtw.lol.garenanow.com",
                        "xmpp_server_url=chattw.lol.garenanow.com",
                        "xmpp_accept_self_signed_cert=true",
                        "startChat=true",
                        "storeEnabled=true",
                        "enableTutorialGame=true",
                        "lobbyLandingURL=http://lobby.lol.tw/",
                        "ladderURL=http://lol.garena.tw/ladders",
                        "helpURL=http://lobby.lol.tw/",
                        "lq_uri=https://loginqueuetw.lol.garenanow.com",
                        "storyPageURL=http://lol.garena.tw/story/index.php?game_box=true",
                        "loadModuleChampionDetail=true",
                        "featuredGamesURL=http://112.121.84.194:8088/observer-mode/rest/featured",
                    };
                case Server.NA:
                    return new[] { 
                        "host=prod.na2.lol.riotgames.com",
                        "xmpp_server_url=chat.na2.lol.riotgames.com",
                        "ladderURL=http://www.leagueoflegends.com/ladders",
                        "storyPageURL=http://www.leagueoflegends.com/story",
                        "lq_uri=https://lq.na2.lol.riotgames.com",
                        "ekg_uri=https://ekg.riotgames.com",
                        "regionTag=na",
                        "rssStatusURLs=null",
                        "lobbyLandingURL=http://frontpage.na.leagueoflegends.com/$localeCode/client/landing",
                        "loadModuleChampionDetail=true",
                        "featuredGamesURL=http://spectator.na1.lol.riotgames.com:80/observer-mode/rest/featured",
                        "riotDataServiceDataSendProbability=1.0",
                        "platformId=na1",
                    };
                case Server.EUNE:
                    return new[] { 
                        "regionTag=eune",
                        "rssStatusURLs=null",
                        "host=prod.eun1.lol.riotgames.com",
                        "xmpp_server_url=chat.eun1.lol.riotgames.com",
                        "lq_uri=https://lq.eun1.lol.riotgames.com",
                        "lobbyLandingURL=http://landing.leagueoflegends.com/spectator_swf/landingPage.swf",
                        "loadModuleChampionDetail=true",
                        "featuredGamesURL=http://spectator.eu.lol.riotgames.com:8088/observer-mode/rest/featured",
                        "storyPageURL=http://www.leagueoflegends.com/story",
                        "ekg_uri=https://ekg.riotgames.com",
                        "riotDataServiceDataSendProbability=1.0",
                        "platformId=EUN1",
                    };
                case Server.EUW:
                    return new[] { 
                        "host=prod.eu.lol.riotgames.com",
                        "xmpp_server_url=chat.eu.lol.riotgames.com",
                        "xmpp_accept_self_signed_cert=true",
                        "startChat=true",
                        "storeEnabled=true",
                        "enableTutorialGame=true",
                        "lobbyLandingURL=http://landing.leagueoflegends.com/spectator_swf/landingPage.swf",
                        "lq_uri=https://lq.eu.lol.riotgames.com",
                        "storyPageURL=http://www.leagueoflegends.com/story",
                        "loadModuleChampionDetail=true",
                        "featuredGamesURL=http://spectator.eu.lol.riotgames.com:8088/observer-mode/rest/featured",
                    };
                case Server.KR:
                    return new[] { 
                        "host=prod.kr.lol.riotgames.com",
                        "xmpp_server_url=chat.kr.lol.riotgames.com",
                        "lq_uri=https://lq.kr.lol.riotgames.com",
                        "rssStatusURLs=null",
                        "regionTag=kr",
                        "lobbyLandingURL=http://www.leagueoflegends.co.kr/Launcher/launcher_main.php",
                        "featuredGamesURL=http://spectator.kr.lol.riotgames.com:80/observer-mode/rest/featured",
                        "storyPageURL=http://leagueoflegends.co.kr/Launcher/launcher_journal.php",
                        "ladderURL=http://www.leagueoflegends.co.kr",
                        "platformId=KR",
                        "ekg_uri=https://ekg.riotgames.com",
                        "riotDataServiceDataSendProbability=1.0",
                    };
                case Server.OCE:
                    return new[] { 
                        "host=prod.oc1.lol.riotgames.com",
                        "xmpp_server_url=chat.oc1.lol.riotgames.com",
                        "ladderURL=http://www.leagueoflegends.com/ladders",
                        "storyPageURL=http://www.leagueoflegends.com/story",
                        "lq_uri=https://lq.oc1.lol.riotgames.com ",
                        "ekg_uri=https://ekg.riotgames.com",
                        "rssStatusURLs=null",
                        "lobbyLandingURL=http://landing.leagueoflegends.com/spectator_swf/landingPage.swf",
                        "loadModuleChampionDetail=true",
                        "featuredGamesURL=http://spectator.oc1.lol.riotgames.com:80/observer-mode/rest/featured",
                        "riotDataServiceDataSendProbability=1.0",
                        "platformId=OC1",
                        "regionTag=oce",
                    };
                case Server.PBE:
                    return new[] { 
                        "host=prod.pbe1.lol.riotgames.com",
                        "xmpp_server_url=chat.pbe1.lol.riotgames.com",
                        "xmpp_accept_self_signed_cert=true",
                        "lobbyLandingURL=http://d2q6fdmnncz9b0.cloudfront.net/landingPage.swf",
                        "ladderURL=http://www.leagueoflegends.com/ladders",
                        "storyPageURL=http://www.leagueoflegends.com/story",
                        "lq_uri=https://lq.pbe1.lol.riotgames.com/login-queue/rest/queue",
                        "featuredGamesURL=http://spectator.pbe1.lol.riotgames.com:8088/observer-mode/rest/featured",
                        "riotDataServiceDataSendProbability=1.0",
                        "ekg_uri=https://ekg.riotgames.com ",
                    };
                case Server.SEA:
                    return new[] { 
                        "host=prod.lol.garenanow.com",
                        "xmpp_server_url=chat.lol.garenanow.com",
                        "xmpp_accept_self_signed_cert=true",
                        "startChat=true",
                        "storeEnabled=true",
                        "enableTutorialGame=true",
                        "lobbyLandingURL=http://lol.garena.com/landing.php",
                        "ladderURL=http://lol.garena.com/ladders",
                        "helpURL=http://lol.garena.com/support",
                        "lq_uri=https://lq.lol.garenanow.com",
                        "useOldChatRenderers=true",
                    };
                default:
                    return null;
            }
        }

        public static string GetLoLVer()
        {
            //取得LoL版本
            using (StreamReader reader = new StreamReader(Variable.installPath + @"\lol.version", Encoding.UTF8))
            {
                try
                {
                    string airVer = reader.ReadLine();
                    Logger.log("Air版本: " + airVer, Logger.LogType.Info);
                    return airVer;
                }
                catch (Exception e)
                {
                    MessageBox.Show("無法取得LoL版本", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.log("無法取得Air版本: ", Logger.LogType.Error);
                    Logger.log(e);
                    return "未知";
                }
            }
        }

        public static bool UploadLogs()
        {
            Logger.log("關閉程式...", Logger.LogType.Info);
            Random random = new Random();
            string rd = random.Next().ToString();
            string rdFile = Variable.CurrentDirectory + @"\Logs\Log" + rd + ".txt";
            File.Copy(Variable.CurrentDirectory + @"\Logs\Log.txt", rdFile);

            try
            {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.Headers.Add("Content-Type", "binary/octet-stream");
                Client.UploadFile("http://nitroxenon.com/loltoolsx/xerror.php", "POST", rdFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string PingCheck(string ip)
        {
            Ping ping = new Ping();
            try
            {
                int value = (int)ping.Send(ip).RoundtripTime;
                string final = value + " ms";
                //if (value == 0)
                //return "請求逾時";
                if (value < 5)
                {
                    return "不明";
                }
                else
                    return "正常 :" + final;
            }
            catch
            {
                return "請求逾時";
            }
        }
    }

    public enum Server
    {
        TW,
        SEA,
        NA,
        KR,
        EUW,
        EUNE,
        OCE,
        PBE,
    }
}
