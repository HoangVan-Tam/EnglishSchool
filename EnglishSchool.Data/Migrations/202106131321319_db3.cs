namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Schedule", name: "courseId", newName: "classId");
            RenameIndex(table: "dbo.Schedule", name: "IX_courseId", newName: "IX_classId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Schedule", name: "IX_classId", newName: "IX_courseId");
            RenameColumn(table: "dbo.Schedule", name: "classId", newName: "courseId");
        }
    }
}
