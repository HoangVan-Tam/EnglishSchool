namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parents", "birthday", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Parents", "birthday");
        }
    }
}
