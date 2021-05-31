using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
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
        [ForeignKey("courses")]
        public int courseId { get; set; }
        public Course courses { get; set; }
    }
}
