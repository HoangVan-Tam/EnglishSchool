using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Employee
    {
        [Key]
        public string userId { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime birthday { get; set; }
        [Required]
        public bool sex { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public string role { get; set; }
    }
}
