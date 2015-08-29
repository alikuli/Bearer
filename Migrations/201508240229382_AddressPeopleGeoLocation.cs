namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressPeopleGeoLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Id", "dbo.Addresses");
            DropForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People");
            DropIndex("dbo.People", new[] { "Id" });
            DropPrimaryKey("dbo.People");
            CreateTable(
                "dbo.GeoLocs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GeoLocation_Latitude = c.String(nullable: false, maxLength: 100),
                        GeoLocation_Longitude = c.String(nullable: false, maxLength: 100),
                        GeoLocation_Radius = c.Double(nullable: false),
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
            
            AddColumn("dbo.People", "AddressId", c => c.Long(nullable: false));
            AddColumn("dbo.People", "DefaultBillToId", c => c.Long());
            AddColumn("dbo.People", "DefaultShipToId", c => c.Long());
            AddColumn("dbo.People", "DefaultConsignToId", c => c.Long());
            AddColumn("dbo.People", "DefaultInformToId", c => c.Long());
            AddColumn("dbo.People", "GeoLocationId", c => c.Long(nullable: false));
            AddColumn("dbo.People", "BoardingStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.People", "LName", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.People", "Id");
            CreateIndex("dbo.People", "AddressId");
            CreateIndex("dbo.People", "DefaultBillToId");
            CreateIndex("dbo.People", "DefaultShipToId");
            CreateIndex("dbo.People", "DefaultConsignToId");
            CreateIndex("dbo.People", "DefaultInformToId");
            CreateIndex("dbo.People", "GeoLocationId");
            AddForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "GeoLocationId", "dbo.GeoLocs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Addresses", "Name");
            DropColumn("dbo.Addresses", "HouseNo");
            DropColumn("dbo.Addresses", "Road");
            DropColumn("dbo.Addresses", "Address2");
            DropColumn("dbo.Addresses", "City");
            DropColumn("dbo.Addresses", "State");
            DropColumn("dbo.Addresses", "Zip");
            DropColumn("dbo.Addresses", "Country");
            DropColumn("dbo.Addresses", "GeoLat");
            DropColumn("dbo.Addresses", "GeoLong");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "GeoLong", c => c.String(maxLength: 100));
            AddColumn("dbo.Addresses", "GeoLat", c => c.String(maxLength: 100));
            AddColumn("dbo.Addresses", "Country", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Addresses", "Zip", c => c.String(maxLength: 50));
            AddColumn("dbo.Addresses", "State", c => c.String(maxLength: 50));
            AddColumn("dbo.Addresses", "City", c => c.String(maxLength: 100));
            AddColumn("dbo.Addresses", "Address2", c => c.String(maxLength: 200));
            AddColumn("dbo.Addresses", "Road", c => c.String(maxLength: 200));
            AddColumn("dbo.Addresses", "HouseNo", c => c.String(maxLength: 50));
            AddColumn("dbo.Addresses", "Name", c => c.String());
            DropForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People");
            DropForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People");
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.People", "GeoLocationId", "dbo.GeoLocs");
            DropForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "GeoLocationId" });
            DropIndex("dbo.People", new[] { "DefaultInformToId" });
            DropIndex("dbo.People", new[] { "DefaultConsignToId" });
            DropIndex("dbo.People", new[] { "DefaultShipToId" });
            DropIndex("dbo.People", new[] { "DefaultBillToId" });
            DropIndex("dbo.People", new[] { "AddressId" });
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "LName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.People", "BoardingStatus");
            DropColumn("dbo.People", "GeoLocationId");
            DropColumn("dbo.People", "DefaultInformToId");
            DropColumn("dbo.People", "DefaultConsignToId");
            DropColumn("dbo.People", "DefaultShipToId");
            DropColumn("dbo.People", "DefaultBillToId");
            DropColumn("dbo.People", "AddressId");
            DropTable("dbo.GeoLocs");
            AddPrimaryKey("dbo.People", "Id");
            CreateIndex("dbo.People", "Id");
            AddForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.People", "Id", "dbo.Addresses", "Id");
        }
    }
}
