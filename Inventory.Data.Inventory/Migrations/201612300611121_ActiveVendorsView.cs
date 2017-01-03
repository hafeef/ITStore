namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ActiveVendorsView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveVendors
                  AS
                  SELECT V.VendorID,V.Name FROM Administration.Vendors V WHERE V.IsActive = 1");
        }

        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveVendors");
        }
    }
}
