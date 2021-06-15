using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Model.DTOs
{
    public class ClassDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfStudent { get; set; }
        public int departmentId { get; set; }
        public DepartmentDTO departments { get; set; }
        public string teacherId { get; set; }
        public EmployeeDTO employees { get; set; }
        public int courseId { get; set; }
        public CourseDTO courses { get; set; }
        public List<ClassDetailOfStudentDTO> classDetailOfStudents { get; set; }
        public List<ScheduleDTO> schedules { get; set; }
    }
    public class ClassInfoForTeacher
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfStudent { get; set; }
        public int departmentId { get; set; }
        public DepartmentDTO departments { get; set; }
        public string teacherId { get; set; }
        public EmployeeDTO employees { get; set; }
        public int courseId { get; set; }
        public CourseDTO courses { get; set; }
        public List<ScheduleDTO> schedules { get; set; }
    }

    public class ClassUpdateDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int departmentId { get; set; }
        public string teacherId { get; set; }
        public int courseId { get; set; }
        List<Schedule> schedules { get; set; }
    }

    public class ClassForStudentDetailDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int numberOfStudent { get; set; }
        public int departmentId { get; set; }
        public DepartmentDTO departments { get; set; }
        public string teacherId { get; set; }
        public EmployeeDTO employees { get; set; }
        public int courseId { get; set; }
        public CourseDTO courses { get; set; }
        public List<ScheduleDTO> schedules { get; set; }
    }
}
