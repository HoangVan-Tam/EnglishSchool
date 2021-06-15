using EnglishSchool.Data.Infracstructure;

namespace EnglishSchool.Data.Repositories
{
    public interface IRepositoryWrapper
    {
        IAttendanceRepository _attendance { get; }
        ITestRepository _test { get; }
        IDepartmentRepository _department { get; }
        IRecruitRepository _recruitment { get; }
        IStudentRepository _student { get; }
        IClassRepository _class { get; }
        IClassDetailOfStudentRepository _classDetailOfStudent { get; }
        INewsRepository _news { get; }
        IQuestionRepository _question { get; }
        IPersonalInformationRepository _personalInformation { get; }
        IEmployeeRepository _employee { get; }
        IDetailTestRepository _detailTest { get; }
        IScheduleRepository _schedule { get; }
        ICourseRepository _course { get; }
    }
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IDbFactory _dbFactory;
        private AttendanceRepository attendanceRepository;
        private DepartmentRepository departmentRepository;
        private RecruitmentRepository recruitmentRepository;
        private StudentRepository studentRepository;
        private ClassRepository classRepository;
        private ClassDetailOfStudentRepository classDetailOfStudentRepository;
        private TestRepository testRepository;
        private NewsRepository newsRepository;
        private QuestionRepository questionRepository;
        private PersonalInformationRepository personalInformationRepository;
        private EmployeeRepository employeeRepository;
        private DetailTestRepository detailTestRepository;
        private ScheduleRepository scheduleRepository;
        private CourseRepository courseRepository;

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

        public IClassRepository _class
        {
            get
            {
                if (classRepository == null)
                {
                    classRepository = new ClassRepository(_dbFactory);
                }
                return classRepository;
            }
        }

        public IClassDetailOfStudentRepository _classDetailOfStudent
        {
            get
            {
                if (classDetailOfStudentRepository == null)
                {
                    classDetailOfStudentRepository = new ClassDetailOfStudentRepository(_dbFactory);
                }
                return classDetailOfStudentRepository;
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

        public IAttendanceRepository _attendance 
        {
            get
            {
                if (attendanceRepository == null)
                {
                    attendanceRepository = new AttendanceRepository(_dbFactory);
                }
                return attendanceRepository;
            }
        }

        public IScheduleRepository _schedule
        {
            get
            {
                if (scheduleRepository == null)
                {
                    scheduleRepository = new ScheduleRepository(_dbFactory);
                }
                return scheduleRepository;
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
    }
}
