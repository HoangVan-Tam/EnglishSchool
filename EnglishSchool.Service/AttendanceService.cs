using AutoMapper;
using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Model.ResponseService;
using System;
using System.Collections.Generic;

namespace EnglishSchool.Service
{
    public interface IAttendanceService
    {
        ResponseService<List<ListCourseDetailOfStudent>> GetListStudent(int courseId);
        ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId);
        ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId, string studentId);
        ResponseService<string> AttendanceStudentOfCourse(List<AttendanceDTO> attendances, int courseId);
    }
    public class AttendanceService : IAttendanceService
    {
        private IRepositoryWrapper _repository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AttendanceService(IRepositoryWrapper repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ResponseService<List<ListCourseDetailOfStudent>> GetListStudent(int courseId)
        {
            var response = new ResponseService<List<ListCourseDetailOfStudent>>();
            try
            {
                var courseDetailOfStudent = _repository._courseDetailOfStudent.GetAllInFormation(courseId);
                response.result = _mapper.Map<List<CourseDetailOfStudent>, List<ListCourseDetailOfStudent>>(courseDetailOfStudent);
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> AttendanceStudentOfCourse(List<AttendanceDTO> attendances, int courseId)
        {
            var response = new ResponseService<string>();
            try
            {
                var listStudent = _repository._courseDetailOfStudent.GetMulti(p => p.courseId == courseId && p.finish==false);
                for(int i = 0; i < attendances.Count; i++)
                {
                    var temp = listStudent.Find(p => p.studentId == attendances[0].studentId);
                    if (temp != null)
                    {
                        Attendance attendance = new Attendance();
                        attendance.absent = attendances[0].absent;
                        attendance.date = attendances[0].date;
                        attendance.reason = attendances[0].reason;
                        attendance.courseDetailId = temp.courseDetailId;
                        _repository._attendance.Add(attendance);
                    }
                }
                SaveChange();
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId)
        {
            var response = new ResponseService<List<ListAttendanceStudentOfCourse>>();
            try
            {
                var result = _repository._courseDetailOfStudent.GetAllAttendanceStudentOfCourse(courseId);
                response.result = _mapper.Map<List<CourseDetailOfStudent>, List<ListAttendanceStudentOfCourse>>(result);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId, string studentId)
        {
            var response = new ResponseService<List<ListAttendanceStudentOfCourse>>();
            try
            {
                var result = _repository._courseDetailOfStudent.GetAllAttendanceStudentOfCourse(courseId, studentId);
                response.result = _mapper.Map<List<CourseDetailOfStudent>, List<ListAttendanceStudentOfCourse>>(result);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }
    }
}
