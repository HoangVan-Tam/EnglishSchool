namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db37 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.schedules", "timeStart", c => c.String(nullable: false));
            AlterColumn("dbo.schedules", "timeEnd", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.schedules", "timeEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.schedules", "timeStart", c => c.DateTime(nullable: false));
        }
    }
}
