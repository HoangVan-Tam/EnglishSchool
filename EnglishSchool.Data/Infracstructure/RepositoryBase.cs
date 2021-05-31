using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Infracstructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private EnglishSchoolDbContext dbContext;
        private readonly IDbSet<T> dbSet;
        protected IDbFactory DbFactory { get; set; }
        protected EnglishSchoolDbContext db
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = DbFactory.Init();
                }
                return dbContext;
            }
        }
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = db.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where<T>(expression).AsQueryable<T>().FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public List<T> GetMulti(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where<T>(expression).ToList();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return dbSet.Count(expression);
        }

        public bool CheckContains(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Count<T>(expression) > 0;
        }

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().Where(expression).AsQueryable<T>().FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsQueryable<T>().ToListAsync();
        }

        public async Task<List<T>> GetMultiAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.CountAsync(expression);
        }

        public async Task<bool> CheckContainsAsync(Expression<Func<T, bool>> expression)
        {
            return await dbContext.Set<T>().CountAsync(expression) > 0;
        }
    }
}
