namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class createdb : DbMigration
    {
        public override void Up()
        {
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
                "dbo.CourseDetailOfStudents",
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
                .ForeignKey("dbo.Courses", t => t.courseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentId)
                .Index(t => t.courseId)
                .Index(t => t.studentId);

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
                .PrimaryKey(t => t.scoreResultId)
                .ForeignKey("dbo.CourseDetailOfStudents", t => t.courseDetailId, cascadeDelete: true)
                .Index(t => t.courseDetailId);

            CreateTable(
                "dbo.Students",
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
                    Parent_parentId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .ForeignKey("dbo.Parents", t => t.Parent_parentId)
                .Index(t => t.departmentId)
                .Index(t => t.Parent_parentId);

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

            CreateTable(
                "dbo.Parents",
                c => new
                {
                    parentId = c.String(nullable: false, maxLength: 128),
                    password = c.String(nullable: false),
                    firstName = c.String(nullable: false),
                    lastName = c.String(nullable: false),
                    sex = c.Boolean(nullable: false),
                    birdDay = c.DateTime(nullable: false),
                    phoneNumber = c.String(nullable: false),
                    email = c.String(),
                    status = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.parentId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Students", "Parent_parentId", "dbo.Parents");
            DropForeignKey("dbo.CourseDetailOfStudents", "studentId", "dbo.Students");
            DropForeignKey("dbo.Students", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.RecruitmentDetails", "recruitmentId", "dbo.Recruitments");
            DropForeignKey("dbo.RecruitmentDetails", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.ListPersonOfEvents", "personId", "dbo.PersonalInformations");
            DropForeignKey("dbo.ListPersonOfEvents", "eventId", "dbo.Events");
            DropForeignKey("dbo.Events", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.ScoreResults", "courseDetailId", "dbo.CourseDetailOfStudents");
            DropForeignKey("dbo.CourseDetailOfStudents", "courseId", "dbo.Courses");
            DropIndex("dbo.RecruitmentDetails", new[] { "recruitmentId" });
            DropIndex("dbo.RecruitmentDetails", new[] { "departmentId" });
            DropIndex("dbo.ListPersonOfEvents", new[] { "personId" });
            DropIndex("dbo.ListPersonOfEvents", new[] { "eventId" });
            DropIndex("dbo.Events", new[] { "departmentId" });
            DropIndex("dbo.Students", new[] { "Parent_parentId" });
            DropIndex("dbo.Students", new[] { "departmentId" });
            DropIndex("dbo.ScoreResults", new[] { "courseDetailId" });
            DropIndex("dbo.CourseDetailOfStudents", new[] { "studentId" });
            DropIndex("dbo.CourseDetailOfStudents", new[] { "courseId" });
            DropTable("dbo.Parents");
            DropTable("dbo.Recruitments");
            DropTable("dbo.RecruitmentDetails");
            DropTable("dbo.PersonalInformations");
            DropTable("dbo.ListPersonOfEvents");
            DropTable("dbo.Events");
            DropTable("dbo.Departments");
            DropTable("dbo.Students");
            DropTable("dbo.ScoreResults");
            DropTable("dbo.CourseDetailOfStudents");
            DropTable("dbo.Courses");
        }
    }
}
