namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedFooProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.PurchaseOrders", "Foo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Inventory.PurchaseOrders", "Foo");
        }
    }
}
