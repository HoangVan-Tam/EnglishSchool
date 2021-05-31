namespace EnglishSchool.Model.DTOs
{
    public class DetailTestDTO
    {
        public int testId { get; set; }
        public QuestionDTO questions { get; set; }
        public bool correct { get; set; }
    }
}
