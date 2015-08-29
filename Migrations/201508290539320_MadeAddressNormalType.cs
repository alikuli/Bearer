namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeAddressNormalType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses");
            DropForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses");
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropIndex("dbo.People", new[] { "DefaultBillToId" });
            DropIndex("dbo.People", new[] { "DefaultShipToId" });
            DropIndex("dbo.People", new[] { "DefaultConsignToId" });
            DropIndex("dbo.People", new[] { "DefaultInformToId" });
            AddColumn("dbo.People", "AddressMainId", c => c.Long());
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
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
                        Country_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.People", "AddressMainId");
            CreateIndex("dbo.People", "DefaultInformToId");
            CreateIndex("dbo.People", "DefaultConsignToId");
            CreateIndex("dbo.People", "DefaultShipToId");
            CreateIndex("dbo.People", "DefaultBillToId");
            CreateIndex("dbo.Addresses", "Country_Id");
            AddForeignKey("dbo.People", "DefaultShipToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultConsignToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.People", "DefaultBillToId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
