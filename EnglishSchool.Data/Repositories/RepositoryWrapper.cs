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
        ITestRepository _test { get; }
        IParentRepository _parent { get; }
        IDepartmentRepository _department { get; }
        IRecruitRepository _recruitment { get; }
        IStudentRepository _student { get; }
        ICourseRepository _course { get; }
        ICourseDetailOfStudentRepository _courseDetailOfStudent { get; }
        INewsRepository _news { get; }
        IQuestionRepository _question { get; }
        IPersonalInformationRepository _personalInformation { get; }
        IEmployeeRepository _employee { get; }
        IDetailTestRepository _detailTest { get; }
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IDbFactory _dbFactory;
        private DepartmentRepository departmentRepository;
        private RecruitmentRepository recruitmentRepository;
        private StudentRepository studentRepository;
        private CourseRepository courseRepository;
        private CourseDetailOfStudentRepository courseDetailOfStudentRepository;
        private ParentRepository parentRepository;
        private TestRepository testRepository;
        private NewsRepository newsRepository;
        private QuestionRepository questionRepository;
        private PersonalInformationRepository personalInformationRepository;
        private EmployeeRepository employeeRepository;
        private DetailTestRepository detailTestRepository;

        public RepositoryWrapper(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public IEmployeeRepository _employee
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository(_dbFactory);
                }
                return employeeRepository;
            }
        }

        public IPersonalInformationRepository _personalInformation
        {
            get
            {
                if (personalInformationRepository == null)
                {
                    personalInformationRepository = new PersonalInformationRepository(_dbFactory);
                }
                return personalInformationRepository;
            }
        }
        public IQuestionRepository _question
        {
            get
            {
                if (questionRepository == null)
                {
                    questionRepository = new QuestionRepository(_dbFactory);
                }
                return questionRepository;
            }
        }
        public INewsRepository _news
        {
            get
            {
                if (newsRepository == null)
                {
                    newsRepository = new NewsRepository(_dbFactory);
                }
                return newsRepository;
            }
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

        public ITestRepository _test
        {   
            get
            {
                if (testRepository == null)
                {
                    testRepository = new TestRepository(_dbFactory);
                }
                return testRepository;
            }
        }

        public IDetailTestRepository _detailTest
        {
            get
            {
                if (detailTestRepository == null)
                {
                    detailTestRepository = new DetailTestRepository(_dbFactory);
                }
                return detailTestRepository;
            }
        }
    }
}
