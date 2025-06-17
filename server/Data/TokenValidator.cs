using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Alaska.Data;
using Microsoft.Data.SqlClient;
using System.Data;

public class TokenValidator
{
    private readonly DbClient db;
    public TokenValidator(DbClient dbClient)
    {
        db = dbClient;
    }
    public async Task ValidateAsync(MessageReceivedContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Split(' ').Last();
        ClaimsPrincipal? principal = null;
        if (!string.IsNullOrEmpty(token))
        {
            var commandText = """
                SELECT a.[user] AS userId, u.[role] AS roleId
                FROM authentications AS a INNER JOIN users AS u ON a.[user] = u.id 
                WHERE a.token = @token
                """;
            var tokenParam = new SqlParameter("@token", SqlDbType.UniqueIdentifier);
            if (Guid.TryParse(token, out Guid tokenGuid))
            {
                tokenParam.Value = tokenGuid;
            }
            else
            {
                context.Fail("Invalid token format");
                return;
            }


            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, reader.GetInt32("userId").ToString()),
                        new Claim(ClaimTypes.Role, reader.GetInt32("roleId").ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "Bearer");
                    principal = new ClaimsPrincipal(identity);
                }
            }, commandText, tokenParam);
        }
        context.Principal = principal;
        if (principal != null)
        {
            context.Success();
        }
        else
        {
            context.Fail("Unauthorized request");
        }
    }
}
