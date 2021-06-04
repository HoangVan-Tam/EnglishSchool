using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseDetailOfEmployeeRepository : IRepository<CourseDetailOfEmployee>
    {
        List<CourseDetailOfEmployee> GetAllCourseOfTeacher(string teacherId);
        bool CheckCourseDetail(List<Schedule> schedule, string userId);
    }
    public class CourseDetailOfEmployeeRepository : RepositoryBase<CourseDetailOfEmployee>, ICourseDetailOfEmployeeRepository
    {
        public CourseDetailOfEmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<CourseDetailOfEmployee> GetAllCourseOfTeacher(string teacherId)
        {
            return db.CourseDetailOfEmployee.Include("courses").Where(p => p.teacherId == teacherId).ToList();
        }

        public bool CheckCourseDetail(List<Schedule> schedule, string userId)
        {
            var check = db.CourseDetailOfEmployee.Where(p => p.teacherId == userId).ToList();
            List<Schedule> lst = new List<Schedule>();
            foreach (var item1 in check)
            {
                lst.AddRange(db.Schedule.Where(p => p.courseId == item1.courseId).ToList());
            }
            foreach (var item2 in schedule)
            {
                if (lst.Where(
                        p => p.day == item2.day && 
                        (Convert.ToDateTime(item2.timeStart) >= Convert.ToDateTime(p.timeStart) || Convert.ToDateTime(item2.timeEnd) <= Convert.ToDateTime(p.timeEnd))
                    ).FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
