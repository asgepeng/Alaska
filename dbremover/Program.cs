using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbremover
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string sql = File.ReadAllText(AppContext.BaseDirectory + "sql.txt");
            using (var conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=alaska;Integrated Security=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine("SQL succesfully executed");
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
