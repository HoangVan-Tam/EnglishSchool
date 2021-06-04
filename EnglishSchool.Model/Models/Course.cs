using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string title { get; set; }
        [Required]
        public string headContent { get; set; }
        [Required]
        public string bodyContent { get; set; }
        [Required]
        public int numberOfMonths { get; set; }
        [Required]
        public DateTime theOpeningDay { get; set; }
        [Required]
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        /*
        [ForeignKey("departments")]
        public int departmentId { get; set; }
        public Department departments { get; set; }*/
        public List<CourseDetailOfStudent> courseDetailOfStudents { get; set; }
        public List<Schedule> schedules { get; set; }
    }
}
