using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        List<Department> GetListDepartmentWithStudent();
    }
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public List<Department> GetListDepartmentWithStudent()
        {
            var result = db.Department.Include("students").ToList();
            return result;
        }
    }
}
