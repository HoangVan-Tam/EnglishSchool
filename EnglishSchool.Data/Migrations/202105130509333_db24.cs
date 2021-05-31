namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    usertId = c.String(nullable: false, maxLength: 128),
                    firstName = c.String(nullable: false),
                    lastName = c.String(nullable: false),
                    birthday = c.DateTime(nullable: false),
                    sex = c.Boolean(nullable: false),
                    address = c.String(nullable: false),
                    phoneNumber = c.String(nullable: false),
                    email = c.String(),
                    password = c.String(nullable: false),
                    role = c.String(nullable: false),
                })
                .PrimaryKey(t => t.usertId);

        }

        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
