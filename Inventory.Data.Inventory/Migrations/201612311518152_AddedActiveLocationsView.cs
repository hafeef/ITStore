namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedActiveLocationsView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveLocations
                  AS
                  SELECT L.LocationID, L.Name FROM Administration.Locations L WHERE L.IsActive = 1");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveLocations");
        }
    }
}
