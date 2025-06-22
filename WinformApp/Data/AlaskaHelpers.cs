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

    public static class FormatHelpers
    {
        public static string LPAD(string value, int maxlength, char fillwith = ' ')
        {
            value = value.Trim();
            if (value.Length >= maxlength)
            {
                return value.Substring(0, maxlength);
            }

            string pad = "";
            int clength = maxlength - value.Length;
            for (int i = 0; i < clength; i++)
            {
                pad += fillwith;
            }
            return string.Concat(pad, value);
        }
        public static string RPAD(string value, int maxlength)
        {
            value = value.Trim();
            if (value.Length >= maxlength)
            {
                return value.Substring(0, maxlength);
            }

            string pad = "";
            int clength = maxlength - value.Length;
            for (int i = 0; i < clength; i++)
            {
                pad += " ";
            }
            return string.Concat(value, pad);
        }
        public static void FilterOnlyNumber(KeyPressEventArgs e)
        {
            var code = Convert.ToInt32(e.KeyChar);
            if (!(code >= 48 && code <= 57) && code != 8)
            {
                e.Handled = true;
            }
        }
        public static decimal GetDecimal(string value)
        {
            return decimal.TryParse(value, out decimal outValue) ? outValue : 0;
        }
        public static void FilterOnlyAlphaNumericValue(KeyPressEventArgs e)
        {
            var code = Convert.ToInt32(e.KeyChar);
            if (!(code >= 65 && code <= 90) && !(code >= 97 && code <= 122) && code != 8 && code != 32 && !(code >= 48 && code <= 57))
            {
                e.Handled = true;
            }
        }
        public static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static void ConvertToNumber(TextBox tb)
        {
            tb.Text = tb.Text.Replace(",", "").Replace(".", "");
        }
        public static decimal RollBackToDecimal(TextBox tb)
        {
            var dec = decimal.TryParse(tb.Text, out decimal value) ? value : 0;
            tb.Text = dec.ToString("N0");
            return dec;
        }
    }
}

