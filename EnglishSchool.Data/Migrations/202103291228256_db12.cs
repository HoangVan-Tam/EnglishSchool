namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db12 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ScoreResults");
            AddPrimaryKey("dbo.ScoreResults", new[] { "scoreResultId", "courseDetailId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ScoreResults");
            AddPrimaryKey("dbo.ScoreResults", "scoreResultId");
        }
    }
}
