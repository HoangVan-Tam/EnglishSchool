using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string detail { get; set; }
        [Required]
        public string requirement { get; set; }
        public List<RecruitmentDetail> listRecruitmentDetails { get; set; }
    }
}
