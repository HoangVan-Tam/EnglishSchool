using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetAllInFomation();
        Student GetAllInfoById(string id);
        int GetLastStudentId();
        bool CheckCourseDetail(List<Schedule> schedule, string studentId);
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public bool CheckCourseDetail(List<Schedule> schedule, string studentId)
        {
            var check = db.CourseDetailOfStudent.Where(p =>p.studentId == studentId && p.finish == false).ToList();
            List<Schedule> lst = new List<Schedule>();
            foreach(var item1 in check)
            {
                lst.AddRange(db.Schedule.Where(p => p.courseId == item1.courseId).ToList());   
            }
            foreach(var item2 in schedule)
            {
                if(lst.Where(p=>p.day==item2.day && (Convert.ToDateTime(item2.timeStart)>=Convert.ToDateTime(p.timeStart) || Convert.ToDateTime(item2.timeEnd) <= Convert.ToDateTime(p.timeEnd))).FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }

        public Student GetAllInfoById(string id)
        {
            return db.Student.Include("departments").Include("parents").Where(p => p.studentId == id).FirstOrDefault();
        }

        public List<Student> GetAllInFomation()
        {
            return db.Student.Include("departments").Include("parents").ToList();
        }

        public int GetLastStudentId()
        {
            try
            {
                var studentId = db.Student.ToList().Last().studentId.Substring(6, 6);
                return Convert.ToInt32(studentId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
