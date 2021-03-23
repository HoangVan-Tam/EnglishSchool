using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.Models
{
    public class ListPersonOfEvent
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("events")]
        public int eventId { get; set; }
        public Event events { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("personalInformation")]
        public string personId { get; set; }
        public PersonalInformation personalInformation { get; set; }
    }
}
