namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCountryIdToAddress : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Addresses", name: "Country_Id", newName: "CountryID");
            RenameIndex(table: "dbo.Addresses", name: "IX_Country_Id", newName: "IX_CountryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Addresses", name: "IX_CountryID", newName: "IX_Country_Id");
            RenameColumn(table: "dbo.Addresses", name: "CountryID", newName: "Country_Id");
        }
    }
}
