namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InventoryInitialDb : DbMigration
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
                    PurchasedQuantity = c.Int(nullable: false),
                    Price = c.Double(nullable: false),
                    Total = c.Double(nullable: false),
                    ReceivedQuatity = c.Int(nullable: false),
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
                    POCreatedDate = c.DateTime(nullable: false),
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
                    PurchaseOrderLineItemID = c.Int(nullable: false),
                    ReceivedDate = c.DateTime(nullable: false),
                    ItemID = c.Int(nullable: false),
                    Price = c.Double(nullable: false),
                    Total = c.Double(nullable: false),
                    SerialNo = c.String(),
                    WareHouseID = c.Int(nullable: false),
                    ShelfID = c.Int(nullable: false),
                    RackID = c.Int(nullable: false),
                    CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ReceivedLineItemID)
                .ForeignKey("Inventory.PurchaseOrders", t => t.PurchaseOrderID, cascadeDelete: true)
                .ForeignKey("Inventory.PurchaseOrderLineItems", t => t.PurchaseOrderLineItemID)
                .Index(t => t.PurchaseOrderID)
                .Index(t => t.PurchaseOrderLineItemID);

            Sql(@"CREATE VIEW Inventory.ActiveItems
                  AS
                  SELECT I.ItemID, I.Description FROM Administration.Items I WHERE I.IsActive = 1");

            Sql(@"CREATE VIEW Inventory.ActiveVendors
                  AS
                  SELECT V.VendorID,V.Name FROM Administration.Vendors V WHERE V.IsActive = 1");

            Sql(@"CREATE VIEW Inventory.ActiveWarehouses
                  AS
                  SELECT W.WareHouseID, W.Name FROM Administration.Warehouses W WHERE W.IsActive = 1");

            Sql(@"CREATE VIEW Inventory.ActiveLocations
                  AS
                  SELECT L.LocationID, L.Name FROM Administration.Locations L WHERE L.IsActive = 1");

            Sql(@"CREATE VIEW Inventory.ActiveShelves
                  AS
                  SELECT S.ShelfID, S.Name FROM Administration.Shelves S WHERE S.IsActive = 1");

            Sql(@"CREATE VIEW Inventory.ActiveRacks
                  AS
                  SELECT R.RackID, R.Name FROM Administration.Racks R WHERE R.IsActive = 1");

        }

        public override void Down()
        {
            DropForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderLineItemID", "Inventory.PurchaseOrderLineItems");
            DropForeignKey("Inventory.ReceivedLineItems", "PurchaseOrderID", "Inventory.PurchaseOrders");
            DropForeignKey("Inventory.PurchaseOrderLineItems", "PurchaseOrderID", "Inventory.PurchaseOrders");
            DropIndex("Inventory.ReceivedLineItems", new[] { "PurchaseOrderLineItemID" });
            DropIndex("Inventory.ReceivedLineItems", new[] { "PurchaseOrderID" });
            DropIndex("Inventory.PurchaseOrderLineItems", new[] { "PurchaseOrderID" });
            DropTable("Inventory.ReceivedLineItems");
            DropTable("Inventory.PurchaseOrders");
            DropTable("Inventory.PurchaseOrderLineItems");

            Sql(@"DROP VIEW Inventory.ActiveRacks");
            Sql(@"DROP VIEW Inventory.ActiveShelves");
            Sql(@"DROP VIEW Inventory.ActiveLocations");
            Sql(@"DROP VIEW Inventory.ActiveWarehouses");
            Sql(@"DROP VIEW Inventory.ActiveVendors");
            Sql(@"DROP VIEW Inventory.ActiveItems");
        }
    }
}
