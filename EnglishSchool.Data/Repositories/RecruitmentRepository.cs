using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IRecruitRepository : IRepository<Recruitment>
    {

    }
    public class RecruitmentRepository : RepositoryBase<Recruitment>, IRecruitRepository
    {
        public RecruitmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
