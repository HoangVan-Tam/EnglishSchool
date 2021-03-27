using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class FullInfoStudentDTO
    {
        public string studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int level { get; set; }
        public DateTime deactivationDate { get; set; }
        public int departmentId { get; set; }
        public int courseId { get; set; }
    }
    public class StudentLoginDTO
    {
        public string studentId { get; set; }
        public string password { get; set; }
    }
    public class StudentLoginReponseDTO
    {
        public string studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int level { get; set; }
        public int departmentId { get; set; }
        public string token { get; set; }

    }
}
