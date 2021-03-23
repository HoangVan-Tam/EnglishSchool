using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class AccountDTO
    {
        public string userName { get; set; }
        public int role { get; set; }
        public string status { get; set; }
        public string token { get; set; }
    }
    public class AccountChangePasswordDTO
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
