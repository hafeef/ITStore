namespace Inventory.Data.Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HelpDeskTicketLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Inventory.InventoryIssues", "HelpDeskTicket", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("Inventory.InventoryIssues", "HelpDeskTicket", c => c.String());
        }
    }
}
