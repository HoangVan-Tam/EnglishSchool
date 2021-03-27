using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class ScoreResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int scoreResultId { get; set; }

        [ForeignKey("courseDetailOfStudents")]
        public int courseDetailId { get; set; }
        public CourseDetailOfStudent courseDetailOfStudents { get; set; }



        [Required]
        public string nameOfExam { get; set; }
        [Required]
        public float listening { get; set; }
        [Required]
        public float speaking { get; set; }
        [Required]
        public float reading { get; set; }
        [Required]
        public float writing { get; set; }
    }
}
