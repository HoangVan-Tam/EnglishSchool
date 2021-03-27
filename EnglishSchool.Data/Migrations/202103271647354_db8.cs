namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "questionDetail", c => c.String(nullable: false));
            DropColumn("dbo.Questions", "question");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "question", c => c.String(nullable: false));
            DropColumn("dbo.Questions", "questionDetail");
        }
    }
}
