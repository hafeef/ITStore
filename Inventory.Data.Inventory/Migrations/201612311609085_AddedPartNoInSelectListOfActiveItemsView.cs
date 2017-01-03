namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedPartNoInSelectListOfActiveItemsView : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER VIEW Inventory.ActiveItems
                  AS
                  SELECT I.ItemID, I.Description, I.PartNumber FROM Administration.Items I WHERE I.IsActive = 1");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveItems");
        }
    }
}
