using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    public class RecruitmentDetail
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("departments")]
        public int departmentId { get; set; }
        public Department departments { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("recruitments")]
        public int recruitmentId { get; set; }
        public Recruitment recruitments { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public bool complete { get; set; }
    }
}
