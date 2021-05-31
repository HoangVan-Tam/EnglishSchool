namespace EnglishSchool.Data.Infracstructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        EnglishSchoolDbContext data;
        public EnglishSchoolDbContext Init()
        {
            if (data == null)
            {
                data = new EnglishSchoolDbContext();
            }
            return data;
        }
        public DbFactory(EnglishSchoolDbContext db)
        {
            data = db;
        }
        protected override void DisposeCore()
        {
            if (data != null)
            {
                data.Dispose();
            }
        }
    }
}
