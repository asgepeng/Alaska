using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

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
}
