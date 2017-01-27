namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInventoryScrapTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Inventory.InventoryScraps",
                c => new
                    {
                        InventoryScrapID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        SerialNo = c.String(maxLength: 100),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryScrapID)
                .Index(t => t.SerialNo, unique: true, name: "UI_InventoryScrap_SerialNo");
            
        }
        
        public override void Down()
        {
            DropIndex("Inventory.InventoryScraps", "UI_InventoryScrap_SerialNo");
            DropTable("Inventory.InventoryScraps");
        }
    }
}
