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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EnglishSchool.Service
{
    public interface IStudentService : IServiceBase<FullInfoStudentDTO>
    {
        ResponseService<FullInfoStudentDTO> GetById(string id);
        ResponseService<string> StudentRegisterCourse(StudentRegisterCourse student);
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
            var tempId = _repository._student.GetLastStudentId()+1;
            var tempCourse = _repository._course.GetSingleByCondition(p => p.id == entity.courseId);
            if (tempCourse == null)
            {
                response.message = "Course is not created";
                response.success = false;
                return response;
            }
            var tempStudent = _mapper.Map<FullInfoStudentDTO, Student>(entity);
            tempStudent.studentId="stu"+ String.Format("{0:D2}", tempStudent.departmentId)+"-"+ String.Format("{0:D6}", tempId);
            var firstName = convertToUnSign3(tempStudent.firstName);
            var lastName = convertToUnSign3(tempStudent.lastName);
            tempStudent.password = firstName.First().ToString().ToUpper() + firstName.Substring(1).ToLower()
                                    + lastName.First().ToString().ToUpper() + "@" + tempStudent.phoneNumber.Substring(6);
            /*
            tempStudent.studentId = "stu"+entity.GetValue("departmentId").ToString() + "-" + String.Format("{0:D6}", tempId);
            tempStudent.password = entity.GetValue("firstName").ToString().First().ToString().ToUpper() + entity.GetValue("firstName").ToString().Substring(1).ToLower() 
                                    + entity.GetValue("lastName").ToString().First().ToString().ToUpper() + "@" + entity.GetValue("phoneNumber").ToString().Substring(6);
            */

            tempStudent.status = true;
            tempStudent.deactivationDate = DateTime.Now.AddMonths(tempCourse.numberOfMonths);
            var db = _db.Init();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    tempStudent.password = BCrypt.Net.BCrypt.HashPassword(tempStudent.password);
                    _repository._student.Add(tempStudent);
                    db.SaveChanges();

                    CourseDetailOfStudent courseOfStudent = new CourseDetailOfStudent()
                    {
                        dayFinish = DateTime.Now.AddMonths(tempCourse.numberOfMonths),
                        dayStart = DateTime.Now,
                        finish = false,
                        courseId = tempCourse.id,
                        studentId = tempStudent.studentId,
                        tuition = tempCourse.tuition / 100 * tempCourse.discount
                    };
                    _repository._courseDetailOfStudent.Add(courseOfStudent);
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
                response.result = _mapper.Map<List<Student>, List<FullInfoStudentDTO>>(_repository._student.GetAll());
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
                response.result = _mapper.Map<Student, FullInfoStudentDTO>(_repository._student.GetSingleByCondition(p => p.studentId == id));
            }
            catch(Exception ex)
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
            var response =new ResponseService<string>();
            try
            {
                _repository._student.Update(_mapper.Map<FullInfoStudentDTO, Student>(entity));
                SaveChanges();
                response.result = "Update Student successfully";
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<string> StudentRegisterCourse(StudentRegisterCourse student)
        {
            var response = new ResponseService<string>();
            var courseDetail = _repository._courseDetailOfStudent.GetSingleByCondition(p => p.courseId == student.courseId && p.studentId == student.studentId);
            if (courseDetail == null || courseDetail.finish == true)
            {
                var course = _repository._course.GetSingleByCondition(p => p.id == student.courseId);
                var db = _db.Init();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        CourseDetailOfStudent courseDetailOfStudent = new CourseDetailOfStudent()
                        {
                            courseId = student.courseId,
                            dayStart = DateTime.Now,
                            dayFinish = DateTime.Now.AddMonths(course.numberOfMonths),
                            finish = false,
                            studentId = student.studentId,
                            tuition = course.tuition + course.tuition / 100 * course.discount,
                        };
                        _repository._courseDetailOfStudent.Add(courseDetailOfStudent);
                        SaveChanges();
                        var tempStudent = _repository._student.GetSingleByCondition(p => p.studentId == student.studentId);
                        tempStudent.deactivationDate = courseDetailOfStudent.dayFinish;
                        SaveChanges();
                        transaction.Commit();
                        response.result = "Register Successfully";
                        db.Dispose();
                    }
                    catch(Exception ex)
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


    }
}
