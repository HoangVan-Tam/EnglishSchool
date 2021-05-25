using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class DepartmentDTO
    {
        public int id { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string detail { get; set; }
        public int numberStudent { get; set; }
    }
}
