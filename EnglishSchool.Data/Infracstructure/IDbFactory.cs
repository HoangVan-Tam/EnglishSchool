using System;

namespace EnglishSchool.Data.Infracstructure
{
    public interface IDbFactory : IDisposable
    {
        EnglishSchoolDbContext Init();
    }
}
