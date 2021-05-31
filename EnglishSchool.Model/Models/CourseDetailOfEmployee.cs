using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class CourseDetailOfEmployee
    {
        [Key]
        [Column(Order =1)]
        [ForeignKey("employees")]
        public string teacherId { get; set; }
        public Employee employees { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("courses")]
        public int courseId { get; set; }
        public Course courses { get; set; }
    }
}
