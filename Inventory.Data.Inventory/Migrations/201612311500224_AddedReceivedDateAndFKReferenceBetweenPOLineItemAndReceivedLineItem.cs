namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedReceivedDateAndFKReferenceBetweenPOLineItemAndReceivedLineItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", c => c.Int(nullable: false));
            AddColumn("Inventory.ReceivedLineItems", "ReceivedDate", c => c.DateTime(nullable: false));
            CreateIndex("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID");
            AddForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", "Inventory.PurchaseOrderLineItems", "PurchaseOrderLineItemID");
        }
        
        public override void Down()
        {
            DropForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", "Inventory.PurchaseOrderLineItems");
            DropIndex("Inventory.ReceivedLineItems", new[] { "PurchaseOrderLineItemID" });
            DropColumn("Inventory.ReceivedLineItems", "ReceivedDate");
            DropColumn("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID");
        }
    }
}
