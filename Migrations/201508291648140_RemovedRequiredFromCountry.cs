namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromCountry : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Countries", "PhoneIntlCode", c => c.String());
            AlterColumn("dbo.Countries", "Abbreviation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "Abbreviation", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "PhoneIntlCode", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
        }
    }
}
