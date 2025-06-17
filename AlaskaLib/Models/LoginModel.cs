using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class LoginModel
    {
        public int id { get; set; } = 0;
        public int userId { get; set; } = 0;
        public string userName { get; set; } = "";
        public string password { get; set; } = "";
        public List<Employee> employees { get; set; }= new List<Employee>();
    }
}
