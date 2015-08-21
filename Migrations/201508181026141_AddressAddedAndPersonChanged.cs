namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressAddedAndPersonChanged : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        HouseNo = c.String(maxLength: 50),
                        Road = c.String(maxLength: 200),
                        Address2 = c.String(maxLength: 200),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 50),
                        Zip = c.String(maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 100),
                        GeoLat = c.String(maxLength: 100),
                        GeoLong = c.String(maxLength: 100),
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedUser = c.String(maxLength: 50),
                        ModifiedDateStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedUser = c.String(maxLength: 50),
                        Comment = c.String(maxLength: 1000),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedByUser = c.String(maxLength: 50),
                        DeleteDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UnDeletedByUser = c.String(maxLength: 50),
                        UnDeleteDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.People", "Address_Id", c => c.Long());
            AlterColumn("dbo.Languages", "Comment", c => c.String(maxLength: 1000));
            AlterColumn("dbo.PersonLanguages", "Comment", c => c.String(maxLength: 1000));
            AlterColumn("dbo.People", "IdentificationNo", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "FName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "MName", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "LName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "Comment", c => c.String(maxLength: 1000));
            AlterColumn("dbo.ContactRecs", "Comment", c => c.String(maxLength: 1000));
            AlterColumn("dbo.SetUps", "Comment", c => c.String(maxLength: 1000));
            CreateIndex("dbo.People", "Address_Id");
            AddForeignKey("dbo.People", "Address_Id", "dbo.Addresses", "Id");
            DropColumn("dbo.People", "HouseNo");
            DropColumn("dbo.People", "Road");
            DropColumn("dbo.People", "Address2");
            DropColumn("dbo.People", "City");
            DropColumn("dbo.People", "State");
            DropColumn("dbo.People", "Zip");
            DropColumn("dbo.People", "Country");
            DropColumn("dbo.People", "GeoLat");
            DropColumn("dbo.People", "GeoLong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "GeoLong", c => c.String());
            AddColumn("dbo.People", "GeoLat", c => c.String());
            AddColumn("dbo.People", "Country", c => c.String(nullable: false));
            AddColumn("dbo.People", "Zip", c => c.String());
            AddColumn("dbo.People", "State", c => c.String());
            AddColumn("dbo.People", "City", c => c.String());
            AddColumn("dbo.People", "Address2", c => c.String());
            AddColumn("dbo.People", "Road", c => c.String());
            AddColumn("dbo.People", "HouseNo", c => c.String());
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "Address_Id" });
            AlterColumn("dbo.SetUps", "Comment", c => c.String());
            AlterColumn("dbo.ContactRecs", "Comment", c => c.String());
            AlterColumn("dbo.People", "Comment", c => c.String());
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String(nullable: false));
            AlterColumn("dbo.People", "LName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "MName", c => c.String());
            AlterColumn("dbo.People", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "IdentificationNo", c => c.String(nullable: false));
            AlterColumn("dbo.PersonLanguages", "Comment", c => c.String());
            AlterColumn("dbo.Languages", "Comment", c => c.String());
            DropColumn("dbo.People", "Address_Id");
            DropTable("dbo.Addresses");
        }
    }
}
