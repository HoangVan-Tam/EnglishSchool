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
    public interface ICourseService : IServiceBase<CourseDTO>
    {

    }
    public class CourseService : ICourseService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> AddAndSave(CourseDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._course.Add(_mapper.Map<CourseDTO, Course>(entity));
                SaveChanges();
                response.result = "Add Course Successfully";
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
            var response = new ResponseService<string>();
            try
            {
                var tempDTO = _repository._course.GetSingleByCondition(p => p.id == id);
                _repository._course.Delete(tempDTO);
                SaveChanges();
                response.result = "Delete Courese Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<List<CourseDTO>> GetAll()
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                response.result = _mapper.Map<List<Course>, List<CourseDTO>>(_repository._course.GetAll());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<CourseDTO> GetById(int id)
        {
            var response = new ResponseService<CourseDTO>();
            try
            {
                response.result = _mapper.Map<Course, CourseDTO>(_repository._course.GetSingleByCondition(p => p.id == id));
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

        public ResponseService<string> Update(CourseDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._course.Update(_mapper.Map<CourseDTO, Course>(entity));
                SaveChanges();
                response.result = "Update Course Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> Add(CourseDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
