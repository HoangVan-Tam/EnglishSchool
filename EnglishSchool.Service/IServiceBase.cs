using EnglishSchool.Model.ResponseService;
using System.Collections.Generic;

namespace EnglishSchool.Service
{
    public interface IServiceBase<T>
    {
        ResponseService<string> AddAndSave(T entity);
        ResponseService<string> Add(T entity);
        ResponseService<string> Update(T entity);
        ResponseService<string> Delete(int id);
        ResponseService<List<T>> GetAll();
        ResponseService<T> GetById(int id);
        void SaveChanges();
    }
}
