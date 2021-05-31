using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;

namespace EnglishSchool.Data.Repositories
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
    }
    public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

    }
}