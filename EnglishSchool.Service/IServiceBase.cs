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
        ResponseService<string> AddAndSave(JObject entity);
        ResponseService<string> Add(JObject entity);
        ResponseService<string> Update(JObject entity);
        ResponseService<string> Delete(int id);
        ResponseService<List<T>> GetAll();
        ResponseService<T> GetById(int id);
        void SaveChanges();
    }
}
