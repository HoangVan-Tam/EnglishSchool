using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string subTitle { get; set; }
        [Required]
        public string detail { get; set; }
        [ForeignKey("department")]
        public int departmentId { get; set; }
        public Department department { get; set; }
    }
}
