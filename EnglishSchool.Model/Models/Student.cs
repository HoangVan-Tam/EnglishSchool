﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Student
    {
        [Key]
        public string studentId { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime birthday { get; set; }
        [Required]
        public bool sex { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string email { get; set; }
        [Required]
        public int level { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public DateTime deactivationDate { get; set; }
        [Required]
        public string password { get; set; }
        [ForeignKey("departments")]
        public int departmentId { get; set; }
        public Department departments { get; set; }
        [ForeignKey("parents")]
        public string parentId  { get; set; }
        public Parent parents { get; set; }
        // [Required]
        public List<CourseDetailOfStudent> courseDetailOfStudents { get; set; }
    }
}
