namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTheAddressInPersonToAClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "Address_Id" });
            DropColumn("dbo.People", "Address_Id");
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
            CreateIndex("dbo.People", "Address_Id");
            AddForeignKey("dbo.People", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
