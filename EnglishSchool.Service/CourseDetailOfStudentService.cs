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
    public interface ICourseDetailOfStudentService : IServiceBase<CourseDetailOfStudentDTO>
    {
        ResponseService<string> Update();
        ResponseService<List<CourseDetailOfStudentDTO>> GetAllCourseOfStudent(string studentId);
        ResponseService<string> Delete(int courseId, string studentId);
        
    }
    public class CourseDetailOfStudentService : ICourseDetailOfStudentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseDetailOfStudentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> AddAndSave(CourseDetailOfStudentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._courseDetailOfStudent.Add(_mapper.Map<CourseDetailOfStudentDTO, CourseDetailOfStudent>(entity));
                SaveChanges();
                response.result = "Add Course Detail Successfully";
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> Delete(int courseId, string studentId)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._courseDetailOfStudent.GetSingleByCondition(p => p.courseId == courseId && p.studentId == studentId);
                _repository._courseDetailOfStudent.Delete(temp);
                SaveChanges();
                response.result = "Delete Successfully";
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

        public ResponseService<List<CourseDetailOfStudentDTO>> GetAll()
        {
            var response = new ResponseService<List<CourseDetailOfStudentDTO>>();
            try
            {
                var temp = _repository._courseDetailOfStudent.GetAllInFormation();
                response.result = _mapper.Map<List<CourseDetailOfStudent>, List<CourseDetailOfStudentDTO>>(temp);
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


        public ResponseService<string> Update()
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = DateTime.Now;
                _repository._courseDetailOfStudent.GetMulti(p => p.dayFinish < DateTime.Now).ForEach(p => p.finish = true);
                SaveChanges();
                response.result = "Update All Successfully";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<CourseDetailOfStudentDTO> GetById(int id)
        {
            var response = new ResponseService<CourseDetailOfStudentDTO>();
            try
            {
                var temp = _repository._courseDetailOfStudent.GetSingleByCondition(p => p.studentId == id.ToString() || p.courseId == id);
                response.result = _mapper.Map<CourseDetailOfStudent, CourseDetailOfStudentDTO>(temp);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<List<CourseDetailOfStudentDTO>> GetAllCourseOfStudent(string studentId)
        {
            var response = new ResponseService<List<CourseDetailOfStudentDTO>>();
            try
            {

                var result = _repository._courseDetailOfStudent.GetAllInFormationById(p => p.studentId == studentId);
                response.result = _mapper.Map<List<CourseDetailOfStudent>, List<CourseDetailOfStudentDTO>>(result);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }




        public ResponseService<string> Add(CourseDetailOfStudentDTO entity)
        {
            throw new NotImplementedException();
        }
        public ResponseService<string> Update(CourseDetailOfStudentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
