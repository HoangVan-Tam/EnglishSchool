namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parents", "birthDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Parents", "birdDay");
        }

        public override void Down()
        {
            AddColumn("dbo.Parents", "birdDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Parents", "birthDay");
        }
    }
}
