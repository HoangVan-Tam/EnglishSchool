using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class CourseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfWeeks { get; set; }
        public DateTime theOpeningDay { get; set; }
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        public string title { get; set; }
        public string headContent { get; set; }
        public string bodyContent { get; set; }
        public int salary { get; set; }
    }
}
