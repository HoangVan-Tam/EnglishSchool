using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Service
{
    public interface IDepartmentService : IServiceBase<DepartmentDTO>
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
        public ResponseService<string> AddAndSave(DepartmentDTO departmentDTO)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._department.Add(_mapper.Map<DepartmentDTO, Department>(departmentDTO));
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
        public ResponseService<List<DepartmentDTO>> GetAll()
        {
            var response = new ResponseService<List<DepartmentDTO>>();
            try
            {
                var result = _repository._department.GetListDepartmentWithStudent();

                response.result = _mapper.Map<List<Department>, List<DepartmentDTO>>(result);
                for (int i = 0; i < response.result.Count; i++)
                {
                    response.result[i].numberStudent = result[i].students.Count;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        //Get by id
        public ResponseService<DepartmentDTO> GetById(int id)
        {
            var response = new ResponseService<DepartmentDTO>();
            try
            {
                response.result = _mapper.Map<Department, DepartmentDTO>(_repository._department.GetSingleByCondition(p => p.id == id));
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        //Update
        public ResponseService<string> Update(DepartmentDTO departmentDTO)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._department.Update(_mapper.Map<DepartmentDTO, Department>(departmentDTO));
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

        public ResponseService<string> Add(DepartmentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
