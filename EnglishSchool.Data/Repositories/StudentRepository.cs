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
        int GetLastStudentId(int departmentId);
        bool CheckCourseDetail(List<Schedule> schedule, string studentId);
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public bool CheckCourseDetail(List<Schedule> schedule, string studentId)
        {
            var check = db.ClassDetailOfStudent.Where(p =>p.studentId == studentId && p.finish == false).ToList();
            List<Schedule> lst = new List<Schedule>();
            foreach(var item1 in check)
            {
                lst.AddRange(db.Schedule.Where(p => p.classId == item1.classId).ToList());   
            }
            foreach(var item2 in schedule)
            {
                if(lst.Where(p=>p.day==item2.day && ((Convert.ToDateTime(item2.timeStart)>=Convert.ToDateTime(p.timeStart) && Convert.ToDateTime(item2.timeStart)<=Convert.ToDateTime(p.timeEnd)) || (Convert.ToDateTime(item2.timeEnd)>=Convert.ToDateTime(p.timeStart) && Convert.ToDateTime(item2.timeEnd) <= Convert.ToDateTime(p.timeEnd)))).FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }

        public Student GetAllInfoById(string id)
        {
            return db.Student.Include("departments").Where(p => p.studentId == id).FirstOrDefault();
        }

        public List<Student> GetAllInFomation()
        {
            return db.Student.Include("departments").Include("courseDetailOfStudents").ToList();
        }

        public int GetLastStudentId(int departmentId)
        {
            try
            {
                if (departmentId < 10)
                {
                    var temp = "stu" + "0" + departmentId.ToString();
                    var studentId = db.Student.Where(p => p.studentId.Contains(temp)).ToList().OrderByDescending(p => p.studentId).First().studentId.Substring(6, 6);
                    return Convert.ToInt32(studentId);
                }
                else
                {
                    var temp = "stu" + departmentId.ToString();
                    var studentId = db.Student.Where(p => p.studentId.Contains(temp)).ToList().OrderByDescending(p => p.studentId).First().studentId.Substring(6, 6);
                    return Convert.ToInt32(studentId);
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
