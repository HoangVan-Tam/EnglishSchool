namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.schedules",
                c => new
                    {
                        scheduleId = c.Int(nullable: false, identity: true),
                        dayAndtime = c.DateTime(nullable: false),
                        courseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.scheduleId)
                .ForeignKey("dbo.Courses", t => t.courseId, cascadeDelete: true)
                .Index(t => t.courseId);
            
            DropColumn("dbo.Courses", "schedule");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "schedule", c => c.String(nullable: false));
            DropForeignKey("dbo.schedules", "courseId", "dbo.Courses");
            DropIndex("dbo.schedules", new[] { "courseId" });
            DropTable("dbo.schedules");
        }
    }
}
