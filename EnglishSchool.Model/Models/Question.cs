using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Question
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int questionId { get; set; }
        [Required]
        public string questionDetail { get; set; }
        [Required]
        public string answer1 { get; set; }
        [Required]
        public string answer2 { get; set; }
        [Required]
        public string answer3 { get; set; }
        [Required]
        public string answer4 { get; set; }
        [Required]
        public int level { get; set; }
        [Required]
        public string rightAnswer { get; set; }
        public List<DetailTest> detailTests { get; set; }
    }
}
