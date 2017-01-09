namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueIndexToSerialNoProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Inventory.ReceivedLineItems", "SerialNo", c => c.String(maxLength: 100));
            CreateIndex("Inventory.ReceivedLineItems", "SerialNo");
        }
        
        public override void Down()
        {
            DropIndex("Inventory.ReceivedLineItems", new[] { "SerialNo" });
            AlterColumn("Inventory.ReceivedLineItems", "SerialNo", c => c.String());
        }
    }
}
