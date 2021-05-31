namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "departmentId");
            AddForeignKey("dbo.Employees", "departmentId", "dbo.Departments", "id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Employees", "departmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "departmentId" });
            DropColumn("dbo.Employees", "departmentId");
        }
    }
}
