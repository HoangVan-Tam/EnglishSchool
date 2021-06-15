using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishSchool.Model.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public DateTime postDate { get; set; }
        public string image { get; set; }
        [Required]
        public string headContent { get; set; }
        [Required]
        public string bodyContent { get; set; }
    }
}
