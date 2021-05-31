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
                    var temp = _repository._schedule.GetSingleByCondition(p => p.courseId == entity.id);
                    var temp2 = _repository._schedule.GetMulti(p => p.courseId == entity.id);
                    if (temp == null)
                    {
                        foreach (var item in entity.schedules)
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
                    else if (temp2.Count == entity.schedules.Count)
                    {
                        foreach (var item in entity.schedules)
                        {
                            _repository._schedule.Update(_mapper.Map<ScheduleDTO, Schedule>(item));
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
                var courseDetails = _repository._courseDetailOfStudent.GetAllInFormationAddCourseInfo(studentId).Select(p=>p.courses).Distinct();
                response.result = _mapper.Map<IEnumerable<Course>, List<CourseDTO>>(courseDetails);
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
