namespace EnglishSchool.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalInformations", "address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalInformations", "address", c => c.String(nullable: false));
        }
    }
}
