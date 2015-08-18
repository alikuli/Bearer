namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeFieldsRequiredInDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "IdentificationNo", c => c.String(nullable: false));
            AlterColumn("dbo.People", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "LName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Country", c => c.String());
            AlterColumn("dbo.People", "NameOfFatherOrHusband", c => c.String());
            AlterColumn("dbo.People", "LName", c => c.String());
            AlterColumn("dbo.People", "FName", c => c.String());
            AlterColumn("dbo.People", "IdentificationNo", c => c.String());
        }
    }
}
