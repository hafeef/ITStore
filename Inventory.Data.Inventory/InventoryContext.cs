﻿using Core.Common.BaseTypes;
using Inventory.DomainClasses.Inventory;
using System;
using System.Data.Entity;
using System.Linq;

namespace Inventory.Data.Inventory
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() : base("Inventory")
        {

        }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public DbSet<ReceivedLineItem> ReceivedLineItems { get; set; }

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
            modelBuilder.Entity<PurchaseOrderLineItem>().Ignore(li => li.ItemDescription);
            modelBuilder.Entity<PurchaseOrderLineItem>().Ignore(li => li.PartNumber);
            modelBuilder.Entity<ReceivedLineItem>().HasRequired(rli => rli.PurchaseOrderLineItem).WithMany().WillCascadeOnDelete(false);
            modelBuilder.HasDefaultSchema("Inventory");
        }
    }
}
