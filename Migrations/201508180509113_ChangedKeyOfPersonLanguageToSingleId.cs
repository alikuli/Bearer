namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedKeyOfPersonLanguageToSingleId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropForeignKey("dbo.Languages", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.People", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropIndex("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            DropIndex("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            RenameColumn(table: "dbo.Languages", name: "PersonLanguage_PersonId", newName: "PersonLanguage_Id");
            RenameColumn(table: "dbo.People", name: "PersonLanguage_PersonId", newName: "PersonLanguage_Id");
            DropPrimaryKey("dbo.PersonLanguages");
            AlterColumn("dbo.PersonLanguages", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.PersonLanguages", "Id");
            CreateIndex("dbo.Languages", "PersonLanguage_Id");
            CreateIndex("dbo.People", "PersonLanguage_Id");
            AddForeignKey("dbo.Languages", "PersonLanguage_Id", "dbo.PersonLanguages", "Id");
            AddForeignKey("dbo.People", "PersonLanguage_Id", "dbo.PersonLanguages", "Id");
            DropColumn("dbo.People", "PersonLanguage_LanguageId");
            DropColumn("dbo.Languages", "PersonLanguage_LanguageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "PersonLanguage_LanguageId", c => c.Long());
            AddColumn("dbo.People", "PersonLanguage_LanguageId", c => c.Long());
            DropForeignKey("dbo.People", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.Languages", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropIndex("dbo.People", new[] { "PersonLanguage_Id" });
            DropIndex("dbo.Languages", new[] { "PersonLanguage_Id" });
            DropPrimaryKey("dbo.PersonLanguages");
            AlterColumn("dbo.PersonLanguages", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            RenameColumn(table: "dbo.People", name: "PersonLanguage_Id", newName: "PersonLanguage_PersonId");
            RenameColumn(table: "dbo.Languages", name: "PersonLanguage_Id", newName: "PersonLanguage_PersonId");
            CreateIndex("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            CreateIndex("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            AddForeignKey("dbo.People", "PersonLanguage_Id", "dbo.PersonLanguages", "Id");
            AddForeignKey("dbo.Languages", "PersonLanguage_Id", "dbo.PersonLanguages", "Id");
            AddForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            AddForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
        }
    }
}
