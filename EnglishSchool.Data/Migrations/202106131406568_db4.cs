namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ClassDetailOfStudent", name: "courseId", newName: "classId");
            RenameIndex(table: "dbo.ClassDetailOfStudent", name: "IX_courseId", newName: "IX_classId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ClassDetailOfStudent", name: "IX_classId", newName: "IX_courseId");
            RenameColumn(table: "dbo.ClassDetailOfStudent", name: "classId", newName: "courseId");
        }
    }
}
