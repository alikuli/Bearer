namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAbreviationToCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Abbreviation", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "PhoneIntlCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "PhoneIntlCode", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            DropColumn("dbo.Countries", "Abbreviation");
        }
    }
}
