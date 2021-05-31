namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.schedules", "timeStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.schedules", "timeEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.schedules", "time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.schedules", "time", c => c.DateTime(nullable: false));
            DropColumn("dbo.schedules", "timeEnd");
            DropColumn("dbo.schedules", "timeStart");
        }
    }
}
