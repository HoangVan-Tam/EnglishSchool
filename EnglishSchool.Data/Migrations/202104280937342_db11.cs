namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                {
                    attendanceId = c.Int(nullable: false, identity: true),
                    courseDetailId = c.Int(nullable: false),
                    date = c.DateTime(nullable: false),
                    absent = c.Boolean(nullable: false),
                    reason = c.String(),
                })
                .PrimaryKey(t => new { t.attendanceId, t.courseDetailId })
                .ForeignKey("dbo.CourseDetailOfStudents", t => t.courseDetailId, cascadeDelete: true)
                .Index(t => t.courseDetailId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "courseDetailId", "dbo.CourseDetailOfStudents");
            DropIndex("dbo.Attendances", new[] { "courseDetailId" });
            DropTable("dbo.Attendances");
        }
    }
}
