﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishSchool.Model.Models
{
    public class Parent
    {
        [Key]
        public string parentId { get; set; }
        public List<Student> students { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public bool sex { get; set; }
        [Required]
        public DateTime birthday { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
    }

}
