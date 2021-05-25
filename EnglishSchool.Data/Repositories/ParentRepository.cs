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
        List<Parent> GetAllInFomation();
        int GetLastParentId();
    }
    public class ParentRepository : RepositoryBase<Parent>, IParentRepository
    {
        public ParentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<Parent> GetAllInFomation()
        {
            return db.Parents.Include("students").ToList();
        }

        public int GetLastParentId()
        {
            try
            {
                var studentId = db.Parents.ToList().Last().parentId.Substring(4, 6);
                return Convert.ToInt32(studentId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
