using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

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
        public static async Task<string> GetPublicIpAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Mengambil IP publik dalam bentuk teks dari ipify.org
                    var ip = await httpClient.GetStringAsync("https://api.ipify.org");
                    return ip;
                }
            }
            catch
            {
                return "";
            }
        }
        public static async Task<string?> StartCloudflareTunnelAsync()
        {
            var regex = new Regex(@"https://[a-zA-Z0-9\-]+\.trycloudflare\.com");

            var psi = new ProcessStartInfo
            {
                FileName = "cloudflared",
                Arguments = "tunnel --url http://localhost:5005 --no-autoupdate",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = psi };
            process.Start();

            string? url = null;

            // Gabungkan stdout dan stderr
            var reader = process.StandardOutput;
            var errorReader = process.StandardError;

            async Task CheckStreamAsync(System.IO.StreamReader stream)
            {
                string? line;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    // Coba cari URL di baris ini
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        url = match.Value;
                        break;
                    }
                }
            }
            

            // Baca stdout & stderr secara paralel, tapi tunggu paling lama 10 detik
            var outputTask = CheckStreamAsync(reader);
            var errorTask = CheckStreamAsync(errorReader);
            await Task.WhenAny(Task.Delay(10000), outputTask, errorTask);

            return url;
        }
    }    
}

