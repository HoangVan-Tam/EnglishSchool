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
    public interface ICourseService : IServiceBase<CourseDTO>
    {
        ResponseService<List<CourseDTO>> GetAll(int deparmentId);
        ResponseService<List<CourseDTO>> GetAllCourseNoOneRegister(string studentId);
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
        public ResponseService<string> AddAndSave(CourseDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _mapper.Map<CourseDTO, Course>(entity);
                _repository._course.Add(temp);
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
                response.result = _mapper.Map<List<Course>, List<CourseDTO>>(_repository._course.GetAllInfoListCourse());
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
            var db = _db.Init();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var temp2 = _repository._schedule.GetMulti(p => p.courseId == entity.id);
                    var temp = temp2.Where(p => p.courseId == entity.id).FirstOrDefault();
                    if (temp == null)
                    {                      
                        foreach (var item in entity.schedules)
                        {
                            if (item.day != null)
                            {
                                Schedule schedule = new Schedule()
                                {

                                    courseId = entity.id,
                                    day = item.day,
                                    timeEnd = item.timeEnd,
                                    timeStart = item.timeStart,
                                };
                                _repository._schedule.Add(schedule);
                            }
                            SaveChanges();
                        }
                    }
                    else if (temp2.Count == entity.schedules.Count)
                    {
                        for(int i = 0; i < temp2.Count; i++)
                        {
                            temp2[i].day = entity.schedules[i].day;
                            temp2[i].timeEnd = entity.schedules[i].timeEnd;
                            temp2[i].timeStart = entity.schedules[i].timeStart;
                        }
                        SaveChanges();
                    }
                    else if (temp2.Count < entity.schedules.Count)
                    {
                        var countSchedule = entity.schedules.Count - temp2.Count;
                        for(int i = countSchedule-1; i ==-1; i--)
                        {
                            _repository._schedule.Add(_mapper.Map<ScheduleDTO, Schedule>(entity.schedules[temp2.Count+i]));
                        }         
                    }
                    var course = _mapper.Map<CourseDTO, CourseUpdateDTO>(entity);
                    _repository._course.Update(_mapper.Map<CourseUpdateDTO, Course>(course));
                    SaveChanges();
                    transaction.Commit();
                    response.result = "Update Course Successfully";
                    db.Dispose();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    response.message = ex.Message;
                    response.success = false;
                }
            } 
            return response;
        }

        public ResponseService<string> Add(CourseDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<CourseDTO>> GetAllCourseNoOneRegister(string studentId)
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                var courseDetails = _repository._courseDetailOfStudent.GetAllInFormationAddCourseInfo();
                var temp = courseDetails.Where(p => p.studentId == studentId && p.finish==false).ToList();
                foreach(var item in temp)
                {
                    if (courseDetails.Where(p => p.courseId == item.courseId).FirstOrDefault()!=null)
                    {
                        courseDetails.RemoveAll(p=>p.courseId==item.courseId);
                    }
                }
                response.result = _mapper.Map<IEnumerable<Course>, List<CourseDTO>>(courseDetails.Select(x=>x.courses).Distinct());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<List<CourseDTO>> GetAll(int departmentId)
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                var temp = _mapper.Map<List<Course>, List<CourseDTO>>(_repository._course.GetAllInfoListCourse().Where(p=>p.departmentId==departmentId).ToList());
                response.result = new List<CourseDTO>();
                foreach (var item in temp)
                {
                    if (_repository._courseDetailOfEmployee.GetSingleByCondition(p => p.courseId == item.id)== null)
                    {
                        response.result.Add(item);
                    }
                }
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
