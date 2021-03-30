using EnglishSchool.Model.ResponseService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
