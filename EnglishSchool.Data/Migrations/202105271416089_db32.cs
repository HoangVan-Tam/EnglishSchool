namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "level", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "level", c => c.Int(nullable: false));
        }
    }
}
