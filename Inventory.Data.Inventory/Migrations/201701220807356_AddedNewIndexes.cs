namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewIndexes : DbMigration
    {
        public override void Up()
        {
            DropIndex("Inventory.PurchaseOrders", new[] { "PoOrContractNumber" });
            DropIndex("Inventory.ReceivedLineItems", new[] { "SerialNo" });
            RenameIndex(table: "Inventory.Transfers", name: "IX_SerialNo", newName: "NIX_Transfer_SerialNo");
            CreateTable(
                "Inventory.InventoryIssues",
                c => new
                    {
                        InventoryIssueID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        SerialNo = c.String(),
                        HelpDeskTicket = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryIssueID);
            
            CreateIndex("Inventory.PurchaseOrders", "PoOrContractNumber", unique: true, name: "UI_PurchaseOrder_PoOrContractNumber");
            CreateIndex("Inventory.ReceivedLineItems", "SerialNo", unique: true, name: "UI_ReceivedLineItem_SerialNo");
        }
        
        public override void Down()
        {
            DropIndex("Inventory.ReceivedLineItems", "UI_ReceivedLineItem_SerialNo");
            DropIndex("Inventory.PurchaseOrders", "UI_PurchaseOrder_PoOrContractNumber");
            DropTable("Inventory.InventoryIssues");
            RenameIndex(table: "Inventory.Transfers", name: "NIX_Transfer_SerialNo", newName: "IX_SerialNo");
            CreateIndex("Inventory.ReceivedLineItems", "SerialNo");
            CreateIndex("Inventory.PurchaseOrders", "PoOrContractNumber");
        }
    }
}
