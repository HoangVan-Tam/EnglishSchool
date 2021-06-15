using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("PersonalInformation")]
    public class PersonalInformation
    {
        [Key]
        public string phoneNumber { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        public string address { get; set; }
        [Required]
        public string status { get; set; }
        public string note { get; set; }
    }
}
