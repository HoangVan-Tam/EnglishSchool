using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
    public interface IEmployeeService : IServiceBase<EmployeeDTO>
    {
        ResponseService<List<EmployeeDTO>> GetAllTeacher();
        ResponseService<EmployeeDTO> GetById(string userId);
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
        public ResponseService<string> Add(EmployeeDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(EmployeeDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._employee.GetLastTeacherId()+1;
                var teacher = _mapper.Map<EmployeeDTO, Employee>(entity);
                teacher.userId = String.Format("{0:D2}", entity.departmentId) + "_giaovien_" + temp.ToString();
                teacher.password= BCrypt.Net.BCrypt.HashPassword("123456789");
                teacher.role = "Teacher";
                teacher.status = true;
                _repository._employee.Add(teacher);
                SaveChanges();
                response.result = "Add teacher successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<EmployeeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<EmployeeDTO>> GetAllTeacher()
        {
            var response = new ResponseService<List<EmployeeDTO>>();
            try
            {
                var result = _repository._employee.GetAllInfo().Where(p=>p.role=="Teacher").ToList();
                response.result = _mapper.Map<List<Employee>, List<EmployeeDTO>>(result);
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response; 
        }

        public ResponseService<EmployeeDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<EmployeeDTO> GetById(string userId)
        {
            var response = new ResponseService<EmployeeDTO>();
            try
            {
                var result = _repository._employee.GetSingleByCondition(p => p.userId == userId);
                response.result = _mapper.Map<Employee, EmployeeDTO>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ResponseService<string> Update(EmployeeDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var employee = _repository._employee.GetSingleByCondition(p => p.userId == entity.userId);
                employee.firstName = entity.firstName;
                employee.lastName = entity.lastName;
                employee.sex = entity.sex;
                employee.birthday = entity.birthday;
                employee.address = entity.address;
                employee.phoneNumber = entity.phoneNumber;
                employee.email = entity.email;
                employee.departmentId = entity.departmentId;
                _repository._employee.Update(employee);
                SaveChanges();
                response.result = "Update Employee successfully";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
