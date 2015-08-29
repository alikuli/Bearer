namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCreatedDateToNonNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Languages", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PersonLanguages", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.People", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ContactRecs", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.GeoLocs", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SetUps", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SetUps", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.GeoLocs", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ContactRecs", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.People", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PersonLanguages", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Languages", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Addresses", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}
