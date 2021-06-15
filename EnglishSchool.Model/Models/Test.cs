using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("Exercise")]
    public class Test
    {
        [Key]
        public int testId { get; set; }
        [Required]
        public int week { get; set; }
        [Required]
        public DateTime startDay { get; set; }
        [Required]
        public DateTime finishDay { get; set; }
        public string comment { get; set; }
        [Required]
        public string status { get; set; }
        public float score { get; set; }
        [ForeignKey("courseDetailOfStudents")]
        public int courseDetailId { get; set; }
        public ClassDetailOfStudent courseDetailOfStudents { get; set; }
        public List<DetailTest> detailTests { get; set; }
    }
}
