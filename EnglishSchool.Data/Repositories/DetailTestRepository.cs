using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IDetailTestRepository : IRepository<DetailTest>
    {
    }
    public class DetailTestRepository : RepositoryBase<DetailTest>, IDetailTestRepository
    {
        public DetailTestRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
