namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.schedules", "day", c => c.String(nullable: false));
            AddColumn("dbo.schedules", "time", c => c.DateTime(nullable: false));
            DropColumn("dbo.schedules", "dayAndtime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.schedules", "dayAndtime", c => c.DateTime(nullable: false));
            DropColumn("dbo.schedules", "time");
            DropColumn("dbo.schedules", "day");
        }
    }
}
