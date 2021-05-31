using System.ComponentModel.DataAnnotations;

namespace EnglishSchool.Model.Models
{
    public class PersonalInformation
    {
        [Key]
        public string phoneNumber { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string status { get; set; }
        public string note { get; set; }
    }
}
