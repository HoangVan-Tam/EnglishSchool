namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db40 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseDetailOfEmployees",
                c => new
                    {
                        teacherId = c.String(nullable: false, maxLength: 128),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.teacherId, t.courseId })
                .ForeignKey("dbo.Courses", t => t.courseId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.teacherId, cascadeDelete: true)
                .Index(t => t.teacherId)
                .Index(t => t.courseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseDetailOfEmployees", "teacherId", "dbo.Employees");
            DropForeignKey("dbo.CourseDetailOfEmployees", "courseId", "dbo.Courses");
            DropIndex("dbo.CourseDetailOfEmployees", new[] { "courseId" });
            DropIndex("dbo.CourseDetailOfEmployees", new[] { "teacherId" });
            DropTable("dbo.CourseDetailOfEmployees");
        }
    }
}
