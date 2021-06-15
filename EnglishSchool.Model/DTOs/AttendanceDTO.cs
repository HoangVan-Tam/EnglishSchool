using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class AttendanceStudentDTO
    {
        public string  studentId { get; set; }
        public DateTime date { get; set; }
        public bool absent { get; set; }
        public string reason { get; set; }
        public string comment { get; set; }
    }

    public class AttendanceDTO
    {
        public int courseId { get; set; }
        public DateTime firstDayOfWeek { get; set; }
        public int session { get; set; }
        public List<AttendanceStudentDTO> attendances { get; set; }
    }
    public class AttendanceOfStudent
    {
        public DateTime date { get; set; }
        public bool absent { get; set; }
        public string reason { get; set; }
    }
}
