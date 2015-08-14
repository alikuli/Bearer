namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCNICfieldOfPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IdentificationNo", c => c.String());
            AddColumn("dbo.People", "BlackListed", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "CNIC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "CNIC", c => c.String());
            DropColumn("dbo.People", "BlackListed");
            DropColumn("dbo.People", "IdentificationNo");
        }
    }
}
