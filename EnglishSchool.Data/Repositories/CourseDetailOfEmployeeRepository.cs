using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseDetailOfEmployeeRepository : IRepository<CourseDetailOfEmployeeRepository>
    {
        List<CourseDetailOfEmployee> GetAllCourseOfTeacher(string teacherId);
    }
    public class CourseDetailOfEmployeeRepository : RepositoryBase<CourseDetailOfEmployeeRepository>, ICourseDetailOfEmployeeRepository
    {
        public CourseDetailOfEmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<CourseDetailOfEmployee> GetAllCourseOfTeacher(string teacherId)
        {
            return db.CourseDetailOfEmployee.Include("courses").Where(p => p.teacherId == teacherId).ToList();
        }
    }
}
