namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedActiveRacksView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveRacks
                  AS
                  SELECT R.RackID, R.Name FROM Administration.Racks R WHERE R.IsActive = 1");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveRacks");
        }
    }
}
