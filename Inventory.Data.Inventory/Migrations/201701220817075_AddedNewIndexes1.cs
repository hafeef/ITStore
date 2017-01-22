namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewIndexes1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("Inventory.PurchaseOrders", "UI_PurchaseOrder_PoOrContractNumber");
            AlterColumn("Inventory.InventoryIssues", "SerialNo", c => c.String(maxLength: 100));
            AlterColumn("Inventory.PurchaseOrders", "PoOrContractNumber", c => c.String());
            CreateIndex("Inventory.InventoryIssues", "SerialNo", name: "NIX_InventoryIssue_SerialNo");
        }
        
        public override void Down()
        {
            DropIndex("Inventory.InventoryIssues", "NIX_InventoryIssue_SerialNo");
            AlterColumn("Inventory.PurchaseOrders", "PoOrContractNumber", c => c.String(maxLength: 100));
            AlterColumn("Inventory.InventoryIssues", "SerialNo", c => c.String());
            CreateIndex("Inventory.PurchaseOrders", "PoOrContractNumber", unique: true, name: "UI_PurchaseOrder_PoOrContractNumber");
        }
    }
}
