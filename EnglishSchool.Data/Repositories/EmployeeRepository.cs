using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        List<Employee> GetAllInfo();
        int CountTeacher();
        int GetLastTeacherId();
    }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int CountTeacher()
        {
            return db.Employee.Where(p => p.role == "Teacher").Count();
        }

        public List<Employee> GetAllInfo()
        {
            return db.Employee.Include("departments").ToList();
        }

        public int GetLastTeacherId()
        {
            try
            {
                var teacherId = db.Employee.Where(p => p.role == "Teacher").ToList().Last().userId.Substring(12);
                return Convert.ToInt32(teacherId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
