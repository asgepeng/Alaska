using System.Net;
using System.Net.Sockets;

namespace WinformApp
{
    public static class NetworkHelper
    {
        public static string GetIPV4Address()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) 
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1"; 
        }
    }
}

