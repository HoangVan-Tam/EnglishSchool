using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class TestDTO
    {
        public int testId { get; set; }
        public DateTime startDay { get; set; }
        public DateTime finishDay { get; set; }
        public string status { get; set; }
        public float score { get; set; }
        public int courseDetailId { get; set; }
    }
}
