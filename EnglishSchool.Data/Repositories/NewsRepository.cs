using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        int GetLastId();
    }
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public int GetLastId()
        {
            return db.News.ToList().Last().id;
        }
    }
}
