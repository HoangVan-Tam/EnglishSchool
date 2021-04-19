using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IScoreResultRepository : IRepository<Parent>
    {

    }
    public class ScoreResultRepository : RepositoryBase<Parent>, IScoreResultRepository
    {
        public ScoreResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
