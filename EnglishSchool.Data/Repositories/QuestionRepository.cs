using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> getRamdon20();
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<Question> getRamdon20()
        {
            return db.Questions.OrderBy(x=>Guid.NewGuid()).Take(50).ToList();
            //return db.Questions.GroupBy(p => p.questionDetail).Select(x => x.FirstOrDefault()).OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
