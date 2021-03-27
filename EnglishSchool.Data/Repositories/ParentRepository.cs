using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {

    }
    public class ParentRepository : RepositoryBase<Parent>, IParentRepository
    {
        public ParentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
