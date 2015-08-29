namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedIntnlCodeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "PhoneIntlCode", c => c.String());
            DropColumn("dbo.Countries", "PhoneIntlCod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "PhoneIntlCod", c => c.String());
            DropColumn("dbo.Countries", "PhoneIntlCode");
        }
    }
}
