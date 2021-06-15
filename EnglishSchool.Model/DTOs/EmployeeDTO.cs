using System;

namespace EnglishSchool.Model.DTOs
{
    public class EmployeeDTO
    {
        public string departmentName { get; set; }
        public int totalCourse { get; set; }
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public bool status { get; set; }
        public int departmentId { get; set; }
        public NameOfDepartment departments { get; set; }
        public int salary { get; set; }
    }
    public class ManageCourse
    {
        public int courseId { get; set; }
        public DateTime firstDayOfWeek { get; set; }
    }
    public class EmployeeLoginDTO
    {
        public int departmentId { get; set; }
        public int salary { get; set; }
        public string departmentName { get; set; }
        public int totalCourse { get; set; }
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }

    public class EmployeeRegisterCourse
    {
        public string userId { get; set; }
        public int id { get; set; }
    }
}
