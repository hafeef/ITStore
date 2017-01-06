using Core.Common.BaseTypes;
using Inventory.DomainClasses.Administration;
using System;
using System.Data.Entity;
using System.Linq;

namespace Inventory.Data.Administration
{
    public class AdminContext : DbContext
    {
        public AdminContext() : base("Inventory")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
        {
            var dateTimeHistories = ChangeTracker.Entries()
                                         .Where(
                                                entry => entry.Entity is EntityBase &&
                                                (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                                               )
                                         .Select(entry => entry.Entity as EntityBase);
            foreach (var history in dateTimeHistories)
            {
                history.ModifiedDateTime = DateTime.Now;

                if (history.CreatedDateTime == DateTime.MinValue)
                {
                    history.CreatedDateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Administration");
            //modelBuilder.Entity<HistoryRow>().ToTable(tableName: "__MigrationHistory", schemaName: "Administration");
            //modelBuilder.Entity<HistoryRow>().HasKey(h => h.MigrationId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
