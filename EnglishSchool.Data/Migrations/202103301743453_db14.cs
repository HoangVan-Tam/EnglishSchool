namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class db14 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Students", name: "Parent_parentId", newName: "parentId");
            RenameIndex(table: "dbo.Students", name: "IX_Parent_parentId", newName: "IX_parentId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Students", name: "IX_parentId", newName: "IX_Parent_parentId");
            RenameColumn(table: "dbo.Students", name: "parentId", newName: "Parent_parentId");
        }
    }
}
