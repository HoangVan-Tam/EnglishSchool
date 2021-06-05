namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "departmentId");
            AddForeignKey("dbo.Courses", "departmentId", "dbo.Departments", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "departmentId", "dbo.Departments");
            DropIndex("dbo.Courses", new[] { "departmentId" });
            DropColumn("dbo.Courses", "departmentId");
        }
    }
}
