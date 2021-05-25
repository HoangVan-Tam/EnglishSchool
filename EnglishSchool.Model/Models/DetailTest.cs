using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class DetailTest
    {
        [Key]
        [Column(Order =1)]
        public int testId { get; set; }
        public Test tests { get; set; }
        [Key]
        [Column(Order = 2)]
        public int questionId { get; set; }
        public Question questions { get; set; }
        public bool correct { get; set; }
    }
}
