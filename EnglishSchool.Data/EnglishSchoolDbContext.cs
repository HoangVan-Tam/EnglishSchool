using EnglishSchool.Model.Models;
using System.Data.Entity;

namespace EnglishSchool.Data
{
    public class EnglishSchoolDbContext : DbContext
    {
        public EnglishSchoolDbContext() : base("school")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<News> News { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Recruitment> Recruitment { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<ClassDetailOfStudent> ClassDetailOfStudent { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<DetailTest> DetailTest { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}
