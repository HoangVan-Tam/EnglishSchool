using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Account
    {
        [Key]
        [MinLength(10)]
        public string userName { get; set; }
        [Required]
        [MinLength(8)]
        public string password { get; set; }
        [Required]
        public int role { get; set; }
        [Required]
        public string status { get; set; }
        public DateTime deactivationDate { get; set; }

        public Student students { get; set; }
    }
}
