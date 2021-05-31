using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {

    }
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

    }
}
