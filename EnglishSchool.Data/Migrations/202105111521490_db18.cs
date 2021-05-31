namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db18 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Parents", "birthday");
        }

        public override void Down()
        {
            AddColumn("dbo.Parents", "birthday", c => c.DateTime(nullable: false));
        }
    }
}
