using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class EmployeeDTO
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
    public class EmployeeLoginDTO
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
