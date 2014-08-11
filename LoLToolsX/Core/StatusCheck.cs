using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace LoLToolsX.Core
{
    public static class StatusCheck
    {
        //Old
        /*
        public static bool pingCheck(string ip)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip);
            if (pingReply.Status == IPStatus.Success)
                return true;
            else if (pingReply.Status == IPStatus.TimedOut)
                return false;
            else
                return false;
        }
         * */
        public static string pingCheck(string ip)
        {
            Ping ping = new Ping();
            try
            {
                int value = (int)ping.Send(ip).RoundtripTime;
                string final = value  + " ms";
                //if (value == 0)
                    //return "請求逾時";
                if (value < 5)
                {
                    return "不明";
                }
                else
                    return "正常 :" +  final;
            }
            catch
            {
                return "請求逾時";
            }
        }

    }
}
