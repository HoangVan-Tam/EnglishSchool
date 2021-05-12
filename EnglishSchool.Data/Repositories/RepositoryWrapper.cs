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
        IScoreResultRepository _scoreResult { get; }
        IParentRepository _parent { get; }
        IDepartmentRepository _department { get; }
        IRecruitRepository _recruitment { get; }
        IStudentRepository _student { get; }
        ICourseRepository _course { get; }
        ICourseDetailOfStudentRepository _courseDetailOfStudent { get; }
        INewsRepository _news { get; }
        IQuestionRepository _question { get; }
        IPersonalInformationRepository _personalInformation { get; }
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
        private ScoreResultRepository scoreResultRepository;
        private NewsRepository newsRepository;
        private QuestionRepository questionRepository;
        private PersonalInformationRepository personalInformationRepository;

        public RepositoryWrapper(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
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

        public IScoreResultRepository _scoreResult
        {   
            get
            {
                if (scoreResultRepository == null)
                {
                    scoreResultRepository = new ScoreResultRepository(_dbFactory);
                }
                return scoreResultRepository;
            }
        }
    }
}
