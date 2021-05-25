using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseDetailOfStudentRepository : IRepository<CourseDetailOfStudent>
    {
        List<CourseDetailOfStudent> GetAllInFormation();
        List<CourseDetailOfStudent> GetAllInFormationById(Expression<Func<CourseDetailOfStudent, bool>> expression);
        void Add(CourseDetailOfStudent entity, int numberOfMonth);
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
        
        public List<CourseDetailOfStudent> GetAllInFormationById(Expression<Func<CourseDetailOfStudent, bool>> expression)
        {
            return db.CourseDetailOfStudent.Include("courses").Include("students").Where(expression).ToList();
        }

        public virtual void Add(CourseDetailOfStudent entity, int numberOfMonth)
        {
            DateTime startday = DateTime.Now;
            var id= db.CourseDetailOfStudent.Add(entity).courseDetailId;  
            for(int i=0; i < numberOfMonth; i++)
            {
                Test test = new Test()
                {
                    status = "Chưa Thi",
                    startDay = startday,
                    finishDay = startday.AddDays(7),
                    score = 0,
                    courseDetailId = id,
                };
                db.Tests.Add(test);
                startday = test.finishDay;
            }
        }
    }
}
