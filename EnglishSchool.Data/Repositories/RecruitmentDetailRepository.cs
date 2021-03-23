using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IRecruitmentDetailRepository : IRepository<RecruitmentDetail>
    {
        List<RecruitmentDetail> GetAllInFomationOfRecruitment();
    }
    public class RecruitmentDetailRepository : RepositoryBase<RecruitmentDetail>, IRecruitmentDetailRepository
    {
        public RecruitmentDetailRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }

        public List<RecruitmentDetail> GetAllInFomationOfRecruitment()
        {   
            return db.ListRecruitmentDetail.Include("departments").Include("recruitments").ToList();
        }
    }
}
