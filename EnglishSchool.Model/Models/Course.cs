using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int numberOfMonths { get; set; }
        [Required]
        public string schedule { get; set; }
        [Required]
        public DateTime theOpeningDay { get; set; }
        [Required]
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        public List<CourseDetailOfStudent> courseDetailOfStudents { get; set; }
    }
}
