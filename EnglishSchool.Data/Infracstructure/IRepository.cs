using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Infracstructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetSingleByCondition(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        List<T> GetMulti(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        bool CheckContains(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetMultiAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate);
    }
}
