using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace LoLToolsX
{
    public static class StatusCheck
    {
        public static bool pingCheck(string ip)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip);
            if (pingReply.Status == IPStatus.TimedOut)
                return false;
            else
                return true;
        }

    }
}
