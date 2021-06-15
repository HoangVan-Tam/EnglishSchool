using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int scheduleId { get; set; }
        [Required]
        public string day { get; set; }
        [Required]
        public string timeStart { get; set; }
        [Required]
        public string timeEnd { get; set; }
        [ForeignKey("classes")]
        public int classId { get; set; }
        public Class classes { get; set; }
    }
}
