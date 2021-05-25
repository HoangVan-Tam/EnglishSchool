using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetAllInFomation();
        Student GetAllInfoById(string id);
        int GetLastStudentId();
        bool CheckCourseDetail(string schedule, string studentId);
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public bool CheckCourseDetail(string schedule, string studentId)
        {
            var check = db.CourseDetailOfStudent.Where(p => p.courses.schedule == schedule && p.studentId==studentId && p.finish == false).FirstOrDefault();
            if (check == null)
                return true;
            return false;
        }

        public Student GetAllInfoById(string id)
        {
            return db.Student.Include("departments").Include("parents").Where(p=>p.studentId==id).FirstOrDefault();
        }

        public List<Student> GetAllInFomation()
        {
            return db.Student.Include("departments").Include("parents").ToList();
        }

        public int GetLastStudentId()
        {
            try
            {
                var studentId =  db.Student.ToList().Last().studentId.Substring(6,6);  
                return Convert.ToInt32(studentId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
