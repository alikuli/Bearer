namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedGeoAddedWorkGeoFixedPerson : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GeoLocs", newName: "GeoLocHomes");
            DropForeignKey("dbo.People", "GeoLocationId", "dbo.GeoLocs");
            DropIndex("dbo.People", new[] { "GeoLocationId" });
            RenameColumn(table: "dbo.People", name: "GeoLocationId", newName: "HomeGeoLocation_Id");
            AddColumn("dbo.GeoLocHomes", "PersonId", c => c.Long(nullable: false));
            AddColumn("dbo.GeoLocHomes", "Person_Id", c => c.Long());
            AlterColumn("dbo.People", "HomeGeoLocation_Id", c => c.Long());
            CreateIndex("dbo.People", "HomeGeoLocation_Id");
            CreateIndex("dbo.GeoLocHomes", "PersonId");
            CreateIndex("dbo.GeoLocHomes", "Person_Id");
            AddForeignKey("dbo.GeoLocHomes", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GeoLocHomes", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.People", "HomeGeoLocation_Id", "dbo.GeoLocHomes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "HomeGeoLocation_Id", "dbo.GeoLocHomes");
            DropForeignKey("dbo.GeoLocHomes", "Person_Id", "dbo.People");
            DropForeignKey("dbo.GeoLocHomes", "PersonId", "dbo.People");
            DropIndex("dbo.GeoLocHomes", new[] { "Person_Id" });
            DropIndex("dbo.GeoLocHomes", new[] { "PersonId" });
            DropIndex("dbo.People", new[] { "HomeGeoLocation_Id" });
            AlterColumn("dbo.People", "HomeGeoLocation_Id", c => c.Long(nullable: false));
            DropColumn("dbo.GeoLocHomes", "Person_Id");
            DropColumn("dbo.GeoLocHomes", "PersonId");
            RenameColumn(table: "dbo.People", name: "HomeGeoLocation_Id", newName: "GeoLocationId");
            CreateIndex("dbo.People", "GeoLocationId");
            AddForeignKey("dbo.People", "GeoLocationId", "dbo.GeoLocs", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.GeoLocHomes", newName: "GeoLocs");
        }
    }
}
