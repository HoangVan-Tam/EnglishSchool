using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;

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
