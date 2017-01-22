namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedRelationshipsBetweenPOLineItemsAndReceivedItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", "Inventory.PurchaseOrderLineItems");
            DropIndex("Inventory.ReceivedLineItems", new[] { "PurchaseOrderLineItemID" });
        }
        
        public override void Down()
        {
            CreateIndex("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID");
            AddForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", "Inventory.PurchaseOrderLineItems", "PurchaseOrderLineItemID");
        }
    }
}
