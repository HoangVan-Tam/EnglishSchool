using System.Collections.Generic;

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
    public class ListQuestionExam
    {
        public List<QuestionExam> results { get; set; }
    }
    public class QuestionExam
    {
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }
        public string difficulty { get; set; }
    }
}
