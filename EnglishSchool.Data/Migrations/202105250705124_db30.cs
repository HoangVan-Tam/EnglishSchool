namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScoreResults", "courseDetailId", "dbo.CourseDetailOfStudents");
            DropIndex("dbo.ScoreResults", new[] { "courseDetailId" });
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        testId = c.Int(nullable: false, identity: true),
                        startDay = c.DateTime(nullable: false),
                        finishDay = c.DateTime(nullable: false),
                        status = c.String(nullable: false),
                        score = c.Single(nullable: false),
                        courseDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.testId)
                .ForeignKey("dbo.CourseDetailOfStudents", t => t.courseDetailId, cascadeDelete: true)
                .Index(t => t.courseDetailId);
            
            CreateTable(
                "dbo.DetailTests",
                c => new
                    {
                        testId = c.Int(nullable: false),
                        questionId = c.Int(nullable: false),
                        correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.testId, t.questionId })
                .ForeignKey("dbo.Questions", t => t.questionId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.testId, cascadeDelete: true)
                .Index(t => t.testId)
                .Index(t => t.questionId);
            
            DropTable("dbo.ScoreResults");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScoreResults",
                c => new
                    {
                        scoreResultId = c.Int(nullable: false, identity: true),
                        courseDetailId = c.Int(nullable: false),
                        nameOfExam = c.String(nullable: false),
                        listening = c.Single(nullable: false),
                        speaking = c.Single(nullable: false),
                        reading = c.Single(nullable: false),
                        writing = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.scoreResultId, t.courseDetailId });
            
            DropForeignKey("dbo.DetailTests", "testId", "dbo.Tests");
            DropForeignKey("dbo.DetailTests", "questionId", "dbo.Questions");
            DropForeignKey("dbo.Tests", "courseDetailId", "dbo.CourseDetailOfStudents");
            DropIndex("dbo.DetailTests", new[] { "questionId" });
            DropIndex("dbo.DetailTests", new[] { "testId" });
            DropIndex("dbo.Tests", new[] { "courseDetailId" });
            DropTable("dbo.DetailTests");
            DropTable("dbo.Tests");
            CreateIndex("dbo.ScoreResults", "courseDetailId");
            AddForeignKey("dbo.ScoreResults", "courseDetailId", "dbo.CourseDetailOfStudents", "courseDetailId", cascadeDelete: true);
        }
    }
}
