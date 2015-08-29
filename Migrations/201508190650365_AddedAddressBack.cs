namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressBack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People");
            DropPrimaryKey("dbo.People");
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
            
            AlterColumn("dbo.People", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.People", "Id");
            CreateIndex("dbo.People", "Id");
            AddForeignKey("dbo.People", "Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People");
            DropForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People");
            DropForeignKey("dbo.People", "Id", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "Id" });
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "Id", c => c.Long(nullable: false, identity: true));
            DropTable("dbo.Addresses");
            AddPrimaryKey("dbo.People", "Id");
            AddForeignKey("dbo.PersonLanguages", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactRecs", "Person_Id", "dbo.People", "Id");
        }
    }
}
