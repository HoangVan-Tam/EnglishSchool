using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("Class")]
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public int numberOfStudent { get; set; }
        [ForeignKey("departments")]
        public int departmentId { get; set; }
        public Department departments { get; set; }
        [ForeignKey("employees")]
        public string teacherId { get; set; }
        public Employee employees { get; set; }
        [ForeignKey("courses")]
        public int courseId { get; set; }
        public Course courses { get; set; }
        public List<ClassDetailOfStudent> classDetailOfStudents { get; set; }
        public List<Schedule> schedules { get; set; }

    }
}
