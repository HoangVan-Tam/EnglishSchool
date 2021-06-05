using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        int GetLastCourseId();
        List<Course> GetAllInfoListCourse();
        Course GetCourseWithSchedule(int courseId);
        List<Course> GetAllCourseNoOneRegister(string studentId);
    }
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int GetLastCourseId()
        {
            return db.Course.ToList().Last().id;
        }
        public List<Course> GetAllInfoListCourse()
        {
            return db.Course.Include("schedules").Include("departments").ToList();
        }

        public List<Course> GetAllCourseNoOneRegister(string studentId)
        {
            return db.Course.Include("courseDetailOfStudents").ToList();
        }

        public Course GetCourseWithSchedule(int courseId)
        {
            return db.Course.Include("schedules").Where(p=>p.id==courseId).FirstOrDefault();
        }
    }
}
