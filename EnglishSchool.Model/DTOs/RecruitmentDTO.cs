using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class RecruitmentDTO
    {
        public int id { get; set; }
        public string vacancies { get; set; }
        public string jobDecription { get; set; }
        public string jobRequirements { get; set; }
        public string rightsOfTheEmployees { get; set; }
        public string address { get; set; }
        public string amount { get; set; }
        public bool complete { get; set; }
    }
}
