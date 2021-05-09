﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class ChangePasswordDTO
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
    public class LoginDTO
    {
        public string userID { get; set; }
        public string password { get; set; }
    }
}
