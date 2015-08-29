namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldForIdForGeoLoction : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.People", name: "HomeGeoLocation_Id", newName: "HomeGeoLocationId");
            RenameIndex(table: "dbo.People", name: "IX_HomeGeoLocation_Id", newName: "IX_HomeGeoLocationId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.People", name: "IX_HomeGeoLocationId", newName: "IX_HomeGeoLocation_Id");
            RenameColumn(table: "dbo.People", name: "HomeGeoLocationId", newName: "HomeGeoLocation_Id");
        }
    }
}
