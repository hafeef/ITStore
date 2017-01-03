namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedFooProp : DbMigration
    {
        public override void Up()
        {
            DropColumn("Inventory.PurchaseOrders", "Foo");
        }
        
        public override void Down()
        {
            AddColumn("Inventory.PurchaseOrders", "Foo", c => c.String());
        }
    }
}
