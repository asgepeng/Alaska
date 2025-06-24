using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Alaska.Data
{
    internal static class AppHelpers
    {
        internal static int GetUserID(HttpContext context)
        {
            var claimsPrincipal = context.User;
            string? userID = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userID, out int id);
            return id;
        }
        internal static (int userId, int roleId) ExtractPrincipal(HttpContext context)
        {
            var claimsPrincipal = context.User;
            string? userID = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string? roleID = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            int.TryParse(userID, out int iUserID);
            int.TryParse(roleID, out int iRoleID);
            return (iUserID, iRoleID);
        }
        internal static async Task<(int, int)> GetDepartmentID(HttpContext context, DbClient db)
        {
            int userId = AppHelpers.GetUserID(context);
            string sql = "SELECT department FROM users WHERE id = @id";
            int departmentId = await db.ExecuteScalarIntegerAsync(sql, new SqlParameter("@id", userId));
            return (userId, departmentId);
        }
        internal static async Task<string> GenerateTransactionID(string prefix, DbClient db)
        {
            int.TryParse(DateTime.Today.ToString("yyMMdd"), out int id);
            string sql = "INSERT INTO `autonumbers` (id, `" +
                prefix + "`) VALUES (@id, 1) ON DUPLICATE KEY UPDATE `" +
                prefix + "` = `" + prefix + "` + 1; SELECT `" + prefix + "` FROM `autonumbers` " +
                "WHERE id = @id;";
            int sequnce = await db.ExecuteScalarIntegerAsync(sql, new SqlParameter("@id", id));
            return sequnce > 0 ? string.Concat(prefix, id.ToString(), sequnce.ToString("0000")) : string.Empty;
        }
    }

    public static class ExcelHelper
    {
        public static string GetCellValue(Cell cell, SharedStringTablePart stringTable)
        {
            if (cell == null || cell.CellValue == null)
                return "";

            string value = cell.CellValue.InnerText;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString && stringTable != null)
            {
                return stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
            }

            return value;
        }

        public static string ToDecimalOrZero(string input)
        {
            return decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? result.ToString(CultureInfo.InvariantCulture)
                : "0";
        }
        public static void AddStyles(WorkbookPart workbookPart)
        {
            var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = new Stylesheet
            {
                Fonts = new Fonts(
                    new Font(),
                    new Font(new Bold())
                ),
                Fills = new Fills(
                    new Fill(new PatternFill { PatternType = PatternValues.None }),
                    new Fill(new PatternFill { PatternType = PatternValues.Gray125 }),
                    new Fill(new PatternFill(new ForegroundColor { Rgb = HexBinaryValue.FromString("F0F8FF") }) { PatternType = PatternValues.Solid }),
                    new Fill(new PatternFill(new ForegroundColor { Rgb = HexBinaryValue.FromString("C0C0C0") }) { PatternType = PatternValues.Solid })
                ),
                Borders = new Borders(new Border()),
                CellFormats = new CellFormats(
                    new CellFormat(),
                    new CellFormat { FontId = 1, ApplyFont = true },
                    new CellFormat { FillId = 2, FontId = 1, ApplyFill = true, ApplyFont = true },
                    new CellFormat { FillId = 3, FontId = 1, ApplyFill = true, ApplyFont = true },
                    new CellFormat { Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Right }, ApplyAlignment = true },
                    new CellFormat { Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Right }, FontId = 1, ApplyAlignment = true, ApplyFont = true },
                    new CellFormat { FillId = 2, FontId = 1, Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Right }, ApplyAlignment = true, ApplyFont = true, ApplyFill = true },
                    new CellFormat { FillId = 3, FontId = 1, Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Right }, ApplyAlignment = true, ApplyFont = true, ApplyFill = true }
                )
            };
            stylesPart.Stylesheet.Save();
        }

        public static Row CreateRow(uint index, params (string text, uint style)[] cells)
        {
            var row = new Row { RowIndex = index };
            foreach (var (text, style) in cells)
                row.Append(CreateCell(text, style));
            return row;
        }

        public static Row CreateSubtotalRow(string subtotalName, uint index, double income, double expense, double balance)
        {
            return CreateRow(index,
                            (subtotalName, 3, CellValues.String),
                            ("", 3, CellValues.String),
                            ("", 3, CellValues.String),
                            (income, 7, CellValues.Number),
                            (expense, 7, CellValues.Number),
                            (balance, 7, CellValues.Number),
                            ("", 3, CellValues.String));
        }

        private static Cell CreateCell(string value, uint styleIndex)
        {
            return new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value ?? string.Empty),
                StyleIndex = styleIndex
            };
        }
        public static Row CreateRow(uint rowIndex, params (object? value, int styleIndex, CellValues type)[] values)
        {
            var row = new Row { RowIndex = rowIndex };

            foreach (var (value, styleIndex, type) in values)
            {
                string cellText = value switch
                {
                    null when type == CellValues.Number => "0",
                    null => string.Empty,
                    _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty
                };

                var cell = new Cell
                {
                    StyleIndex = (uint)styleIndex,
                    DataType = new EnumValue<CellValues>(type),
                    CellValue = new CellValue(cellText)
                };

                row.Append(cell);
            }

            return row;
        }
    }
}
