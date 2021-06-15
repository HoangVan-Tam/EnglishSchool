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
    public interface IEmployeeService : IServiceBase<EmployeeDTO>
    {
        ResponseService<List<EmployeeDTO>> GetAllTeacher();
        ResponseService<List<EmployeeDTO>> GetAllTeacher(int departmentId);
        ResponseService<List<ClassInfoForTeacher>> GetAllCourseOfTeacher(string teacherId);
        ResponseService<EmployeeDTO> GetById(string userId);
        ResponseService<List<TeacherManageStudent>> ManageListStudent(DateTime ngayDauTuan, int courseId);
        ResponseService<List<TeacherManageStudentVer2>> ManageListStudentVer2(DateTime ngayDauTuan, int courseId);
        ResponseService<string> EmployeeRegisterCourse(EmployeeRegisterCourse student);
    }
    public class EmployeeService : IEmployeeService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IDbFactory _db;

        public EmployeeService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
        {
            _db = db;
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
                var temp = _repository._employee.GetLastTeacherId() + 1;
                var teacher = _mapper.Map<EmployeeDTO, Employee>(entity);
                teacher.userId = String.Format("{0:D2}", entity.departmentId) + "_giaovien_" + temp.ToString();
                teacher.password = BCrypt.Net.BCrypt.HashPassword("123456789");
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
                var result = _repository._employee.GetAllInfo().Where(p => p.role == "Teacher").ToList();
                response.result = _mapper.Map<List<Employee>, List<EmployeeDTO>>(result);
            }
            catch (Exception ex)
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

        public ResponseService<List<TeacherManageStudent>> ManageListStudent(DateTime ngayDauTuan, int courseId)
        {
            var response = new ResponseService<List<TeacherManageStudent>>();
            try
            {
                var result = _repository._classDetailOfStudent.GetAllInFormation(courseId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<TeacherManageStudent>>(result);
                var lastDayOfWeek = ngayDauTuan.AddDays(6);
                for (int i = 0; i < response.result.Count(); i++)
                {
                    response.result[i].beginningOfTheWeek = ngayDauTuan;
                    response.result[i].weekend = ngayDauTuan.AddDays(6);
                    var courseDeatilId = response.result[i].courseDetailId;
                    response.result[i].attendances = _mapper.Map<List<Attendance>, List<AttendanceOfStudent>>(_repository._attendance.GetMulti(p => p.courseDetailId == courseDeatilId && p.date >= ngayDauTuan && p.date <= lastDayOfWeek));
                    response.result[i].tests = _mapper.Map<List<Test>, List<Test2DTO>>(_repository._test.GetMulti(p => p.courseDetailId == courseDeatilId && p.startDay >= ngayDauTuan && p.startDay <= lastDayOfWeek));
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<List<ClassInfoForTeacher>> GetAllCourseOfTeacher(string teacherId)
        {
            var response = new ResponseService<List<ClassInfoForTeacher>>();
            try
            {
                var result = _repository._class.GetAllInfoListClass().Where(p=>p.teacherId==teacherId).ToList();
                response.result = _mapper.Map<List<Class>, List<ClassInfoForTeacher>>(result);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<List<TeacherManageStudentVer2>> ManageListStudentVer2(DateTime ngayDauTuan, int courseId)
        {   
            var response = new ResponseService<List<TeacherManageStudentVer2>>();
            try
            {
                var result = _repository._classDetailOfStudent.GetAllAttendanceStudentOfCourseVer2(courseId);
                if (result.Count > 0)
                {
                    response.result = _mapper.Map<List<ClassDetailOfStudent>, List<TeacherManageStudentVer2>>(result);
                    var lastDayOfWeek = ngayDauTuan.AddDays(6);
                    var firstDayOfWeek = ngayDauTuan;
                    var courseSchedule = _repository._class.GetCourseWithSchedule(result[0].classId).schedules.Count();
                    for (int i = 0; i < response.result.Count(); i++)
                    {
                        int temp2 = new int();
                        response.result[i].tests = response.result[i].tests.Where(p => p.startDay.Date <= firstDayOfWeek.Date && p.finishDay >= lastDayOfWeek.Date).ToList();
                        if (response.result[i].tests.Count != 0)
                        {
                            var temp = result[i].attendances.Where(p => p.date.Date >= firstDayOfWeek.Date && p.date.Date <= lastDayOfWeek.Date).ToList();
                            if (temp.Count != 0)
                            {
                                response.result[i].tests[0].attendances = _mapper.Map<List<Attendance>, List<AttendanceOfStudent>>(temp);
                                temp2 = response.result[i].tests[0].attendances.Count();
                            }
                            else
                            {
                                temp2 = 0;
                            }
                        }
                        if (temp2 == 0 || temp2 < courseSchedule)
                        {
                            if (response.result[i].tests[0].attendances == null)
                            {
                                response.result[i].tests[0].attendances = new List<AttendanceOfStudent>();
                            }
                            List<AttendanceOfStudent> lst = new List<AttendanceOfStudent>();
                            for (int j = 0; j < courseSchedule - temp2; j++)
                            {
                                lst.Add(null);
                            }
                            response.result[i].tests[0].attendances.AddRange(lst);
                        }
                    }
                }
                else
                {
                    response.result = null;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }


        public ResponseService<string> EmployeeRegisterCourse(EmployeeRegisterCourse employee)
        {
            return null;
            /*var response = new ResponseService<string>();
            var schedule = _repository._schedule.GetMulti(p => p.courseId == employee.id);
            var checkSchedule = _repository._course.CheckCourseDetail(schedule, employee.userId);
            if (checkSchedule == true)
            {
                var course = _repository._course.GetSingleByCondition(p => p.id == employee.id);
                var db = _db.Init();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        _repository._course.GetSingleByCondition(p => p.id == employee.id).teacherId = employee.userId;
                        SaveChanges();
                        transaction.Commit();
                        response.result = "Register Successfully";
                        db.Dispose();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.success = false;
                        response.message = ex.Message;
                    }

                }
            }
            else
            {
                response.success = false;
                response.message = "Teacher has the same class schedule";
            }
            return response;*/
        }

        public ResponseService<List<EmployeeDTO>> GetAllTeacher(int departmentId)
        {
            var response = new ResponseService<List<EmployeeDTO>>();
            try
            {
                var result = _repository._employee.GetMulti(p => p.departmentId == departmentId && p.role=="Teacher");
                response.result = _mapper.Map<List<Employee>, List<EmployeeDTO>>(result);
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}