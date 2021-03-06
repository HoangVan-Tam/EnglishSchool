using System;
using System.Collections.Generic;

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
        public bool status { get; set; }
        public DateTime deactivationDate { get; set; }
        public int departmentId { get; set; }
        public int courseId { get; set; }
        public int classId { get; set; }
        public NameOfDepartment departments { get; set; }
        public NameOfParent parents { get; set; }
    }
    public class NameOfDepartment
    {
        public string name { get; set; }
    }
    public class NameOfParent
    {
        public string parentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
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

    public class StudentRegisterCourse
    {
        public string studentId { get; set; }
        public int classId { get; set; }
        public int courseId { get; set; }
    }

    public class ManageStudentDTO
    {
        public List<Test3DTO> tests { get; set; }
    }
}
