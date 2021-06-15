using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("ClassDetailOfStudent")]
    public class ClassDetailOfStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int courseDetailId { get; set; }

        [ForeignKey("classes")]
        public int classId { get; set; }
        public Class classes { get; set; }

        [ForeignKey("students")]
        public string studentId { get; set; }
        public Student students { get; set; }

        [Required]
        public DateTime dayStart { get; set; }
        [Required]
        public DateTime dayFinish { get; set; }
        [Required]
        public bool finish { get; set; }
        [Required]
        public float tuition { get; set; }
        public List<Test> tests { get; set; }
        public List<Attendance> attendances { get; set; }
    }
}
