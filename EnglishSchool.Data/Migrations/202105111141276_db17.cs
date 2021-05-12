namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recruitments", "keyPurposeOfTheJob", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "keyAreasOfResponsibility", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "jobRequirements", c => c.String());
            DropColumn("dbo.Recruitments", "detail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recruitments", "detail", c => c.String(nullable: false));
            DropColumn("dbo.Recruitments", "jobRequirements");
            DropColumn("dbo.Recruitments", "keyAreasOfResponsibility");
            DropColumn("dbo.Recruitments", "keyPurposeOfTheJob");
        }
    }
}
