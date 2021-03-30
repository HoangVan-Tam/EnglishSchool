using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseDetailOfStudentRepository : IRepository<CourseDetailOfStudent>
    {
        List<CourseDetailOfStudent> GetAllInFormation();
    }
    public class CourseDetailOfStudentRepository : RepositoryBase<CourseDetailOfStudent>, ICourseDetailOfStudentRepository
    {
        public CourseDetailOfStudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<CourseDetailOfStudent> GetAllInFormation()
        {
            return db.CourseDetailOfStudent.Include("courses").Include("students").ToList();
        }
    }
}
