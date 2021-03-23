using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class RecruitmentDetailDTO
    {
        public int departmentId { get; set; }
        public int recruitmentId { get; set; }
        public int amount { get; set; }
        public bool complete { get; set; }
        public NameOfDepartment departments { get; set; }
        public NameOfRecruitment recruitments { get; set; }
    }
    public class NameOfRecruitment
    {
        public string vacancies { get; set; }
    }
    public class NameOfDepartment
    {
        public string name { get; set; }
    }
    public class ReceiveListRecruitmentDetailDTO
    {
        public int departmentId { get; set; }
        public int recruitmentId { get; set; }
        public int amount { get; set; }
        public bool complete { get; set; }
    }
}
