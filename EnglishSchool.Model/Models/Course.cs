using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string title { get; set; }
        [Required]
        public string headContent { get; set; }
        [Required]
        public string bodyContent { get; set; }
        [Required]
        public int numberOfWeeks { get; set; }
        [Required]
        public DateTime theOpeningDay { get; set; }
        [Required]
        public int salary { get; set; }
        [Required]
        public float tuition { get; set; }
        public string note { get; set; }
        public float discount { get; set; }
        public List<Class> classes { get; set; }
    }
}
