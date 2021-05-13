namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "status");
        }
    }
}
