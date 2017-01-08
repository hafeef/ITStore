namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExpireAndWarrantyDatesToReceivedItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.ReceivedLineItems", "WarrantyDate", c => c.DateTime(nullable: false));
            AddColumn("Inventory.ReceivedLineItems", "ExpiryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Inventory.ReceivedLineItems", "ExpiryDate");
            DropColumn("Inventory.ReceivedLineItems", "WarrantyDate");
        }
    }
}
