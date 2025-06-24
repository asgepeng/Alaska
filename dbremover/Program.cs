using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbremover
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "sc.exe",
                Arguments = "stop AlaskaServer",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "sc.exe",
                Arguments = "delete AlaskaServer",
                UseShellExecute = false,
                CreateNoWindow = true
            });
            using (var conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=master;Integrated Security=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("DROP DATABASE alaska", conn);
                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("Database alaska berhasil dihapus");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            Console.ReadLine();
        }
    }
}
