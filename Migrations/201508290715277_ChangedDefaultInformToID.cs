namespace Bearer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDefaultInformToID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "DefaultInformToId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.People", "DefaultInformToId");
            AddForeignKey("dbo.People", "DefaultInformToId", "dbo.Addresses", "Id");
        }
    }
}
