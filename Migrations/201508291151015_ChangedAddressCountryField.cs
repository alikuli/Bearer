namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAddressCountryField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "CountryID", "dbo.Countries");
            DropIndex("dbo.Addresses", new[] { "CountryID" });
            AlterColumn("dbo.Addresses", "CountryID", c => c.Long());
            CreateIndex("dbo.Addresses", "CountryID");
            AddForeignKey("dbo.Addresses", "CountryID", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "CountryID", "dbo.Countries");
            DropIndex("dbo.Addresses", new[] { "CountryID" });
            AlterColumn("dbo.Addresses", "CountryID", c => c.Long(nullable: false));
            CreateIndex("dbo.Addresses", "CountryID");
            AddForeignKey("dbo.Addresses", "CountryID", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
