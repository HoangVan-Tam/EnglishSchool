using EnglishSchool.Data.Infracstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishSchool.Data.Repositories
{
    public interface IRepositoryWrapper
    {
        IParentRepository _parent { get; }
        IDepartmentRepository _department { get; }
        IRecruitRepository _recruitment { get; }
        IRecruitmentDetailRepository _listRecruitmentDetail { get; }
        IStudentRepository _student { get; }
        ICourseRepository _course { get; }
        ICourseDetailOfStudentRepository _courseDetailOfStudent { get; }
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IDbFactory _dbFactory;
        private DepartmentRepository departmentRepository;
        private RecruitmentRepository recruitmentRepository;
        private RecruitmentDetailRepository listRecruitmentDetailRepository;
        private StudentRepository studentRepository;
        private CourseRepository courseRepository;
        private CourseDetailOfStudentRepository courseDetailOfStudentRepository;
        private ParentRepository parentRepository;

        public RepositoryWrapper(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public IDepartmentRepository _department
        {
            get
            {
                if (departmentRepository == null)
                {
                    departmentRepository = new DepartmentRepository(_dbFactory);
                }
                return departmentRepository;
            }
        }


        public IRecruitRepository _recruitment
        {
            get
            {
                if (recruitmentRepository == null)
                {
                    recruitmentRepository = new RecruitmentRepository(_dbFactory);
                }
                return recruitmentRepository;
            }
        }

        public IRecruitmentDetailRepository _listRecruitmentDetail
        {
            get
            {
                if (listRecruitmentDetailRepository == null)
                {
                    listRecruitmentDetailRepository = new RecruitmentDetailRepository(_dbFactory);
                }
                return listRecruitmentDetailRepository;
            }
        }

        public IStudentRepository _student
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepository(_dbFactory);
                }
                return studentRepository;
            }
        }

        public ICourseRepository _course
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(_dbFactory);
                }
                return courseRepository;
            }
        }

        public ICourseDetailOfStudentRepository _courseDetailOfStudent
        {
            get
            {
                if (courseDetailOfStudentRepository == null)
                {
                    courseDetailOfStudentRepository = new CourseDetailOfStudentRepository(_dbFactory);
                }
                return courseDetailOfStudentRepository;
            }
        }

        public IParentRepository _parent
        {
            get
            {
                if (parentRepository == null)
                {
                    parentRepository = new ParentRepository(_dbFactory);
                }
                return parentRepository;
            }
        }
    }
}
