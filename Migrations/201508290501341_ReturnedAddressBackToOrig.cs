namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnedAddressBackToOrig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "AddressId" });
            AddColumn("dbo.Addresses", "Name", c => c.String());
            DropColumn("dbo.People", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "AddressId", c => c.Long(nullable: false));
            DropColumn("dbo.Addresses", "Name");
            CreateIndex("dbo.People", "AddressId");
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
