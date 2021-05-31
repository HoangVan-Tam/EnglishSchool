namespace EnglishSchool.Model.DTOs
{
    public class QuestionDTO
    {
        public int questionId { get; set; }
        public string questionDetail { get; set; }
        public string level { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string rightAnswer { get; set; }
    }
}
