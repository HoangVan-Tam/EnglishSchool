using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class CourseDetailOfStudentDTO
    {
        public int courseId { get; set; }
        public int studentId { get; set; }
        public DateTime dayStart { get; set; }
        public DateTime dayFinish { get; set; }
        public bool finish { get; set; }
        public NameOfCourse courses { get; set; }
        public NameOfStudent students { get; set; }
    }
    public class NameOfStudent
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class NameOfCourse
    {
        public string name { get; set; }
    }
}
