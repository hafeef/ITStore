namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedPoCreatedDateToPurchaseOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.PurchaseOrders", "POCreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Inventory.PurchaseOrders", "POCreatedDate");
        }
    }
}
