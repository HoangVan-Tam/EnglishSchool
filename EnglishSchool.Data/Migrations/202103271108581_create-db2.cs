namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class createdb2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ListPersonOfEvents", "eventId", "dbo.Events");
            DropForeignKey("dbo.ListPersonOfEvents", "personId", "dbo.PersonalInformations");
            DropIndex("dbo.ListPersonOfEvents", new[] { "eventId" });
            DropIndex("dbo.ListPersonOfEvents", new[] { "personId" });
            DropTable("dbo.ListPersonOfEvents");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.ListPersonOfEvents",
                c => new
                {
                    eventId = c.Int(nullable: false),
                    personId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.eventId, t.personId });

            CreateIndex("dbo.ListPersonOfEvents", "personId");
            CreateIndex("dbo.ListPersonOfEvents", "eventId");
            AddForeignKey("dbo.ListPersonOfEvents", "personId", "dbo.PersonalInformations", "phoneNumber", cascadeDelete: true);
            AddForeignKey("dbo.ListPersonOfEvents", "eventId", "dbo.Events", "id", cascadeDelete: true);
        }
    }
}
