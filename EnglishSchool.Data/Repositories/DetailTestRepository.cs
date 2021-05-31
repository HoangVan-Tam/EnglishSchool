using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface IDetailTestRepository : IRepository<DetailTest>
    {
        List<DetailTest> GetAllInFomation();
        List<DetailTest> GetAllInFomation(int testId);
    }
    public class DetailTestRepository : RepositoryBase<DetailTest>, IDetailTestRepository
    {
        public DetailTestRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<DetailTest> GetAllInFomation(int testId)
        {
            return db.DetailTest.Include("questions").ToList();
        }

        public List<DetailTest> GetAllInFomation()
        {
            throw new NotImplementedException();
        }
    }
}
