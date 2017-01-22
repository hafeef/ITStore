namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedTransferEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Inventory.Transfers",
                c => new
                    {
                        TransferID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        SerialNo = c.String(maxLength: 100),
                        FromWarehouseID = c.Int(nullable: false),
                        ToWarehouseID = c.Int(nullable: false),
                        FromRackID = c.Int(nullable: false),
                        ToRackID = c.Int(nullable: false),
                        FromShelfID = c.Int(nullable: false),
                        ToShelfID = c.Int(nullable: false),
                        TransferDate = c.DateTime(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransferID)
                .Index(t => t.SerialNo);
            
        }
        
        public override void Down()
        {
            DropIndex("Inventory.Transfers", new[] { "SerialNo" });
            DropTable("Inventory.Transfers");
        }
    }
}
