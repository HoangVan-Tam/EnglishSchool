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
    public interface IAttendanceService
    {
        ResponseService<List<ListCourseDetailOfStudent>> GetListStudent(int courseId);
        ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId);
        ResponseService<List<ListAttendanceStudentOfCourse>> GetListAttendanceStudentOfCourse(int courseId, string studentId);
        ResponseService<string> AttendanceStudentOfCourse(AttendanceDTO attendances);
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
                var courseDetailOfStudent = _repository._classDetailOfStudent.GetAllInFormation(courseId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ListCourseDetailOfStudent>>(courseDetailOfStudent);
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public ResponseService<string> AttendanceStudentOfCourse(AttendanceDTO attendances)
        {
            var response = new ResponseService<string>();
            try
            {
                var listStudent = _repository._classDetailOfStudent.GetMulti(p => p.classId == attendances.courseId && p.finish==false);
                var courseSchedule = _repository._class.GetCourseWithSchedule(attendances.courseId).schedules;
                var date = DateTime.Now.Date;
                for (int i = 0; i < attendances.attendances.Count; i++)
                {
                    var temp = listStudent.Where(p => p.studentId == attendances.attendances[i].studentId).FirstOrDefault();
                    if (temp != null)
                    {
                        Attendance attendance = new Attendance();
                        if (attendances.session == 1)
                        {
                            if (courseSchedule[0].day == "T2")
                            {
                                attendance.date = attendances.firstDayOfWeek.Date;
                            }
                            else if (courseSchedule[0].day == "T3")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(1);
                            }
                            else if (courseSchedule[0].day == "T4")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(2);
                            }
                            else if (courseSchedule[0].day == "T7")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(5);
                            }
                            else if (courseSchedule[0].day == "CN")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(6);
                            }
                            else if (courseSchedule[0].day == "T5")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(3);
                            }
                            else if (courseSchedule[0].day == "T6")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(4);
                            }
                        }
                        else if (attendances.session == 2)
                        {
                            if (courseSchedule[1].day == "T3")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(1);
                            }
                            else if (courseSchedule[1].day == "T4")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(2);
                            }
                            else if (courseSchedule[1].day == "T5")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(3);
                            }
                            else if (courseSchedule[1].day == "T6")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(4);
                            }
                            else if (courseSchedule[1].day == "CN")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(5);
                            }
                            else if (courseSchedule[1].day == "T7")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(6);
                            }
                        }
                        else
                        {
                            if (courseSchedule[2].day == "T7")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(5);
                            }
                            
                            else if (courseSchedule[2].day == "T6")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(4);
                            }
                            else if (courseSchedule[2].day == "T5")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(3);
                            }
                            else if (courseSchedule[2].day == "CN")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(6);
                            }
                            else if (courseSchedule[2].day == "T4")
                            {
                                attendance.date = attendances.firstDayOfWeek.AddDays(2);
                            }
                        }
                        attendances.firstDayOfWeek= attendances.firstDayOfWeek.Date;
                        _repository._test.GetSingleByCondition(p => p.courseDetailId == temp.courseDetailId && p.startDay == attendances.firstDayOfWeek).comment = attendances.attendances[i].comment;
                        attendance.absent = attendances.attendances[i].absent;
                        attendance.reason = attendances.attendances[i].reason;
                        attendance.courseDetailId = temp.courseDetailId;
                        _repository._attendance.Add(attendance);
                    }
                    SaveChange();
                }  
                response.result="Attendance Successfully";
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
                var result = _repository._classDetailOfStudent.GetAllAttendanceStudentOfCourse(courseId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ListAttendanceStudentOfCourse>>(result);
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
                var result = _repository._classDetailOfStudent.GetAllAttendanceStudentOfCourse(courseId, studentId);
                response.result = _mapper.Map<List<ClassDetailOfStudent>, List<ListAttendanceStudentOfCourse>>(result);
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
