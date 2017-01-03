namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedActiveShelvesView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveShelves
                  AS
                  SELECT S.ShelfID, S.Name FROM Administration.Shelves S WHERE S.IsActive = 1");
        }
        
        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveShelves");
        }
    }
}
