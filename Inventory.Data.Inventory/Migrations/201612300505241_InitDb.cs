namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Inventory.PurchaseOrderLineItems",
                c => new
                    {
                        PurchaseOrderLineItemID = c.Int(nullable: false, identity: true),
                        PurchaseOrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderLineItemID)
                .ForeignKey("Inventory.PurchaseOrders", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.PurchaseOrderID);
            
            CreateTable(
                "Inventory.PurchaseOrders",
                c => new
                    {
                        PurchaseOrderID = c.Int(nullable: false, identity: true),
                        VendorID = c.Int(nullable: false),
                        POTypeValue = c.Int(nullable: false),
                        POTypeText = c.String(),
                        PoOrContractNumber = c.String(),
                        GrandTotal = c.Double(nullable: false),
                        ReceivedTotal = c.Double(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderID);
            
            CreateTable(
                "Inventory.ReceivedLineItems",
                c => new
                    {
                        ReceivedLineItemID = c.Int(nullable: false, identity: true),
                        PurchaseOrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        SerialNo = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReceivedLineItemID)
                .ForeignKey("Inventory.PurchaseOrders", t => t.PurchaseOrderID, cascadeDelete: true)
                .Index(t => t.PurchaseOrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderID", "Inventory.PurchaseOrders");
            DropForeignKey("Inventory.PurchaseOrderLineItems", "PurchaseOrderID", "Inventory.PurchaseOrders");
            DropIndex("Inventory.ReceivedLineItems", new[] { "PurchaseOrderID" });
            DropIndex("Inventory.PurchaseOrderLineItems", new[] { "PurchaseOrderID" });
            DropTable("Inventory.ReceivedLineItems");
            DropTable("Inventory.PurchaseOrders");
            DropTable("Inventory.PurchaseOrderLineItems");
        }
    }
}
