namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruitmentDetails", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.RecruitmentDetails", "recruitmentId", "dbo.Recruitments");
            DropIndex("dbo.RecruitmentDetails", new[] { "departmentId" });
            DropIndex("dbo.RecruitmentDetails", new[] { "recruitmentId" });
            AddColumn("dbo.Recruitments", "jobDecription", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "rightsOfTheEmployees", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "amount", c => c.Int(nullable: false));
            AddColumn("dbo.Recruitments", "complete", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Recruitments", "jobRequirements", c => c.String(nullable: false));
            DropColumn("dbo.Recruitments", "keyPurposeOfTheJob");
            DropColumn("dbo.Recruitments", "keyAreasOfResponsibility");
            DropColumn("dbo.Recruitments", "requirement");
            DropTable("dbo.RecruitmentDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecruitmentDetails",
                c => new
                    {
                        departmentId = c.Int(nullable: false),
                        recruitmentId = c.Int(nullable: false),
                        amount = c.Int(nullable: false),
                        complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.departmentId, t.recruitmentId });
            
            AddColumn("dbo.Recruitments", "requirement", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "keyAreasOfResponsibility", c => c.String(nullable: false));
            AddColumn("dbo.Recruitments", "keyPurposeOfTheJob", c => c.String(nullable: false));
            AlterColumn("dbo.Recruitments", "jobRequirements", c => c.String());
            DropColumn("dbo.Recruitments", "complete");
            DropColumn("dbo.Recruitments", "amount");
            DropColumn("dbo.Recruitments", "rightsOfTheEmployees");
            DropColumn("dbo.Recruitments", "jobDecription");
            CreateIndex("dbo.RecruitmentDetails", "recruitmentId");
            CreateIndex("dbo.RecruitmentDetails", "departmentId");
            AddForeignKey("dbo.RecruitmentDetails", "recruitmentId", "dbo.Recruitments", "id", cascadeDelete: true);
            AddForeignKey("dbo.RecruitmentDetails", "departmentId", "dbo.Departments", "id", cascadeDelete: true);
        }
    }
}
