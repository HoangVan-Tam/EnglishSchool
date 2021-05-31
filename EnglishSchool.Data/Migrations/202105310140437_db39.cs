namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "week", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "week");
        }
    }
}
