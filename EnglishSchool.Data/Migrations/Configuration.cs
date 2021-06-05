namespace EnglishSchool.Data.Migrations
{
    using EnglishSchool.Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EnglishSchool.Data.EnglishSchoolDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EnglishSchool.Data.EnglishSchoolDbContext context)
        {
            context.Employee.Add(
                new Employee
                {
                    userId = "administrator",
                    status = true,
                    address = "Củ Chi",
                    birthday = new DateTime(1999, 09, 29),
                    email = "hoangvantam.2909@gmail.com",
                    firstName = "Văn Tâm",
                    lastName = "Hoàng",
                    phoneNumber = "0385154427",
                    role = "Admin",
                    sex = true,
                    password = BCrypt.Net.BCrypt.HashPassword("administrator"),
                    departmentId=13
                }
            );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
