using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IDepartmentService : IServiceBase<JObject>
    {

    }
    public class DepartmentService : IDepartmentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // Add
        public ResponseService<string> AddAndSave(JObject departmentDTO)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._department.Add(_mapper.Map<JObject, Department>(departmentDTO));
                SaveChanges();
                response.result = "Add Student Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }


        //Delete
        public ResponseService<string> Delete(int id)
        {
            var response = new ResponseService<string>();
            try
            {
                var tempDTO = _repository._department.GetSingleByCondition(p => p.id == id);
                _repository._department.Delete(tempDTO);
                SaveChanges();
                response.result = "Delete Department Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }


        //Get all
        public ResponseService<List<JObject>> GetAll()
        {
            var response = new ResponseService<List<JObject>>();
            try
            {
                response.result = _mapper.Map<List<Department>, List<JObject>>(_repository._department.GetAll());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        //Get by id
        public ResponseService<JObject> GetById(int id)
        {
            var response = new ResponseService<JObject>();
            try
            {
                response.result = _mapper.Map<Department, JObject>(_repository._department.GetSingleByCondition(p => p.id == id));
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        //Update
        public ResponseService<string> Update(JObject departmentDTO)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._department.Update(_mapper.Map<JObject, Department>(departmentDTO));
                SaveChanges();
                response.result = "Update Department Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ResponseService<string> Add(JObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
