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
using System.Text.RegularExpressions;

namespace EnglishSchool.Service
{
    public interface IStudentService : IServiceBase<FullInfoStudentDTO>
    {
        ResponseService<FullInfoStudentDTO> GetById(string id);
        ResponseService<string> StudentRegisterCourse(StudentRegisterCourse student);
        ResponseService<ManageStudentDTO> ManageStudent(string studentId, int classId);

    }
    public class StudentService : IStudentService
    {

        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDbFactory _db;
        public StudentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _db = db;
        }


        public string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public ResponseService<string> AddAndSave(FullInfoStudentDTO entity)
        {
            var response = new ResponseService<string>();
            var tempId = _repository._student.GetLastStudentId(entity.departmentId) + 1;

            var tempCourse = _repository._course.GetSingleByCondition(p => p.id == entity.courseId);
            if (tempCourse == null)
            {
                response.message = "Class is not created";
                response.success = false;
                return response;
            }
            var db = _db.Init();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var tempStudent = _mapper.Map<FullInfoStudentDTO, Student>(entity);
                    tempStudent.studentId = "stu" + String.Format("{0:D2}", tempStudent.departmentId) + "-" + String.Format("{0:D6}", tempId);
                    var firstName = convertToUnSign3(tempStudent.firstName);
                    var lastName = convertToUnSign3(tempStudent.lastName);
                    
                    tempStudent.password = "123456789";
                    tempStudent.status = true;
                    tempStudent.deactivationDate = DateTime.Now.AddDays(tempCourse.numberOfWeeks*7);
                    tempStudent.password = BCrypt.Net.BCrypt.HashPassword(tempStudent.password);
                    _repository._student.Add(tempStudent);
                    db.SaveChanges();

                    ClassDetailOfStudent courseOfStudent = new ClassDetailOfStudent()
                    {
                        dayFinish = DateTime.Now.AddDays(tempCourse.numberOfWeeks*7),
                        dayStart = DateTime.Now,
                        finish = false,
                        classId = entity.classId,
                        studentId = tempStudent.studentId,
                        tuition = (tempCourse.tuition - (tempCourse.tuition / 100 * tempCourse.discount)),
                    };
                    _repository._classDetailOfStudent.Add(courseOfStudent, tempCourse.numberOfWeeks);
                    db.SaveChanges();
                    transaction.Commit();
                    response.result = "Add Student Successfully";
                    db.Dispose();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    response.success = false;
                    response.message = "Student Error " + ex.Message;
                }
            }
            return response;
        }


        public ResponseService<List<FullInfoStudentDTO>> GetAll()
        {
            ResponseService<List<FullInfoStudentDTO>> response = new ResponseService<List<FullInfoStudentDTO>>();
            try
            {
                response.result = _mapper.Map<List<Student>, List<FullInfoStudentDTO>>(_repository._student.GetAllInFomation());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = "Student Error" + ex.Message;
            }
            return response;
        }

        public ResponseService<FullInfoStudentDTO> GetById(string id)
        {
            ResponseService<FullInfoStudentDTO> response = new ResponseService<FullInfoStudentDTO>();
            try
            {
                response.result = _mapper.Map<Student, FullInfoStudentDTO>(_repository._student.GetAllInfoById(id));
            }
            catch (Exception ex)
            {
                response.message = "Student Error " + ex.Message;
                response.success = false;
            }
            return response;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ResponseService<string> Update(FullInfoStudentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var student = _repository._student.GetSingleByCondition(p => p.studentId == entity.studentId);
                student.firstName = entity.firstName;
                student.lastName = entity.lastName;
                student.sex = entity.sex;
                student.birthday = entity.birthday;
                student.address = entity.address;
                student.phoneNumber = entity.phoneNumber;
                student.email = entity.email;
                student.level = entity.level;
                student.departmentId = entity.departmentId;
                _repository._student.Update(student);
                SaveChanges();
                response.result = "Update Student successfully";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> StudentRegisterCourse(StudentRegisterCourse student)
        {
            var response = new ResponseService<string>();
            var courseDetail = _repository._classDetailOfStudent.GetSingleByCondition(p => p.classId == student.classId && p.studentId == student.studentId && p.finish == false);
            if (courseDetail == null)
            {
                var schedule = _repository._schedule.GetMulti(p => p.classId == student.classId);
                var checkSchedule = _repository._student.CheckCourseDetail(schedule, student.studentId);
                if (checkSchedule == true)
                {
                    var classes = _repository._class.GetSingleByCondition(p => p.id == student.classId);
                    var course = _repository._course.GetSingleByCondition(p => p.id == student.courseId);
                    var db = _db.Init();
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ClassDetailOfStudent courseDetailOfStudent = new ClassDetailOfStudent()
                            {
                                classId = student.classId,
                                dayStart = DateTime.Now.Date,
                                dayFinish = DateTime.Now.AddMonths(course.numberOfWeeks),
                                finish = false,
                                studentId = student.studentId,
                                tuition = (course.tuition - (course.tuition / 100 * course.discount)),
                            };
                            _repository._classDetailOfStudent.Add(courseDetailOfStudent, course.numberOfWeeks);
                            SaveChanges();
                            var tempStudent = _repository._student.GetSingleByCondition(p => p.studentId == student.studentId);
                            if (tempStudent.deactivationDate < courseDetailOfStudent.dayFinish)
                            {
                                tempStudent.deactivationDate = courseDetailOfStudent.dayFinish;
                            }
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
                    response.message = "Student has the same class schedule";
                }
            }
            else
            {
                response.success = false;
                response.message = "Student has not completed the course";
            }
            return response;
        }

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Add(FullInfoStudentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<FullInfoStudentDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<ManageStudentDTO> ManageStudent(string studentId, int classId)
        {
            var response = new ResponseService<ManageStudentDTO>();
            try
            {
                var result = _repository._classDetailOfStudent.GetAllInFormation(studentId, classId);
                response.result = _mapper.Map<ClassDetailOfStudent, ManageStudentDTO>(result);
                var temp = _repository._class.GetCourseWithSchedule(result.classId).schedules.Count;

                foreach (var item in response.result.tests)
                {
                    item.attendances = new List<AttendanceOfStudent>();
                    item.attendances = _mapper.Map<List<Attendance>, List<AttendanceOfStudent>>(result.attendances.Where(p => p.date.Date >= item.startDay.Date && p.date <= item.finishDay.Date).ToList());
                    if (item.attendances.Count < temp)
                    {
                        var temp1 = item.attendances.Count;
                        for (int i = 0; i < temp - temp1; i++)
                        {
                            item.attendances.Add(null);
                        }
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
