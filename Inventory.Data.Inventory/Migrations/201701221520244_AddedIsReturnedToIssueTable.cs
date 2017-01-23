namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsReturnedToIssueTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.InventoryIssues", "IsReturned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Inventory.InventoryIssues", "IsReturned");
        }
    }
}
