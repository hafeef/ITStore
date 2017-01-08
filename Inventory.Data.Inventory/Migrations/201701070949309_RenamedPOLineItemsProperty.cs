namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedPOLineItemsProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.PurchaseOrderLineItems", "ReceivedQuantity", c => c.Int(nullable: false));
            DropColumn("Inventory.PurchaseOrderLineItems", "ReceivedQuatity");
        }
        
        public override void Down()
        {
            AddColumn("Inventory.PurchaseOrderLineItems", "ReceivedQuatity", c => c.Int(nullable: false));
            DropColumn("Inventory.PurchaseOrderLineItems", "ReceivedQuantity");
        }
    }
}
