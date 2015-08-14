namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPersonLanguage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LanguagePersonLanguages", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.LanguagePersonLanguages", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.PersonLanguagePersons", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.PersonLanguagePersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropIndex("dbo.LanguagePersonLanguages", new[] { "Language_Id" });
            DropIndex("dbo.LanguagePersonLanguages", new[] { "PersonLanguage_Id" });
            DropIndex("dbo.PersonLanguagePersons", new[] { "PersonLanguage_Id" });
            DropIndex("dbo.PersonLanguagePersons", new[] { "Person_Id" });
            DropPrimaryKey("dbo.PersonLanguages");
            AddColumn("dbo.People", "PersonLanguage_PersonId", c => c.Long());
            AddColumn("dbo.People", "PersonLanguage_LanguageId", c => c.Long());
            AddColumn("dbo.Languages", "PersonLanguage_PersonId", c => c.Long());
            AddColumn("dbo.Languages", "PersonLanguage_LanguageId", c => c.Long());
            AlterColumn("dbo.PersonLanguages", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            CreateIndex("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            CreateIndex("dbo.PersonLanguages", "PersonId");
            CreateIndex("dbo.PersonLanguages", "LanguageId");
            CreateIndex("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            AddForeignKey("dbo.PersonLanguages", "LanguageId", "dbo.Languages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            AddForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            AddForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            DropTable("dbo.LanguagePersonLanguages");
            DropTable("dbo.PersonLanguagePersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PersonLanguagePersons",
                c => new
                    {
                        PersonLanguage_Id = c.Long(nullable: false),
                        Person_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonLanguage_Id, t.Person_Id });
            
            CreateTable(
                "dbo.LanguagePersonLanguages",
                c => new
                    {
                        Language_Id = c.Long(nullable: false),
                        PersonLanguage_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.PersonLanguage_Id });
            
            DropForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages");
            DropForeignKey("dbo.PersonLanguages", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            DropIndex("dbo.PersonLanguages", new[] { "LanguageId" });
            DropIndex("dbo.PersonLanguages", new[] { "PersonId" });
            DropIndex("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" });
            DropPrimaryKey("dbo.PersonLanguages");
            AlterColumn("dbo.PersonLanguages", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Languages", "PersonLanguage_LanguageId");
            DropColumn("dbo.Languages", "PersonLanguage_PersonId");
            DropColumn("dbo.People", "PersonLanguage_LanguageId");
            DropColumn("dbo.People", "PersonLanguage_PersonId");
            AddPrimaryKey("dbo.PersonLanguages", "Id");
            CreateIndex("dbo.PersonLanguagePersons", "Person_Id");
            CreateIndex("dbo.PersonLanguagePersons", "PersonLanguage_Id");
            CreateIndex("dbo.LanguagePersonLanguages", "PersonLanguage_Id");
            CreateIndex("dbo.LanguagePersonLanguages", "Language_Id");
            AddForeignKey("dbo.People", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            AddForeignKey("dbo.Languages", new[] { "PersonLanguage_PersonId", "PersonLanguage_LanguageId" }, "dbo.PersonLanguages", new[] { "PersonId", "LanguageId" });
            AddForeignKey("dbo.PersonLanguagePersons", "Person_Id", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonLanguagePersons", "PersonLanguage_Id", "dbo.PersonLanguages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LanguagePersonLanguages", "PersonLanguage_Id", "dbo.PersonLanguages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LanguagePersonLanguages", "Language_Id", "dbo.Languages", "Id", cascadeDelete: true);
        }
    }
}
