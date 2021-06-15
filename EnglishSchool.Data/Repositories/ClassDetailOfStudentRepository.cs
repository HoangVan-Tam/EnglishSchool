using EnglishSchool.Data.Infracstructure;
using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EnglishSchool.Data.Repositories
{
    public interface IClassDetailOfStudentRepository : IRepository<ClassDetailOfStudent>
    {
        List<ClassDetailOfStudent> GetAllInFormation();
        List<ClassDetailOfStudent> GetAllInFormation(string studentId);
        List<ClassDetailOfStudent> GetAllInFormation(int classId);
        ClassDetailOfStudent GetAllInFormation(string studentId, int classId);
        List<ClassDetailOfStudent> GetAllInFormationById(Expression<Func<ClassDetailOfStudent, bool>> expression);
        void Add(ClassDetailOfStudent entity, int numberOfMonth);
        List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourse(int classId);
        List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourse(int classId, string studentId);
        List<ClassDetailOfStudent> GetAllInFormationAddCourseInfo();
        List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourseVer2(int classId);
    }
    public class ClassDetailOfStudentRepository : RepositoryBase<ClassDetailOfStudent>, IClassDetailOfStudentRepository
    {
        public ClassDetailOfStudentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public List<ClassDetailOfStudent> GetAllInFormation(int classId)
        {
            return db.ClassDetailOfStudent.Include("students").Where(p=>p.classId== classId && p.finish==false).ToList();
        }
        public List<ClassDetailOfStudent> GetAllInFormation()
        {
            return db.ClassDetailOfStudent.Include("classes").Include("students").ToList();
        }

        public ClassDetailOfStudent GetAllInFormation(string studentId, int courseId)
        {
            return db.ClassDetailOfStudent.Include("attendances").Include("tests").Where(p=>p.studentId==studentId && p.classId==courseId && p.finish==false).FirstOrDefault();
        }

        public List<ClassDetailOfStudent> GetAllInFormationAddCourseInfo()
        {
            return db.ClassDetailOfStudent.Include("courses").ToList();
        }

        public List<ClassDetailOfStudent> GetAllInFormationById(Expression<Func<ClassDetailOfStudent, bool>> expression)
        {
            return db.ClassDetailOfStudent.Include("classes").Include("students").Where(expression).ToList();
        }

        public virtual void Add(ClassDetailOfStudent entity, int numberOfMonth)
        {
            DateTime startday = new DateTime(2021,05,31);
            /*if (startday.DayOfWeek.ToString() != "Monday")
            {
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)startday.DayOfWeek + 7) % 7;
                startday = startday.AddDays(daysUntilMonday).Date;
            }*/
            var id = db.ClassDetailOfStudent.Add(entity).courseDetailId;
            for (int i = 0; i < numberOfMonth; i++)
            {
                Test test = new Test()
                {
                    week = i + 1,
                    status = "Chưa Thi",
                    startDay = startday.Date,
                    finishDay = startday.AddDays(6).Date,
                    score = 0,
                    courseDetailId = id,
                };
                db.Test.Add(test);
                startday = test.finishDay.AddDays(1).Date;
            }
        }

        public List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourse(int classId)
        {
            return db.ClassDetailOfStudent.Include("attendances").Include("students").Where(p => p.classId == classId).ToList();
        }

        public List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourseVer2(int classId)
        {
            return db.ClassDetailOfStudent.Include("attendances").Include("students").Include("tests").Where(p => p.classId == classId).ToList();
        }

        public List<ClassDetailOfStudent> GetAllAttendanceStudentOfCourse(int classId, string studentId)
        {
            return db.ClassDetailOfStudent.Include("attendances").Include("students").Where(p => p.classId == classId && p.studentId==studentId).ToList();
        }

        public List<ClassDetailOfStudent> GetAllInFormation(string studentId)
        {
            return db.ClassDetailOfStudent.Include("classes").Include("students").Where(p=>p.studentId==studentId).ToList();
        }
    }
}
