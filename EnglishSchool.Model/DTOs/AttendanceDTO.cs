using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class AttendanceDTO
    {
        public string  studentId { get; set; }
        public DateTime date { get; set; }
        public bool absent { get; set; }
        public string reason { get; set; }
    }
    public class AttendanceOfStudent
    {
        public DateTime date { get; set; }
        public bool absent { get; set; }
        public string reason { get; set; }
    }
}
