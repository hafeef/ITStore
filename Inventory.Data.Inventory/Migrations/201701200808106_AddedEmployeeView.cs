namespace Inventory.Data.Inventory.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedEmployeeView : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW Inventory.ActiveEmployees
                  AS
                  SELECT E.EmployeeID, E.CivilID, E.Name FROM Administration.Employees E WHERE E.IsActive = 1");
        }
        
        public override void Down()
        {
            Sql(@"DROP VIEW Inventory.ActiveEmployees");
        }
    }
}
