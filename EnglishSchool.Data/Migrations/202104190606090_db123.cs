namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "departmentId", "dbo.Departments");
            DropIndex("dbo.Events", new[] { "departmentId" });
            CreateTable(
                "dbo.News",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        postDate = c.DateTime(nullable: false),
                        image = c.String(),
                        detail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.Events");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        subTitle = c.String(nullable: false),
                        detail = c.String(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.News");
            CreateIndex("dbo.Events", "departmentId");
            AddForeignKey("dbo.Events", "departmentId", "dbo.Departments", "id", cascadeDelete: true);
        }
    }
}
