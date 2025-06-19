using Alaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My
{
    public static class Application
    {
        public static string ApiUrl { get; set; } = "http://localhost";
        public static string ApiToken { get; set; } = "";
        public static User? User { get; set; } = null;
    }
}