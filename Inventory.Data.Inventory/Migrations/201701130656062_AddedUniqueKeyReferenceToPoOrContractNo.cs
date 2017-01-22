namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedUniqueKeyReferenceToPoOrContractNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Inventory.PurchaseOrders", "PoOrContractNumber", c => c.String(maxLength: 100));
            CreateIndex("Inventory.PurchaseOrders", "PoOrContractNumber");
        }
        
        public override void Down()
        {
            DropIndex("Inventory.PurchaseOrders", new[] { "PoOrContractNumber" });
            AlterColumn("Inventory.PurchaseOrders", "PoOrContractNumber", c => c.String());
        }
    }
}
