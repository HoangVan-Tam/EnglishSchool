namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recruitments", "address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recruitments", "address");
        }
    }
}
