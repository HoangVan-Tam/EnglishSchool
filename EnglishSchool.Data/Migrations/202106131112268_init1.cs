namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        attendanceId = c.Int(nullable: false, identity: true),
                        courseDetailId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        absent = c.Boolean(nullable: false),
                        reason = c.String(),
                    })
                .PrimaryKey(t => new { t.attendanceId, t.courseDetailId })
                .ForeignKey("dbo.ClassDetailOfStudent", t => t.courseDetailId, cascadeDelete: true)
                .Index(t => t.courseDetailId);
            
            CreateTable(
                "dbo.ClassDetailOfStudent",
                c => new
                    {
                        courseDetailId = c.Int(nullable: false, identity: true),
                        courseId = c.Int(nullable: false),
                        studentId = c.String(maxLength: 128),
                        dayStart = c.DateTime(nullable: false),
                        dayFinish = c.DateTime(nullable: false),
                        finish = c.Boolean(nullable: false),
                        tuition = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.courseDetailId)
                .ForeignKey("dbo.Class", t => t.courseId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.studentId)
                .Index(t => t.courseId)
                .Index(t => t.studentId);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        numberOfStudent = c.Int(nullable: false),
                        departmentId = c.Int(nullable: false),
                        teacherId = c.String(maxLength: 128),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Course", t => t.courseId, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.departmentId, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.teacherId)
                .Index(t => t.departmentId)
                .Index(t => t.teacherId)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        title = c.String(),
                        headContent = c.String(nullable: false),
                        bodyContent = c.String(nullable: false),
                        numberOfWeeks = c.Int(nullable: false),
                        theOpeningDay = c.DateTime(nullable: false),
                        salary = c.Int(nullable: false),
                        tuition = c.Single(nullable: false),
                        note = c.String(),
                        discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Department",
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
                "dbo.Employee",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        sex = c.Boolean(nullable: false),
                        address = c.String(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        email = c.String(),
                        password = c.String(nullable: false),
                        status = c.Boolean(nullable: false),
                        role = c.String(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userId)
                .ForeignKey("dbo.Department", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        studentId = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        sex = c.Boolean(nullable: false),
                        address = c.String(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        email = c.String(),
                        level = c.Int(nullable: false),
                        status = c.Boolean(nullable: false),
                        deactivationDate = c.DateTime(nullable: false),
                        password = c.String(nullable: false),
                        departmentId = c.Int(nullable: false),
                        parentId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Department", t => t.departmentId, cascadeDelete: true)
                .ForeignKey("dbo.Parent", t => t.parentId)
                .Index(t => t.departmentId)
                .Index(t => t.parentId);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        parentId = c.String(nullable: false, maxLength: 128),
                        password = c.String(nullable: false),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        sex = c.Boolean(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        email = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.parentId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        scheduleId = c.Int(nullable: false, identity: true),
                        day = c.String(nullable: false),
                        timeStart = c.String(nullable: false),
                        timeEnd = c.String(nullable: false),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.scheduleId)
                .ForeignKey("dbo.Class", t => t.courseId, cascadeDelete: true)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.Exercise",
                c => new
                    {
                        testId = c.Int(nullable: false, identity: true),
                        week = c.Int(nullable: false),
                        startDay = c.DateTime(nullable: false),
                        finishDay = c.DateTime(nullable: false),
                        comment = c.String(),
                        status = c.String(nullable: false),
                        score = c.Single(nullable: false),
                        courseDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.testId)
                .ForeignKey("dbo.ClassDetailOfStudent", t => t.courseDetailId, cascadeDelete: true)
                .Index(t => t.courseDetailId);
            
            CreateTable(
                "dbo.DetailExercise",
                c => new
                    {
                        testId = c.Int(nullable: false),
                        questionId = c.Int(nullable: false),
                        correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.testId, t.questionId })
                .ForeignKey("dbo.Question", t => t.questionId, cascadeDelete: true)
                .ForeignKey("dbo.Exercise", t => t.testId, cascadeDelete: true)
                .Index(t => t.testId)
                .Index(t => t.questionId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        questionId = c.Int(nullable: false, identity: true),
                        questionDetail = c.String(nullable: false),
                        answer1 = c.String(nullable: false),
                        answer2 = c.String(nullable: false),
                        answer3 = c.String(nullable: false),
                        answer4 = c.String(nullable: false),
                        level = c.String(nullable: false),
                        rightAnswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.questionId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        postDate = c.DateTime(nullable: false),
                        image = c.String(),
                        headContent = c.String(nullable: false),
                        bodyContent = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PersonalInformation",
                c => new
                    {
                        phoneNumber = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        address = c.String(),
                        status = c.String(nullable: false),
                        note = c.String(),
                    })
                .PrimaryKey(t => t.phoneNumber);
            
            CreateTable(
                "dbo.Recruitment",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        vacancies = c.String(nullable: false, maxLength: 100),
                        jobDecription = c.String(nullable: false),
                        jobRequirements = c.String(nullable: false),
                        rightsOfTheEmployees = c.String(nullable: false),
                        address = c.String(nullable: false),
                        amount = c.Int(nullable: false),
                        complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "courseDetailId", "dbo.ClassDetailOfStudent");
            DropForeignKey("dbo.DetailExercise", "testId", "dbo.Exercise");
            DropForeignKey("dbo.DetailExercise", "questionId", "dbo.Question");
            DropForeignKey("dbo.Exercise", "courseDetailId", "dbo.ClassDetailOfStudent");
            DropForeignKey("dbo.ClassDetailOfStudent", "studentId", "dbo.Student");
            DropForeignKey("dbo.ClassDetailOfStudent", "courseId", "dbo.Class");
            DropForeignKey("dbo.Schedule", "courseId", "dbo.Class");
            DropForeignKey("dbo.Class", "teacherId", "dbo.Employee");
            DropForeignKey("dbo.Class", "departmentId", "dbo.Department");
            DropForeignKey("dbo.Student", "parentId", "dbo.Parent");
            DropForeignKey("dbo.Student", "departmentId", "dbo.Department");
            DropForeignKey("dbo.Employee", "departmentId", "dbo.Department");
            DropForeignKey("dbo.Class", "courseId", "dbo.Course");
            DropIndex("dbo.DetailExercise", new[] { "questionId" });
            DropIndex("dbo.DetailExercise", new[] { "testId" });
            DropIndex("dbo.Exercise", new[] { "courseDetailId" });
            DropIndex("dbo.Schedule", new[] { "courseId" });
            DropIndex("dbo.Student", new[] { "parentId" });
            DropIndex("dbo.Student", new[] { "departmentId" });
            DropIndex("dbo.Employee", new[] { "departmentId" });
            DropIndex("dbo.Class", new[] { "courseId" });
            DropIndex("dbo.Class", new[] { "teacherId" });
            DropIndex("dbo.Class", new[] { "departmentId" });
            DropIndex("dbo.ClassDetailOfStudent", new[] { "studentId" });
            DropIndex("dbo.ClassDetailOfStudent", new[] { "courseId" });
            DropIndex("dbo.Attendance", new[] { "courseDetailId" });
            DropTable("dbo.Recruitment");
            DropTable("dbo.PersonalInformation");
            DropTable("dbo.News");
            DropTable("dbo.Question");
            DropTable("dbo.DetailExercise");
            DropTable("dbo.Exercise");
            DropTable("dbo.Schedule");
            DropTable("dbo.Parent");
            DropTable("dbo.Student");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
            DropTable("dbo.Class");
            DropTable("dbo.ClassDetailOfStudent");
            DropTable("dbo.Attendance");
        }
    }
}
