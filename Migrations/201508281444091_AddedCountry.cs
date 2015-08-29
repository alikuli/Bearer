namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneIntlCod = c.String(),
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Addresses", "Country_Id", c => c.Long());
            CreateIndex("dbo.Addresses", "Country_Id");
            AddForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropColumn("dbo.Addresses", "Country_Id");
            DropTable("dbo.Countries");
        }
    }
}
