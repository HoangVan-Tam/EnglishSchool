using EnglishSchool.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseDetailOfStudent> CourseDetailOfStudent { get; set; }
        public DbSet<Parent> parents { get; set; }
        public DbSet<ScoreResult> scoreResults { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
