using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Model.DTOs
{
    public class CourseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfMonths { get; set; }
        public List<ScheduleDTO> schedules { get; set; }
        public DateTime theOpeningDay { get; set; }
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        public string title { get; set; }
        public string headContent { get; set; }
        public string bodyContent { get; set; }
    }
    public class CourseUpdateDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfMonths { get; set; }
        public DateTime theOpeningDay { get; set; }
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        public string title { get; set; }
        public string headContent { get; set; }
        public string bodyContent { get; set; }
    }
}
