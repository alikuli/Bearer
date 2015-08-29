namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDefaultInformToIDRevert : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.People", "DefaultInformToId");
            AddForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "DefaultInformToId" });
        }
    }
}
