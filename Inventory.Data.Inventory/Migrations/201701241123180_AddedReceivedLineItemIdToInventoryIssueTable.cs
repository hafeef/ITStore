namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReceivedLineItemIdToInventoryIssueTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.InventoryIssues", "ReceivedLineItemID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Inventory.InventoryIssues", "ReceivedLineItemID");
        }
    }
}
