using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Infracstructure
{
    public interface IDbFactory : IDisposable
    {
        EnglishSchoolDbContext Init();
    }
}
