namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                {
                    questionId = c.Int(nullable: false, identity: true),
                    question = c.String(nullable: false),
                    answer1 = c.String(nullable: false),
                    answer2 = c.String(nullable: false),
                    answer3 = c.String(nullable: false),
                    answer4 = c.String(nullable: false),
                    level = c.Int(nullable: false),
                    rightAnswer = c.String(nullable: false),
                })
                .PrimaryKey(t => t.questionId);

        }

        public override void Down()
        {
            DropTable("dbo.Questions");
        }
    }
}
