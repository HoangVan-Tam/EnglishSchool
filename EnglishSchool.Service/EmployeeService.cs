using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IEmployeeService : IServiceBase<Employee>
    {

    }
    public class EmployeeService : IEmployeeService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EmployeeService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(Employee entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseService<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
