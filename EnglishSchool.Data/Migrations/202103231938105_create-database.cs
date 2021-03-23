namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        password = c.String(nullable: false),
                        role = c.Int(nullable: false),
                        status = c.String(nullable: false),
                        deactivationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.userName);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        studentId = c.String(),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        sex = c.Boolean(nullable: false),
                        address = c.String(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        email = c.String(),
                        level = c.Int(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userName)
                .ForeignKey("dbo.Accounts", t => t.userName)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.userName)
                .Index(t => t.departmentId);
            
            CreateTable(
                "dbo.CourseDetailOfStudents",
                c => new
                    {
                        courseId = c.Int(nullable: false),
                        studentId = c.String(nullable: false, maxLength: 128),
                        dayStart = c.DateTime(nullable: false),
                        dayFinish = c.DateTime(nullable: false),
                        finish = c.Boolean(nullable: false),
                        tuition = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.courseId, t.studentId, t.dayStart })
                .ForeignKey("dbo.Courses", t => t.courseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentId, cascadeDelete: true)
                .Index(t => t.courseId)
                .Index(t => t.studentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        numberOfMonths = c.Int(nullable: false),
                        tuition = c.Single(nullable: false),
                        note = c.String(),
                        discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        city = c.String(nullable: false),
                        name = c.String(nullable: false),
                        address = c.String(nullable: false),
                        detail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        subTitle = c.String(nullable: false),
                        detail = c.String(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
            CreateTable(
                "dbo.ListPersonOfEvents",
                c => new
                    {
                        eventId = c.Int(nullable: false),
                        personId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.eventId, t.personId })
                .ForeignKey("dbo.Events", t => t.eventId, cascadeDelete: true)
                .ForeignKey("dbo.PersonalInformations", t => t.personId, cascadeDelete: true)
                .Index(t => t.eventId)
                .Index(t => t.personId);
            
            CreateTable(
                "dbo.PersonalInformations",
                c => new
                    {
                        phoneNumber = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.phoneNumber);
            
            CreateTable(
                "dbo.RecruitmentDetails",
                c => new
                    {
                        departmentId = c.Int(nullable: false),
                        recruitmentId = c.Int(nullable: false),
                        amount = c.Int(nullable: false),
                        complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.departmentId, t.recruitmentId })
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .ForeignKey("dbo.Recruitments", t => t.recruitmentId, cascadeDelete: true)
                .Index(t => t.departmentId)
                .Index(t => t.recruitmentId);
            
            CreateTable(
                "dbo.Recruitments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        vacancies = c.String(nullable: false, maxLength: 100),
                        detail = c.String(nullable: false),
                        requirement = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.RecruitmentDetails", "recruitmentId", "dbo.Recruitments");
            DropForeignKey("dbo.RecruitmentDetails", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.ListPersonOfEvents", "personId", "dbo.PersonalInformations");
            DropForeignKey("dbo.ListPersonOfEvents", "eventId", "dbo.Events");
            DropForeignKey("dbo.Events", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseDetailOfStudents", "studentId", "dbo.Students");
            DropForeignKey("dbo.CourseDetailOfStudents", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "userName", "dbo.Accounts");
            DropIndex("dbo.RecruitmentDetails", new[] { "recruitmentId" });
            DropIndex("dbo.RecruitmentDetails", new[] { "departmentId" });
            DropIndex("dbo.ListPersonOfEvents", new[] { "personId" });
            DropIndex("dbo.ListPersonOfEvents", new[] { "eventId" });
            DropIndex("dbo.Events", new[] { "departmentId" });
            DropIndex("dbo.CourseDetailOfStudents", new[] { "studentId" });
            DropIndex("dbo.CourseDetailOfStudents", new[] { "courseId" });
            DropIndex("dbo.Students", new[] { "departmentId" });
            DropIndex("dbo.Students", new[] { "userName" });
            DropTable("dbo.Recruitments");
            DropTable("dbo.RecruitmentDetails");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.ListPersonOfEvents");
            DropTable("dbo.Events");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseDetailOfStudents");
            DropTable("dbo.Students");
            DropTable("dbo.Accounts");
        }
    }
}
