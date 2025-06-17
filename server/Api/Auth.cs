using Alaska.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Alaska.Models;
using Microsoft.Data.SqlClient;

namespace Alaska.Api
{
    internal static class Auth
    {
        public static void MapAuthentications(this WebApplication app)
        {
            app.MapPost("/auth/login", LoginAsync);
            app.MapPost("/auth/logout", LogoutAsync).RequireAuthorization();
            app.MapGet("/auth/profile", GetProfileAsync);
        }
        private static async Task<IResult> GetProfileAsync(HttpContext context, DbClient db)
        {
            int userId = AppHelpers.GetUserID(context);
            UserProfileInfo? user = null;
            string sql = @"SELECT u.id, u.name WHERE u.id=@id";
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        user = new UserProfileInfo()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)                            
                        };
                    }
                    await reader.CloseAsync();
                    await reader.DisposeAsync();
                }
            }, sql, new SqlParameter("@id", userId));
            return Results.Ok(user);
        }
        private static async Task<IResult> LoginAsync(LoginRequest? request, DbClient db)
        {
            if (request is null) return Results.Ok(LoginResponse.CreateFailed());

            string sql = @"SELECT [id], [name], [role]
FROM users
WHERE [login] = @username AND [password] = HASHBYTES('SHA2_256', CAST(@password AS NVARCHAR))";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@username", request.Username);
            parameters[1] = new SqlParameter("@password", request.Password);

            User? user = null;
            await db.ExecuteReaderAsync(async (SqlDataReader reader) =>
            {
                if (await reader.ReadAsync())
                {
                    user = new User()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        RoleId = reader.GetInt32(2)
                    };
                }
            }, sql, parameters);
            
            if (user != null)
            {
                var token = Guid.NewGuid();
                var insertParams = new SqlParameter[]
                {
                    new SqlParameter("@userID", user.Id),
                    new SqlParameter("@token", System.Data.SqlDbType.UniqueIdentifier) { Value = token },
                    new SqlParameter("@expired", DateTime.Now.AddHours(9))
                };
                await db.ExecuteNonQueryAsync("INSERT INTO authentications ([token], [user], [expired]) VALUES (@token, @userID, @expired)", insertParams);
                return Results.Ok(LoginResponse.CreateSuccess(token.ToString(), user));
            }
            return Results.Ok(LoginResponse.CreateFailed());
        }
        private static async Task<IResult> LogoutAsync(HttpContext context, DbClient db)
        {
            int iUserID = AppHelpers.GetUserID(context);
            if (iUserID > 0)
            {
                bool success = await db.ExecuteNonQueryAsync("DELETE FROM authentications WHERE [user] = @userID", new SqlParameter("@userID", iUserID));
                return Results.Ok(true);
            }
            return Results.Ok(false);
        }
    }

}
