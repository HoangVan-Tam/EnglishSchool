using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class ScoreResultDTO
    {
        public int scoreResultId { get; set; }
        public int courseDetailId { get; set; }
        public string nameOfExam { get; set; }
        public float listening { get; set; }
        public float speaking { get; set; }
        public float reading { get; set; }
        public float writing { get; set; }
    }
}
