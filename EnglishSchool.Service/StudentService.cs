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
using System.Threading.Tasks;

namespace EnglishSchool.Service
{
        public interface IStudentService : IServiceBase<StudentDTO>
    {
        ResponseService<StudentDTO> GetById(string id);
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
       

        public ResponseService<string> AddAndSave(StudentDTO entity)
        {
            var response = new ResponseService<string>();
            var tempId = _repository._student.GetLastStudentId()+1;
            Account account = new Account()
            {
                userName = entity.departmentId.ToString() + "." + String.Format("{0:D5}", tempId) + ".vn",
                password = entity.firstName.First().ToString().ToUpper() + entity.firstName.Substring(1).ToLower() + entity.lastName.First().ToString().ToUpper() + "@" + entity.phoneNumber.Substring(6),
                role = 2,
                status = "available"
            };
            var db = _db.Init();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    _repository._account.Add(account);
                    db.SaveChanges();
                    entity.userName = account.userName;
                    entity.studentId = String.Format("{0:D5}", tempId);
                    _repository._student.Add(_mapper.Map<StudentDTO, Student>(entity));
                    db.SaveChanges();
                    var tempCourse = _repository._course.GetSingleByCondition(p=>p.id==entity.courseId);
                    CourseDetailOfStudent courseOfStudent = new CourseDetailOfStudent()
                    {
                        courseId = tempCourse.id,
                        studentId = entity.studentId,
                        dayStart = DateTime.Now,
                        dayFinish = DateTime.Now.AddMonths(tempCourse.numberOfMonths),
                        finish = false,
                    };
                    _repository._courseDetailOfStudent.Add(courseOfStudent);
                    db.SaveChanges();
                    transaction.Commit();
                    response.result = "Add Student Successfully";
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

        public ResponseService<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseService<List<StudentDTO>> GetAll()
        {
            ResponseService<List<StudentDTO>> response = new ResponseService<List<StudentDTO>>();
            try
            {
                response.result = _mapper.Map<List<Student>, List<StudentDTO>>(_repository._student.GetAll());
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = "Student Error" + ex.Message;
            }
            return response;
        }

        public ResponseService<StudentDTO> GetById(string id)
        {
            ResponseService<StudentDTO> response = new ResponseService<StudentDTO>();
            try
            {
                response.result = _mapper.Map<Student, StudentDTO>(_repository._student.GetSingleByCondition(p => p.studentId == id));
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

        public ResponseService<string> Update(StudentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<string> Add(StudentDTO entity)
        {
            throw new NotImplementedException();
        }

        public ResponseService<StudentDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
