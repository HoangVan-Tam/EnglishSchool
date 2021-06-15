using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EnglishSchool.Data.Repositories
{
    public interface IClassRepository : IRepository<Class>
    {
        int GetLastCourseId();
        List<Class> GetAllInfoListClass(Expression<Func<Class, bool>> expression);
        List<Class> GetAllInfoListClass();
        List<Class> GetAllInfoCoursForTeacher(string userId);
        Class GetCourseWithSchedule(int courseId);
        List<Class> GetAllCourseNoOneRegister(string studentId);
        bool CheckCourseDetail(List<ScheduleDTO> schedules, string teacherId);
    }
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public int GetLastCourseId()
        {
            return db.Class.ToList().Last().id;
        }
        public List<Class> GetAllInfoListClass(Expression<Func<Class, bool>> expression)
        {
            return db.Class.Include("classDetailOfStudents").Include("schedules").Include("departments").Include("courses").Include("employees").Where(expression).ToList();
        }

        public List<Class> GetAllInfoListClass()
        {
            return db.Class.Include("classDetailOfStudents").Include("schedules").Include("departments").Include("courses").Include("employees").ToList();
        }

        public List<Class> GetAllCourseNoOneRegister(string studentId)
        {
            return db.Class.Include("courseDetailOfStudents").ToList();
        }

        public Class GetCourseWithSchedule(int courseId)
        {
            return db.Class.Include("schedules").Where(p=>p.id==courseId).FirstOrDefault();
        }

        public bool CheckCourseDetail(List<ScheduleDTO> schedules, string teacherId)
        {
            var lstcourseofteacher = db.Class.Include("schedules").Where(p => p.teacherId == teacherId).ToList();
            for(int i = 0; i < lstcourseofteacher.Count(); i++)
            {
                for(int j = 0; j < schedules.Count; j++)
                {
                    var timeStart=Convert.ToDateTime(lstcourseofteacher[i].schedules[j].timeStart);
                    var timeEnd = Convert.ToDateTime(lstcourseofteacher[i].schedules[j].timeEnd);
                    var timeStartRegister = Convert.ToDateTime(schedules[j].timeStart);
                    var timeEndRegister = Convert.ToDateTime(schedules[j].timeStart);
                    if (lstcourseofteacher[i].schedules[j].day==schedules[j].day && (timeStart<=timeStartRegister && timeEnd>=timeStartRegister || timeStart>=timeEndRegister && timeEndRegister<=timeEnd)==true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Class> GetAllInfoCoursForTeacher(string userId)
        {
            return db.Class.Include("classDetailOfStudents").Include("schedules").Include("departments").Include("courses").Include("employees").Where(p=>p.teacherId==userId).ToList();
        }
    }
}
