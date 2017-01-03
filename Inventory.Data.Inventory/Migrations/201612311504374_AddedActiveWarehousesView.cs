namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedActiveWarehousesView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveWarehouses
                  AS
                  SELECT W.WareHouseID, W.Name FROM Administration.Warehouses W WHERE W.IsActive = 1");
        }
        
        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveWarehouses");
        }
    }
}
