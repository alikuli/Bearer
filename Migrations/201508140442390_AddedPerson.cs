namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CNIC = c.String(),
                        FName = c.String(),
                        MName = c.String(),
                        LName = c.String(),
                        Sex = c.Int(nullable: false),
                        SonOfOrWifeOf = c.Int(nullable: false),
                        NameOfFatherOrHusband = c.String(),
                        HouseNo = c.String(),
                        Road = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Country = c.String(),
                        TaxType = c.String(),
                        GeoLat = c.String(),
                        GeoLong = c.String(),
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUser = c.String(maxLength: 50),
                        ModifiedDateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUser = c.String(maxLength: 50),
                        Comment = c.String(),
                        Active = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        DeleteByUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactRecs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateContacted = c.DateTime(precision: 7, storeType: "datetime2"),
                        MethodOfContactEnum = c.Int(nullable: false),
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUser = c.String(maxLength: 50),
                        ModifiedDateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUser = c.String(maxLength: 50),
                        Comment = c.String(),
                        Active = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        DeleteByUser = c.String(maxLength: 50),
                        Person_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.PersonLanguages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonId = c.Long(nullable: false),
                        LanguageId = c.Long(nullable: false),
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUser = c.String(maxLength: 50),
                        ModifiedDateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUser = c.String(maxLength: 50),
                        Comment = c.String(),
                        Active = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        DeleteByUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUser = c.String(maxLength: 50),
                        ModifiedDateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUser = c.String(maxLength: 50),
                        Comment = c.String(),
                        Active = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        DeleteByUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguagePersonLanguages",
                c => new
                    {
                        Language_Id = c.Long(nullable: false),
                        PersonLanguage_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.PersonLanguage_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.PersonLanguages", t => t.PersonLanguage_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.PersonLanguage_Id);
            
            CreateTable(
                "dbo.PersonLanguagePersons",
                c => new
                    {
                        PersonLanguage_Id = c.Long(nullable: false),
                        Person_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonLanguage_Id, t.Person_Id })
                .ForeignKey("dbo.PersonLanguages", t => t.PersonLanguage_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.PersonLanguage_Id)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonLanguagePersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PersonLanguagePersons", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.LanguagePersonLanguages", "PersonLanguage_Id", "dbo.PersonLanguages");
            DropForeignKey("dbo.LanguagePersonLanguages", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonLanguagePersons", new[] { "Person_Id" });
            DropIndex("dbo.PersonLanguagePersons", new[] { "PersonLanguage_Id" });
            DropIndex("dbo.LanguagePersonLanguages", new[] { "PersonLanguage_Id" });
            DropIndex("dbo.LanguagePersonLanguages", new[] { "Language_Id" });
            DropIndex("dbo.ContactRecs", new[] { "Person_Id" });
            DropTable("dbo.PersonLanguagePersons");
            DropTable("dbo.LanguagePersonLanguages");
            DropTable("dbo.Languages");
            DropTable("dbo.PersonLanguages");
            DropTable("dbo.ContactRecs");
            DropTable("dbo.People");
        }
    }
}
