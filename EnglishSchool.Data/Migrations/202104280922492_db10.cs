namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "schedule", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "theOpeningDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "theOpeningDay");
            DropColumn("dbo.Courses", "schedule");
        }
    }
}
