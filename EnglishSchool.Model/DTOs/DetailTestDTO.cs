using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class DetailTestDTO
    {
        public TestDTO tests { get; set; }
        public QuestionDTO questions { get; set; }
        public bool correct { get; set; }
    }
}
