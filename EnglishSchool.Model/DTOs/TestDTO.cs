using System;
using System.Collections.Generic;

namespace EnglishSchool.Model.DTOs
{
    public class TestDTO
    {
        public int week { get; set; }
        public int testId { get; set; }
        public DateTime startDay { get; set; }
        public DateTime finishDay { get; set; }
        public string status { get; set; }
        public float score { get; set; }
        public int courseDetailId { get; set; }
    }
    public class SubmitTestDTO
    {
        public int testId { get; set; }
        public float score { get; set; }
    }

   /* public class Test2Ver2DTO
    {
        public int week { get; set; }
        public int testId { get; set; }
        public string status { get; set; }
        public float score { get; set; }
        public int courseDetailId { get; set; }
        public List<AttendanceOfStudent> attendances { get; set; }
    }*/
    public class Test2DTO
    {
        public int week { get; set; }
        public int testId { get; set; }
        public string status { get; set; }
        public float score { get; set; }
        public int courseDetailId { get; set; }
    }

    public class Test3DTO
    {
        public int week { get; set; }
        public int testId { get; set; }
        public DateTime startDay { get; set; }
        public DateTime finishDay { get; set; }
        public string status { get; set; }
        public float score { get; set; }
        public int courseDetailId { get; set; }
        public List<AttendanceOfStudent> attendances { get; set; }
    }

}
