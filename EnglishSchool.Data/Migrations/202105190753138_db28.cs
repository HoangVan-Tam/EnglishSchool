namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "title", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "headContent", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "bodyContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "bodyContent");
            DropColumn("dbo.Courses", "headContent");
            DropColumn("dbo.Courses", "title");
        }
    }
}
