using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IScoreResultRepository : IRepository<ScoreResult>
    {

    }
    public class ScoreResultRepository : RepositoryBase<ScoreResult>, IScoreResultRepository
    {
        public ScoreResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
