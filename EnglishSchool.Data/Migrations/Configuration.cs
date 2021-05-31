namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EnglishSchool.Data.EnglishSchoolDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EnglishSchool.Data.EnglishSchoolDbContext context)
        {
            //context.CourseDetailOfEmployee.Add(new Model.Models.CourseDetailOfEmployee() { teacherId = "02_giaovien_1", courseId = 13 });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
