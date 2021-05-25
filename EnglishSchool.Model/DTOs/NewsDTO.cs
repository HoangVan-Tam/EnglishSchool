using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Model.DTOs
{
    public class NewsDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime postDate { get; set; }
        public string postDateClient { get; set; }
        public string image { get; set; }
        public string detail { get; set; }
    }
    public class AddNews
    {
        public NewsDTO entity { get; set; }
    }
}
