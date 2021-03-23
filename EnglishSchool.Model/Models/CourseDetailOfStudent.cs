using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class CourseDetailOfStudent
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("courses")]
        public int courseId { get; set; }
        public Course courses { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("students")]
        public string studentId { get; set; }
        public Student students { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime dayStart { get; set; }
        [Required]
        public DateTime dayFinish { get; set; }
        [Required]
        public bool finish { get; set; }
        public float tuition { get; set; }
    }
}
