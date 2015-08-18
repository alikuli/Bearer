namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChangesUnknown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "UnDeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.People", "UnDeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ContactRecs", "UnDeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.ContactRecs", "UnDeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.PersonLanguages", "UnDeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.PersonLanguages", "UnDeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Languages", "UnDeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.Languages", "UnDeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "UnDeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.SetUps", "UnDeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SetUps", "UnDeleteDate");
            DropColumn("dbo.SetUps", "UnDeletedByUser");
            DropColumn("dbo.Languages", "UnDeleteDate");
            DropColumn("dbo.Languages", "UnDeletedByUser");
            DropColumn("dbo.PersonLanguages", "UnDeleteDate");
            DropColumn("dbo.PersonLanguages", "UnDeletedByUser");
            DropColumn("dbo.ContactRecs", "UnDeleteDate");
            DropColumn("dbo.ContactRecs", "UnDeletedByUser");
            DropColumn("dbo.People", "UnDeleteDate");
            DropColumn("dbo.People", "UnDeletedByUser");
        }
    }
}
