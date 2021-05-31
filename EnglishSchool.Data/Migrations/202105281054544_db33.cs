namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "headContent", c => c.String(nullable: false));
            AddColumn("dbo.News", "bodyContent", c => c.String(nullable: false));
            DropColumn("dbo.News", "detail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "detail", c => c.String(nullable: false));
            DropColumn("dbo.News", "bodyContent");
            DropColumn("dbo.News", "headContent");
        }
    }
}
