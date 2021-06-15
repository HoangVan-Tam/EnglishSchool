using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("Employee")]
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
        [ForeignKey("departments")]
        public int departmentId { get; set; }
        public Department departments { get; set; }

    }
}
