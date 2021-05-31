using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    public class Recruitment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string vacancies { get; set; }
        [Required]
        public string jobDecription { get; set; }
        [Required]
        public string jobRequirements { get; set; }
        [Required]
        public string rightsOfTheEmployees { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public bool complete { get; set; }

    }
}
