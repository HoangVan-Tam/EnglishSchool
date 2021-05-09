﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Attendance
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int attendanceId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("courseDetailOfStudents")]
        public int courseDetailId { get; set; }
        public CourseDetailOfStudent courseDetailOfStudents { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public bool absent { get; set; }
        public string reason { get; set; }

    }
}