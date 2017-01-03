namespace Inventory.Data.Administration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Administration.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "Administration.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "Administration.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        CivilID = c.String(),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "Administration.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        PartNumber = c.String(),
                        CategoryID = c.Int(nullable: false),
                        ItemTypeID = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("Administration.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("Administration.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("Administration.ItemTypes", t => t.ItemTypeID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.ItemTypeID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "Administration.ItemTypes",
                c => new
                    {
                        ItemTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemTypeID);
            
            CreateTable(
                "Administration.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "Administration.Racks",
                c => new
                    {
                        RackID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RackID);
            
            CreateTable(
                "Administration.Shelves",
                c => new
                    {
                        ShelfID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShelfID);
            
            CreateTable(
                "Administration.Vendors",
                c => new
                    {
                        VendorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        TelephoneNo = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VendorID);
            
            CreateTable(
                "Administration.Warehouses",
                c => new
                    {
                        WareHouseID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LocationID = c.Int(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WareHouseID)
                .ForeignKey("Administration.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Administration.Warehouses", "LocationID", "Administration.Locations");
            DropForeignKey("Administration.Items", "ItemTypeID", "Administration.ItemTypes");
            DropForeignKey("Administration.Items", "CategoryID", "Administration.Categories");
            DropForeignKey("Administration.Items", "BrandID", "Administration.Brands");
            DropIndex("Administration.Warehouses", new[] { "LocationID" });
            DropIndex("Administration.Items", new[] { "BrandID" });
            DropIndex("Administration.Items", new[] { "ItemTypeID" });
            DropIndex("Administration.Items", new[] { "CategoryID" });
            DropTable("Administration.Warehouses");
            DropTable("Administration.Vendors");
            DropTable("Administration.Shelves");
            DropTable("Administration.Racks");
            DropTable("Administration.Locations");
            DropTable("Administration.ItemTypes");
            DropTable("Administration.Items");
            DropTable("Administration.Employees");
            DropTable("Administration.Categories");
            DropTable("Administration.Brands");
        }
    }
}
