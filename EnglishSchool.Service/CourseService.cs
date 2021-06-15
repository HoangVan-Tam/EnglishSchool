using System;
using System.Collections.Generic;
using AutoMapper;
using DevExpress.Xpo;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;

namespace EnglishSchool.Service
{

    public interface ICourseService : IServiceBase<CourseDTO>
    {
        ResponseService<List<CourseDTO>> GetAll(int departmentId);
    }
    public class CourseService : ICourseService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDbFactory _db;
        public CourseService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _db = db;
        }


        public ResponseService<string> Add(CourseDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> AddAndSave(CourseDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var newCourse = _mapper.Map<CourseDTO, Course>(entity);
                _repository._course.Add(newCourse);
                SaveChanges();
                response.result=("Add Course Successfully");
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<CourseDTO>> GetAll()
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                var result = _repository._course.GetAll();
                response.result=_mapper.Map<List<Course>,List<CourseDTO>> (result);
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<CourseDTO> GetById(int id)
        {
            throw new NotImplementedException();
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
                var course = _mapper.Map<CourseDTO, Course>(entity);
                _repository._course.Update(course);
                SaveChanges();
                response.result = "Update Successfully";
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message=ex.Message;
            }
            return response;
        }

        public ResponseService<List<CourseDTO>> GetAll(int departmentId)
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                var result = _repository._course.GetAll();
                response.result = _mapper.Map<List<Course>, List<CourseDTO>>(result);
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