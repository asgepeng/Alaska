using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;

namespace Alaska.Data
{
    public class DbClient
    {
        private string connString;
        public DbClient()
        {
            this.connString = "Server=.\\SQLEXPRESS;Database=alaska;Integrated Security=True;TrustServerCertificate=True";
        }
        public DbClient(string connectionString)
        {
            this.connString = connectionString;
        }
        private (SqlConnection conn, SqlCommand cmd) CreateConnection(string commandText, params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(connString);
            var cmd = new SqlCommand(commandText, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            return (conn, cmd);
        }
        public async Task<int> ExecuteScalarIntegerAsync(string commandText, params SqlParameter[] parameters)
        {
            using SqlConnection conn = new SqlConnection(this.connString);
            using SqlCommand cmd = new SqlCommand(commandText, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            try
            {
                await conn.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    await conn.CloseAsync();
                }
                await conn.DisposeAsync();
            }
        }
        public async Task<object?> ExecuteScalarAsync(string commandText, params SqlParameter[] parameters)
        {
            object? value = null;
            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    try
                    {
                        await conn.OpenAsync();
                        value = await cmd.ExecuteScalarAsync();
                    }
                    catch (Exception ex)
                    {
                        System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\sql_error.txt", ex.Message);
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed)
                        {
                            await conn.CloseAsync();
                        }
                    }
                }
            }
            return value;
        }
        public async Task<bool> AnyRecords(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public async Task<bool> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    System.IO.File.WriteAllText(AppContext.BaseDirectory + "\\sql_error_non_query.txt", DateTime.Now + "\n" + ex.ToString());
                    return false;
                }
                finally
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
        }

        public async Task ExecuteReaderAsync(Action<SqlDataReader> handler, string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn)
            using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        handler(reader);
                    }
                }
                catch
                { }
                finally
                {
                    await conn.CloseAsync();
                }
            }
        }
        public string SQLString(string value)
        {
            return value.Replace("'", "''");
        }
        public string SQLDateTime(DateTime value)
        {
            return value.ToString("dd-MM-yyyy hh:mm:ss:fffff");
        }
        public string SQLDate(DateTime value)
        {
            return value.ToString("dd-MM-yyyy");
        }
    }

    public class ExecuteResult
    {
        private ExecuteResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; }
        public string Error { get; }
        public int Data { get; set; } = -1;
        public static ExecuteResult CreateSuccess()
        {
            return new ExecuteResult(true, string.Empty);
        }
        public static ExecuteResult CreateFailed(string error)
        {
            return new ExecuteResult(false, error);
        }
    }

    internal class DbInitializer
    {
        private readonly string connstring = "server=localhost;database=information_schema;uid=root;pwd=d024c5b1-fa9f-492a-a345-03b1995340b9";
        private readonly DbClient db;
        internal DbInitializer()
        {
            this.db = new DbClient(this.connstring);
        }
        internal async Task CreateDatabase()
        {
            bool exists = false;
            exists = await db.AnyRecords("SELECT 1 FROM information_schema.schemata WHERE SCHEMA_NAME = 'alaska'");
            if (!exists)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("CREATE DATABASE posdb;");
                sb.AppendLine("CREATE USER 'astroboy'@'%' IDENTIFIED BY 'Orogin@k-66171';");
                sb.AppendLine("GRANT ALL PRIVILEGES ON posdb.*TO 'astroboy'@'%';");
                sb.AppendLine("FLUSH PRIVILEGED;");
                sb.AppendLine("USE  posdb;");
                sb.AppendLine("CREATE TABLE `users` (id INT NOT NULL AUTO_INCREMENT, `username` VARCHAR(100) NOT NULL DEFAULT '', `email` VARCHAR(50) NOT NULL DEFAULT '', CONSTRAINT pk_users PRIMARY KEY CLUSTERED (`id` ASC));");
                sb.AppendLine("CREATE TABLE `logins` (id INT NOT NULL AUTO_INCREMENT, `name` VARCHAR(50) NOT NULL DEFAULT '', `password` BINARY(32) NOT NULL, CONSTRAINT pk_logins PRIMARY KEY CLUSTERED (id ASC);");

                await db.ExecuteNonQueryAsync(sb.ToString());
            }
        }
    }
}
