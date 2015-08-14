namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToLongAndOtherStuff : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SetUps");
            AddColumn("dbo.People", "DeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.People", "DeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ContactRecs", "DeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.ContactRecs", "DeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.PersonLanguages", "DeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.PersonLanguages", "DeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Languages", "DeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.Languages", "DeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "CreatedDateStarted", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "CreatedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "CreatedUser", c => c.String(maxLength: 50));
            AddColumn("dbo.SetUps", "ModifiedDateStart", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SetUps", "ModifiedUser", c => c.String(maxLength: 50));
            AddColumn("dbo.SetUps", "Comment", c => c.String());
            AddColumn("dbo.SetUps", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.SetUps", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.SetUps", "DeletedByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.SetUps", "DeleteDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.People", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.People", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContactRecs", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContactRecs", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PersonLanguages", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PersonLanguages", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Languages", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Languages", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SetUps", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.SetUps", "Id");
            DropColumn("dbo.People", "DeleteByUser");
            DropColumn("dbo.ContactRecs", "DeleteByUser");
            DropColumn("dbo.PersonLanguages", "DeleteByUser");
            DropColumn("dbo.Languages", "DeleteByUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "DeleteByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.PersonLanguages", "DeleteByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.ContactRecs", "DeleteByUser", c => c.String(maxLength: 50));
            AddColumn("dbo.People", "DeleteByUser", c => c.String(maxLength: 50));
            DropPrimaryKey("dbo.SetUps");
            AlterColumn("dbo.SetUps", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Languages", "Deleted", c => c.Int(nullable: false));
            AlterColumn("dbo.Languages", "Active", c => c.Int(nullable: false));
            AlterColumn("dbo.PersonLanguages", "Deleted", c => c.Int(nullable: false));
            AlterColumn("dbo.PersonLanguages", "Active", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecs", "Deleted", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactRecs", "Active", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "Deleted", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "Active", c => c.Int(nullable: false));
            DropColumn("dbo.SetUps", "DeleteDate");
            DropColumn("dbo.SetUps", "DeletedByUser");
            DropColumn("dbo.SetUps", "Deleted");
            DropColumn("dbo.SetUps", "Active");
            DropColumn("dbo.SetUps", "Comment");
            DropColumn("dbo.SetUps", "ModifiedUser");
            DropColumn("dbo.SetUps", "ModifiedDate");
            DropColumn("dbo.SetUps", "ModifiedDateStart");
            DropColumn("dbo.SetUps", "CreatedUser");
            DropColumn("dbo.SetUps", "CreatedDate");
            DropColumn("dbo.SetUps", "CreatedDateStarted");
            DropColumn("dbo.Languages", "DeleteDate");
            DropColumn("dbo.Languages", "DeletedByUser");
            DropColumn("dbo.PersonLanguages", "DeleteDate");
            DropColumn("dbo.PersonLanguages", "DeletedByUser");
            DropColumn("dbo.ContactRecs", "DeleteDate");
            DropColumn("dbo.ContactRecs", "DeletedByUser");
            DropColumn("dbo.People", "DeleteDate");
            DropColumn("dbo.People", "DeletedByUser");
            AddPrimaryKey("dbo.SetUps", "Id");
        }
    }
}
