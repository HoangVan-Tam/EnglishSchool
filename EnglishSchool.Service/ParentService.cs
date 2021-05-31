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
    public interface IParentService : IServiceBase<ParentDTO>
    {
        ResponseService<bool> AddStudentOfParent(string student, string parentId);
        ResponseService<ManageStudentDTO> ManageStudent(string studentId, int courseId);
    }
    public class ParentService : IParentService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDbFactory _db;
        public ParentService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper, IDbFactory db)
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

        public ResponseService<string> AddAndSave(ParentDTO entity)
        {
            var response = new ResponseService<string>();
            var tempId = _repository._parent.GetLastParentId() + 1;
            var student = _repository._student.GetSingleByCondition(p => p.studentId == entity.studentId);
            var checkParent = _repository._parent.GetSingleByCondition(p => p.phoneNumber == entity.phoneNumber);
            if (checkParent != null)
            {
                response.success = false;
                response.message = "Parent was created";
            }
            else
            {
                var db = _db.Init();
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var parent = _mapper.Map<ParentDTO, Parent>(entity);
                        parent.parentId = "par" + "-" + String.Format("{0:D6}", tempId);
                        var firstName = convertToUnSign3(parent.firstName);
                        var lastName = convertToUnSign3(parent.lastName);
                        parent.password = lastName.First().ToString().ToUpper() + lastName.Substring(1).ToLower()
                                                + firstName.First().ToString().ToUpper() + "@" + parent.phoneNumber.Substring(6);
                        parent.password = BCrypt.Net.BCrypt.HashPassword(parent.password);
                        parent.status = true;
                        parent.students = new List<Student>();
                        parent.students.Add(student);
                        _repository._parent.Add(parent);
                        SaveChanges();
                        student.parentId = parent.parentId;
                        _repository._student.Update(student);
                        SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.success = false;
                        response.message = ex.Message;
                    }
                }
            }

            return response;
        }


        public ResponseService<string> Add(ParentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Update(ParentDTO entity)
        {
            var response = new ResponseService<string>();
            try
            {
                var temp = _repository._parent.GetSingleByCondition(p => p.parentId == entity.parentId);
                temp.lastName = entity.lastName;
                temp.firstName = entity.firstName;
                temp.birthday = entity.birthday;
                temp.sex = entity.sex;
                temp.phoneNumber = entity.phoneNumber;
                temp.email = entity.email;
                _repository._parent.Update(temp);
                SaveChanges();
                response.result = "Update Parent successfully";
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

        public ResponseService<List<ParentDTO>> GetAll()
        {
            var response = new ResponseService<List<ParentDTO>>();
            try
            {
                response.result = _mapper.Map<List<Parent>, List<ParentDTO>>(_repository._parent.GetAll());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseService<ParentDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ResponseService<bool> AddStudentOfParent(string student, string parentId)
        {
            var response = new ResponseService<bool>();
            try
            {
                var temp = _repository._student.GetSingleByCondition(p => p.studentId == student);
                if (temp.parentId == null)
                {
                    temp.parentId = parentId;
                    _repository._student.Update(temp);
                    response.result = true;
                }
                else
                {
                    response.result = false;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }



        public ResponseService<ManageStudentDTO> ManageStudent(string studentId, int courseId)
        {
            var response = new ResponseService<ManageStudentDTO>();
            try
            {
                var result = _repository._courseDetailOfStudent.GetAllInFormation(studentId, courseId);
                response.result = _mapper.Map<CourseDetailOfStudent, ManageStudentDTO>(result);
                var temp = _repository._course.GetCourseWithSchedule(result.courseId).schedules.Count();
                
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
