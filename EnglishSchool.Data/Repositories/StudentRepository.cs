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
        int GetLastStudentId();
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<Student> GetAllInFomation()
        {
            return db.Student.Include("departments").ToList();
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
