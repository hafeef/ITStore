using Core.Common.BaseTypes;
using Inventory.DomainClasses.Inventory;
using System;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Data.Inventory
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() : base("Inventory")
        {
            this.Configuration.LazyLoadingEnabled = false;
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
            modelBuilder.Entity<ReceivedLineItem>().Ignore(rli => rli.ItemDescription);
            modelBuilder.Entity<ReceivedLineItem>().Ignore(rli => rli.PartNumber);
            modelBuilder.Entity<ReceivedLineItem>().Property(rli => rli.SerialNo).HasMaxLength(100);
            modelBuilder.Entity<ReceivedLineItem>().Property(rli => rli.SerialNo).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<PurchaseOrder>().Property(li => li.PoOrContractNumber).HasMaxLength(100);
            modelBuilder.Entity<PurchaseOrder>().Property(li => li.PoOrContractNumber).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));
            modelBuilder.HasDefaultSchema("Inventory");
        }
    }
}
