using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
