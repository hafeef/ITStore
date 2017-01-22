namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedPriceTotalFromReceivedItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("Inventory.ReceivedLineItems", "Price");
            DropColumn("Inventory.ReceivedLineItems", "Total");
        }
        
        public override void Down()
        {
            AddColumn("Inventory.ReceivedLineItems", "Total", c => c.Double(nullable: false));
            AddColumn("Inventory.ReceivedLineItems", "Price", c => c.Double(nullable: false));
        }
    }
}
