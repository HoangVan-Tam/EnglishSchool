using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Model.DTOs
{
    public class CourseDetailOfStudentDTO
    {
        public int courseDetailId { get; set; }
        public int courseId { get; set; }
        public string studentId { get; set; }
        public DateTime dayStart { get; set; }
        public DateTime dayFinish { get; set; }
        public bool finish { get; set; }
        public float tuition { get; set; }
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
        public List<ScheduleDTO> schedules { get; set; }
    }
    public class ListCourseDetailOfStudent
    {
        public InfoStudent students { get; set; }

    }
    public class InfoStudent
    {
        public string studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class ListAttendanceStudentOfCourse
    {
        public InfoStudent students { get; set; }
        public List<AttendanceOfStudent> attendances { get; set; }
    }


    public class CourseStudentNoRegister
    {
        public CourseDTO courses { get; set; }
    }

    public class TeacherManageStudentVer2
    {
        public DateTime beginningOfTheWeek { get; set; }
        public DateTime weekend { get; set; }
        public int courseDetailId { get; set; }
        public InfoStudent students { get; set; }
        public List<Test3DTO> tests { get; set; }
    }

    public class TeacherManageStudent
    {
        public DateTime beginningOfTheWeek { get; set; }
        public DateTime weekend { get; set; }
        public int courseDetailId { get; set; }
        public InfoStudent students { get; set; }
        public List<Test2DTO> tests { get; set; }
        public List<AttendanceOfStudent> attendances { get; set; }
    }
}
