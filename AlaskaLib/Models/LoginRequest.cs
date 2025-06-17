using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        private LoginResponse(bool isSuccess, string token  = "", User? user = null)
        {
            this.Success = isSuccess;
            this.ApiToken = token;
            this.User = user;
        }
        public LoginResponse() { }
        [JsonPropertyName("success")] public bool Success { get; set; } = false;
        [JsonPropertyName("apiToken")] public string ApiToken { get; set; } = "";
        [JsonPropertyName("user")] public User? User { get; set; } = null;
        public static LoginResponse CreateSuccess(string token, User user)
        {
            return new LoginResponse(true, token, user);
        }
        public static LoginResponse CreateFailed()
        {
            return new LoginResponse(false);
        }
    }
}
