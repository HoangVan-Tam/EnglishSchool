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
        public int numberOfMonths { get; set; }
        public string schedule { get; set; }
        public DateTime theOpeningDay { get; set; }
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
    }
}
