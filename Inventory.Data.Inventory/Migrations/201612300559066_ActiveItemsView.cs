namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ActiveItemsView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveItems
                  AS
                  SELECT I.ItemID, I.Description FROM Administration.Items I WHERE I.IsActive = 1");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveItems");
        }
    }
}
