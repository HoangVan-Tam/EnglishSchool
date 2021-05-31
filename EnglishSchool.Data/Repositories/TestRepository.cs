using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;

namespace EnglishSchool.Data.Repositories
{
    public interface ITestRepository : IRepository<Test>
    {

    }
    public class TestRepository : RepositoryBase<Test>, ITestRepository
    {
        public TestRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
