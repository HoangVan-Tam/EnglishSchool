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
    public interface IClassService : IServiceBase<ClassDTO>
    {
        ResponseService<List<ClassDTO>> GetAll(int deparmentId);
        ResponseService<List<ClassInfoForTeacher>> GetAllInfoOfTeacher(string userId);
        ResponseService<List<ClassDetailOfStudentDTO>> GetAllInfoOfStudent(string userId);
        ResponseService<List<ClassDTO>> GetAll(int deparmentId, int courseId);
        ResponseService<List<CourseDTO>> GetAllCourseNoOneRegister(string studentId);
        ResponseService<string> Update(ClassInfoForTeacher entity);
    }
    public class ClassService : IClassService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDbFactory _db;
        public ClassService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _db = db;
        }
        public ResponseService<string> AddAndSave(ClassDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var checkSchedule = _repository._class.CheckCourseDetail(entity.schedules, entity.teacherId);
                if (checkSchedule == true)
                {
                    var temp = _mapper.Map<ClassDTO, Class>(entity);
                    _repository._class.Add(temp);
                    SaveChanges();
                    response.result = "Add Course Successfully";
                }
                else
                {
                    response.success = false;
                    response.message = "Teacher Has The Same Schedule";
                }
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
                var tempDTO = _repository._class.GetSingleByCondition(p => p.id == id);
                _repository._class.Delete(tempDTO);
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

        public ResponseService<List<ClassDTO>> GetAll()
        {
            var response = new ResponseService<List<ClassDTO>>();
            try
            {
                response.result = _mapper.Map<List<Class>, List<ClassDTO>>(_repository._class.GetAllInfoListClass());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<ClassDTO> GetById(int id)
        {
            var response = new ResponseService<ClassDTO>();
            try
            {
                response.result = _mapper.Map<Class, ClassDTO>(_repository._class.GetSingleByCondition(p => p.id == id));
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

        public ResponseService<string> Update(ClassInfoForTeacher entity)
        {
            var response = new ResponseService<string>();
            var db = _db.Init();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var temp2 = _repository._schedule.GetMulti(p => p.classId == entity.id);
                    var temp = temp2.Where(p => p.classId == entity.id).FirstOrDefault();
                    if (temp == null)
                    {                      
                        foreach (var item in entity.schedules)
                        {
                            if (item.day != null)
                            {
                                Schedule schedule = new Schedule()
                                {

                                    classId = entity.id,
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
                    var course = _mapper.Map<ClassInfoForTeacher, ClassUpdateDTO>(entity);
                    _repository._class.Update(_mapper.Map<ClassUpdateDTO, Class>(course));
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

        public ResponseService<string> Add(ClassDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<CourseDTO>> GetAllCourseNoOneRegister(string studentId)
        {
            var response = new ResponseService<List<CourseDTO>>();
            try
            {
                var classDetails = _repository._classDetailOfStudent.GetAllInFormation();
                var temp = classDetails.Where(p => p.studentId == studentId && p.finish==false).ToList();
                foreach(var item in temp)
                {
                    if (classDetails.Where(p => p.classId == item.classId).FirstOrDefault()!=null)
                    {
                        classDetails.RemoveAll(p=>p.classId == item.classId);
                    }
                }
                var result = _mapper.Map<IEnumerable<Class>, List<ClassInfoForTeacher>>(classDetails.Select(x=>x.classes).Distinct());
                if (result.Count() != 0)
                {
                    response.result = new List<CourseDTO>();
                    foreach (var item in result)
                    {
                        var temp1 = _mapper.Map<Course, CourseDTO>(_repository._course.GetSingleByCondition(p => p.id == item.courseId));

                        response.result.Add(temp1);
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

        public ResponseService<List<ClassDTO>> GetAll(int departmentId)
        {
            var response = new ResponseService<List<ClassDTO>>();
            try
            {
                var temp = _repository._class.GetAllInfoListClass().Where(p=>p.departmentId==departmentId && p.teacherId == null).ToList();
                response.result = _mapper.Map<List<Class>, List<ClassDTO>>(temp);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<List<ClassDTO>> GetAll(int deparmentId, int courseId)
        {
            var response = new ResponseService<List<ClassDTO>>();
            try
            {
                var result = _repository._class.GetMulti(p => p.departmentId == deparmentId && p.courseId == courseId);
                response.result=_mapper.Map<List<Class>, List<ClassDTO>>(result);
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<List<ClassInfoForTeacher>> GetAllInfoOfTeacher(string userId)
        {
            var response = new ResponseService<List<ClassInfoForTeacher>>();
            try
            {
                var result = _repository._class.GetAllInfoCoursForTeacher(userId);
                response.result = _mapper.Map<List<Class>, List<ClassInfoForTeacher>>(result);
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<List<ClassDetailOfStudentDTO>> GetAllInfoOfStudent(string userId)
        {
            var response = new ResponseService<List<ClassDetailOfStudentDTO>>();
            try
            {
                var result = _repository._classDetailOfStudent.GetAllInFormation(userId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ClassDetailOfStudentDTO>>(result);
                foreach (var item in response.result)
                {
                    item.classes.schedules = _mapper.Map<List<Schedule>, List<ScheduleDTO>>(_repository._schedule.GetMulti(p => p.classId == item.classes.id));
                    item.classes.departments = _mapper.Map<Department, DepartmentDTO>(_repository._department.GetSingleByCondition(p => p.id == item.classes.departmentId));
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> Update(ClassDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
