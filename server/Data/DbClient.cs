using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Data.Common;

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
        }
        public async Task<byte[]> ExportToExcelAsync(string commandText, params SqlParameter[] parameters)
        {
            var filename = AppContext.BaseDirectory + DateTime.Now.Ticks.ToString();
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        using (SpreadsheetDocument document = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook))
                        {
                            WorkbookPart workbookPart = document.AddWorkbookPart();
                            workbookPart.Workbook = new Workbook();

                            // Tambahkan stylesheet untuk format tanggal
                            WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                            stylesPart.Stylesheet = GenerateStylesheet();
                            stylesPart.Stylesheet.Save();

                            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                            SheetData sheetData = new SheetData();
                            worksheetPart.Worksheet = new Worksheet(sheetData);

                            if (document.WorkbookPart is null) throw new ArgumentException("document.WorkbookPar is null");
                            Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                            Sheet sheet = new Sheet()
                            {
                                Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                                SheetId = 1,
                                Name = "Data"
                            };
                            sheets.Append(sheet);

                            // Header row
                            Row headerRow = new Row();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                Cell cell = new Cell()
                                {
                                    DataType = CellValues.String,
                                    CellValue = new CellValue(columnName)
                                };
                                headerRow.AppendChild(cell);
                            }
                            sheetData.AppendChild(headerRow);

                            // Data rows
                            while (reader.Read())
                            {
                                Row newRow = new Row();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Type type = reader.GetFieldType(i);
                                    Cell cell = new Cell();
                                    object? value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                                    if (value is null)
                                    {
                                        type = typeof(string);
                                        value = "";
                                    }

                                    if (type == typeof(DateTime) && value != DBNull.Value)
                                    {
                                        double oaDate = ((DateTime)value).ToOADate();
                                        cell.CellValue = new CellValue(oaDate.ToString(CultureInfo.InvariantCulture));
                                        cell.DataType = CellValues.Number;
                                        cell.StyleIndex = 1; // Gaya tanggal
                                    }
                                    else if (type == typeof(decimal))
                                    {
                                        cell.CellValue = new CellValue(((decimal)value).ToString(CultureInfo.InvariantCulture));
                                        cell.DataType = CellValues.Number;
                                    }
                                    else if (type == typeof(int))
                                    {
                                        cell.CellValue = new CellValue(((int)value).ToString());
                                        cell.DataType = CellValues.Number;
                                    }
                                    else if (type == typeof(long))
                                    {
                                        cell.CellValue = new CellValue(((long)value).ToString());
                                        cell.DataType = CellValues.Number;
                                    }
                                    else
                                    {
                                        cell.CellValue = new CellValue(value?.ToString() ?? "");
                                        cell.DataType = CellValues.String;
                                    }

                                    newRow.AppendChild(cell);
                                }
                                sheetData.AppendChild(newRow);
                            }

                            workbookPart.Workbook.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
                if (File.Exists(filename))
                {
                    return File.ReadAllBytes(filename);
                }
                else
                {
                    return Array.Empty<byte>();
                }
            }
        }
        private Stylesheet GenerateStylesheet()
        {
            return new Stylesheet(
                new NumberingFormats(
                    new NumberingFormat()
                    {
                        NumberFormatId = 164, // Custom formats must be >= 164
                        FormatCode = "yyyy-mm-dd HH:Mm:ss"
                    }
                ),
                new Fonts(new Font()),
                new Fills(new Fill()),
                new Borders(new Border()),
                new CellFormats(
                    new CellFormat(), // default
                    new CellFormat()
                    {
                        NumberFormatId = 164,
                        ApplyNumberFormat = true
                    }
                )
            );
        }
        private List<(string columnName, Type dataType, bool isSum)> GetColumns(DbDataReader reader, string[] sumColumns)
        {
            var list = new List<(string, Type, bool)>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);
                var isSum = sumColumns != null && sumColumns.Contains(columnName);
                list.Add((reader.GetName(i), reader.GetFieldType(i), isSum));
            }
            return list;
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

    internal static class DbInitializer
    {
        internal static async void InitializeDatabase(this WebApplication app)
        {
            string connstring = "Server=.\\SQLEXPRESS;Database=master;Integrated Security=True;TrustServerCertificate=True";
            DbClient db = new DbClient(connstring);
            bool exists = false;
            exists = await db.AnyRecords("SELECT 1 FROM sys.databases WHERE [name] = 'alaska'");
            if (!exists)
            {
                await db.ExecuteNonQueryAsync("CREATE DATABASE alaska");
                db = new DbClient("Server=.\\SQLEXPRESS;Database=alaska;Integrated Security=True;TrustServerCertificate=True");
                await db.ExecuteNonQueryAsync(global::server.Properties.Resources.sql_ddl);
            }
        }
    }
}
