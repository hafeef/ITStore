﻿using Inventory.DomainClasses.Inventory;
using System.Data.Entity;

namespace Inventory.Data.Inventory
{
    public class AdminReferenceContext : DbContext
    {
        public AdminReferenceContext() : base("Inventory")
        {
            Database.SetInitializer<AdminReferenceContext>(null);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Administration");
            modelBuilder.Entity<Item>().ToTable("ActiveItems", "Inventory");
            modelBuilder.Entity<Vendor>().ToTable("ActiveVendors", "Inventory");
            modelBuilder.Entity<Location>().ToTable("ActiveLocations", "Inventory");
            modelBuilder.Entity<Warehouse>().ToTable("ActiveWarehouses", "Inventory");
            modelBuilder.Entity<Rack>().ToTable("ActiveRacks", "Inventory");
            modelBuilder.Entity<Shelf>().ToTable("ActiveShelves", "Inventory");
            base.OnModelCreating(modelBuilder);
        }
    }
}