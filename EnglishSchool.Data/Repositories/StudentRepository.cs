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
        int GetLastStudentId();
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int GetLastStudentId()
        {
            try
            {
                return Convert.ToInt32(db.Student.ToList().Last().studentId);
            }
            catch
            {
                return 0;
            }

        }
    }
}
