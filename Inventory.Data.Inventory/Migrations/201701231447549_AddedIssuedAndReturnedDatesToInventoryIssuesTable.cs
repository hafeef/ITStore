namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIssuedAndReturnedDatesToInventoryIssuesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Inventory.InventoryIssues", "IssuedDate", c => c.DateTime());
            AddColumn("Inventory.InventoryIssues", "ReturnedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("Inventory.InventoryIssues", "ReturnedDate");
            DropColumn("Inventory.InventoryIssues", "IssuedDate");
        }
    }
}
