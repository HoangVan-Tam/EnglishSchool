﻿namespace EnglishSchool.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class createdb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalInformations", "status", c => c.String(nullable: false));
            AddColumn("dbo.PersonalInformations", "note", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.PersonalInformations", "note");
            DropColumn("dbo.PersonalInformations", "status");
        }
    }
}
