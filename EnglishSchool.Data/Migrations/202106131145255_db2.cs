namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Student", "parentId", "dbo.Parent");
            DropIndex("dbo.Student", new[] { "parentId" });
            DropColumn("dbo.Student", "parentId");
            DropTable("dbo.Parent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        parentId = c.String(nullable: false, maxLength: 128),
                        password = c.String(nullable: false),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        sex = c.Boolean(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        email = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.parentId);
            
            AddColumn("dbo.Student", "parentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Student", "parentId");
            AddForeignKey("dbo.Student", "parentId", "dbo.Parent", "parentId");
        }
    }
}
