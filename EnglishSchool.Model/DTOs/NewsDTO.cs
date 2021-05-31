using System;

namespace EnglishSchool.Model.DTOs
{
    public class NewsDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime postDate { get; set; }
        public string postDateClient { get; set; }
        public string image { get; set; }
        public string headContent { get; set; }
        public string bodyContent { get; set; }
    }
    public class AddNews
    {
        public NewsDTO entity { get; set; }
    }
}
