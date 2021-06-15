using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class ScheduleDTO
    {
        public int scheduleId { get; set; }
        public string day { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public int classId { get; set; }
    }
}
