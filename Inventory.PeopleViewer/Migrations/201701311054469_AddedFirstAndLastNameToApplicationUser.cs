namespace Inventory.PeopleViewer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFirstAndLastNameToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("Security.AspNetUsers", "FirstName", c => c.String(maxLength: 30));
            AddColumn("Security.AspNetUsers", "LastName", c => c.String(maxLength: 30));
            AddColumn("Security.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Security.AspNetUsers", "Gender");
            DropColumn("Security.AspNetUsers", "LastName");
            DropColumn("Security.AspNetUsers", "FirstName");
        }
    }
}
