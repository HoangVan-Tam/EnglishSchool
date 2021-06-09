using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EnglishSchool.Data.Repositories
{
    public interface ICourseDetailOfStudentRepository : IRepository<CourseDetailOfStudent>
    {
        List<CourseDetailOfStudent> GetAllInFormation();
        List<CourseDetailOfStudent> GetAllInFormation(int courseId);
        CourseDetailOfStudent GetAllInFormation(string studentId, int courseId);
        List<CourseDetailOfStudent> GetAllInFormationById(Expression<Func<CourseDetailOfStudent, bool>> expression);
        void Add(CourseDetailOfStudent entity, int numberOfMonth);
        List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourse(int courseId);
        List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourse(int courseId, string studentId);
        List<CourseDetailOfStudent> GetAllInFormationAddCourseInfo();
        List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourseVer2(int courseId);
    }
    public class CourseDetailOfStudentRepository : RepositoryBase<CourseDetailOfStudent>, ICourseDetailOfStudentRepository
    {
        public CourseDetailOfStudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<CourseDetailOfStudent> GetAllInFormation(int courseId)
        {
            return db.CourseDetailOfStudent.Include("students").Where(p=>p.courseId==courseId && p.finish==false).ToList();
        }
        public List<CourseDetailOfStudent> GetAllInFormation()
        {
            return db.CourseDetailOfStudent.Include("courses").Include("students").ToList();
        }

        public CourseDetailOfStudent GetAllInFormation(string studentId, int courseId)
        {
            return db.CourseDetailOfStudent.Include("attendances").Include("tests").Where(p=>p.studentId==studentId && p.courseId==courseId && p.finish==false).FirstOrDefault();
        }

        public List<CourseDetailOfStudent> GetAllInFormationAddCourseInfo()
        {
            return db.CourseDetailOfStudent.Include("courses").ToList();
        }

        public List<CourseDetailOfStudent> GetAllInFormationById(Expression<Func<CourseDetailOfStudent, bool>> expression)
        {
            return db.CourseDetailOfStudent.Include("students").Where(expression).ToList();
        }

        public virtual void Add(CourseDetailOfStudent entity, int numberOfMonth)
        {
            DateTime startday = DateTime.Now;
            if (startday.DayOfWeek.ToString() != "Monday")
            {
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)startday.DayOfWeek + 7) % 7;
                startday = startday.AddDays(daysUntilMonday);
            }
            var id = db.CourseDetailOfStudent.Add(entity).courseDetailId;
            for (int i = 0; i < numberOfMonth; i++)
            {
                Test test = new Test()
                {
                    week = i + 1,
                    status = "Chưa Thi",
                    startDay = startday,
                    finishDay = startday.AddDays(6),
                    score = 0,
                    courseDetailId = id,
                };
                db.Tests.Add(test);
                startday = test.finishDay.AddDays(1);
            }
        }

        public List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourse(int courseId)
        {
            return db.CourseDetailOfStudent.Include("attendances").Include("students").Where(p => p.courseId == courseId).ToList();
        }

        public List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourseVer2(int courseId)
        {
            return db.CourseDetailOfStudent.Include("attendances").Include("students").Include("tests").Where(p => p.courseId == courseId).ToList();
        }

        public List<CourseDetailOfStudent> GetAllAttendanceStudentOfCourse(int courseId, string studentId)
        {
            return db.CourseDetailOfStudent.Include("attendances").Include("students").Where(p => p.courseId == courseId && p.studentId==studentId).ToList();
        }
    }
}
