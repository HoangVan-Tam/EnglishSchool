namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db26 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employees");
            AddColumn("dbo.Employees", "userId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Employees", "userId");
            DropColumn("dbo.Employees", "usertId");
        }

        public override void Down()
        {
            AddColumn("dbo.Employees", "usertId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Employees");
            DropColumn("dbo.Employees", "userId");
            AddPrimaryKey("dbo.Employees", "usertId");
        }
    }
}
