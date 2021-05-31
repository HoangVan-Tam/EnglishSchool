namespace EnglishSchool.Data.Infracstructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private EnglishSchoolDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public EnglishSchoolDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public async void CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
