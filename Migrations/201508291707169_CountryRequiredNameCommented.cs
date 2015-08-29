namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryRequiredNameCommented : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
        }
    }
}
