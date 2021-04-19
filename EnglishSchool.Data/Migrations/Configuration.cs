namespace EnglishSchool.Data.Migrations
{
    using EnglishSchool.Model.Models;
    using System;
    using System.Collections.Generic;
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
            context.Department.Add(new Department() { name = "WSE Gò Vấp", city = "TPHCM", detail = "Trung tâm đầu tiên tại Việt Nam", address = "Phan Văn Trị, Gò Vấp, TPHCM" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });

            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            context.Questions.Add(new Question() { questionDetail = "What _____ you do? ", answer1 = "do", answer2 = "does", answer3 = "are", answer4 = "is", level = 1, rightAnswer = "do" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
