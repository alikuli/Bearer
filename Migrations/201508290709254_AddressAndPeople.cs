namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressAndPeople : DbMigration
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
                        CreatedDateStarted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        Country_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);
            
            AddColumn("dbo.People", "Address_Id", c => c.Long());
            AddColumn("dbo.People", "Address_Id1", c => c.Long());
            AddColumn("dbo.People", "Address_Id2", c => c.Long());
            AddColumn("dbo.People", "Address_Id3", c => c.Long());
            AddColumn("dbo.People", "Address_Id4", c => c.Long());
            CreateIndex("dbo.People", "AddressMainId");
            CreateIndex("dbo.People", "DefaultBillToId");
            CreateIndex("dbo.People", "DefaultShipToId");
            CreateIndex("dbo.People", "DefaultConsignToId");
            CreateIndex("dbo.People", "DefaultInformToId");
            CreateIndex("dbo.People", "Address_Id");
            CreateIndex("dbo.People", "Address_Id1");
            CreateIndex("dbo.People", "Address_Id2");
            CreateIndex("dbo.People", "Address_Id3");
            CreateIndex("dbo.People", "Address_Id4");
            AddForeignKey("dbo.People", "AddressMainId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "Address_Id1", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "Address_Id2", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "Address_Id3", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "Address_Id4", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Address_Id4", "dbo.Addresses");
            DropForeignKey("dbo.People", "Address_Id3", "dbo.Addresses");
            DropForeignKey("dbo.People", "Address_Id2", "dbo.Addresses");
            DropForeignKey("dbo.People", "Address_Id1", "dbo.Addresses");
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "AddressMainId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropIndex("dbo.People", new[] { "Address_Id4" });
            DropIndex("dbo.People", new[] { "Address_Id3" });
            DropIndex("dbo.People", new[] { "Address_Id2" });
            DropIndex("dbo.People", new[] { "Address_Id1" });
            DropIndex("dbo.People", new[] { "Address_Id" });
            DropIndex("dbo.People", new[] { "DefaultInformToId" });
            DropIndex("dbo.People", new[] { "DefaultConsignToId" });
            DropIndex("dbo.People", new[] { "DefaultShipToId" });
            DropIndex("dbo.People", new[] { "DefaultBillToId" });
            DropIndex("dbo.People", new[] { "AddressMainId" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropColumn("dbo.People", "Address_Id4");
            DropColumn("dbo.People", "Address_Id3");
            DropColumn("dbo.People", "Address_Id2");
            DropColumn("dbo.People", "Address_Id1");
            DropColumn("dbo.People", "Address_Id");
            DropTable("dbo.Addresses");
        }
    }
}
