using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSchool.Service
{
    public interface ICourseDetailOfStudentService : IServiceBase<ClassDetailOfStudentDTO>
    {
        ResponseService<string> Update();
        ResponseService<List<ClassDetailOfStudentDTO>> GetAllCourseOfStudent(string studentId);
        ResponseService<string> Delete(int courseId, string studentId);
        
    }
    public class ClassDetailOfStudentService : ICourseDetailOfStudentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassDetailOfStudentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseService<string> AddAndSave(ClassDetailOfStudentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                _repository._classDetailOfStudent.Add(_mapper.Map<ClassDetailOfStudentDTO, ClassDetailOfStudent>(entity));
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
                var temp = _repository._classDetailOfStudent.GetSingleByCondition(p => p.classId == courseId && p.studentId == studentId);
                _repository._classDetailOfStudent.Delete(temp);
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

        public ResponseService<List<ClassDetailOfStudentDTO>> GetAll()
        {
            var response = new ResponseService<List<ClassDetailOfStudentDTO>>();
            try
            {
                var temp = _repository._classDetailOfStudent.GetAllInFormation();
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ClassDetailOfStudentDTO>>(temp);
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
                _repository._classDetailOfStudent.GetMulti(p => p.dayFinish < DateTime.Now).ForEach(p => p.finish = true);
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


        public ResponseService<ClassDetailOfStudentDTO> GetById(int id)
        {
            var response = new ResponseService<ClassDetailOfStudentDTO>();
            try
            {
                var temp = _repository._classDetailOfStudent.GetSingleByCondition(p => p.studentId == id.ToString() || p.classId == id);
                response.result = _mapper.Map<ClassDetailOfStudent, ClassDetailOfStudentDTO>(temp);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<List<ClassDetailOfStudentDTO>> GetAllCourseOfStudent(string studentId)
        {
            var response = new ResponseService<List<ClassDetailOfStudentDTO>>();
            try
            {

                var result = _repository._classDetailOfStudent.GetAllInFormationById(p => p.studentId == studentId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ClassDetailOfStudentDTO>>(result); 
                foreach(var item in response.result)
                {
                    item.classes.schedules = _mapper.Map<List<Schedule>, List<ScheduleDTO>>(_repository._schedule.GetMulti(p => p.classId == item.classes.id));
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }




        public ResponseService<string> Add(ClassDetailOfStudentDTO entity)
        {
            throw new NotImplementedException();
        }
        public ResponseService<string> Update(ClassDetailOfStudentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
