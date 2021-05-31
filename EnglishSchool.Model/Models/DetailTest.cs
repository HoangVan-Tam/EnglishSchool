using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    public class DetailTest
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("tests")]
        public int testId { get; set; }
        public Test tests { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("questions")]
        public int questionId { get; set; }
        public Question questions { get; set; }
        public bool correct { get; set; }
    }
}
