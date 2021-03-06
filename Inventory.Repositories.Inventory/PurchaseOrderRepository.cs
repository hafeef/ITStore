﻿using System;
using System.Linq;
using System.Data.Entity;
using Inventory.Contracts.Inventory;
using Inventory.DomainClasses.Inventory;
using Inventory.Data.Inventory;
using Inventory.ViewModels.Inventory;
using System.Transactions;
using Core.Common.Resolvers;
using Core.Common.Enums;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace Inventory.Repositories.Inventory
{
    public class PurchaseOrderRepository : DataRepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(InventoryContext context) : base(context)
        {

        }

        public void CreatePurchaseOrder(PurchaseOrderVM newPurchaseOrder)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    var purchaseOrder = AutoMapper.Mapper.Map<PurchaseOrder>(newPurchaseOrder);
                    Insert(purchaseOrder);
                    transaction.Commit();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdatePurchaseOrder(PurchaseOrderVM newPurchaseOrder)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        var purchaseOrder = AutoMapper.Mapper.Map<PurchaseOrder>(newPurchaseOrder);
                        foreach (var item in purchaseOrder.PurchaseOrderLineItems.Where(li => li.EntityState != ObjectState.Unchanged))
                            context.Entry(item).State = StateResolver.Resolve(item.EntityState);
                        foreach (var item in purchaseOrder.ReceivedLineItems.Where(rli => rli.EntityState != ObjectState.Unchanged))
                            context.Entry(item).State = StateResolver.Resolve(item.EntityState);
                        context.SaveChanges();
                        context.Entry(purchaseOrder).State = StateResolver.Resolve(purchaseOrder.EntityState);
                        context.SaveChanges();
                        scope.Complete();
                    }
                }
            }
            catch (Exception Ex)
            {

                HandleException(Ex);
            }
        }

        public virtual void HandleException(Exception exception)
        {

            DbUpdateException dbUpdateEx = exception as DbUpdateException;
            if (dbUpdateEx != null)
            {
                if (dbUpdateEx != null
                        && dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    SqlException sqlException = dbUpdateEx.InnerException.InnerException as SqlException;
                    if (sqlException != null)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                            case 547:   // Constraint check violation
                            case 2601:
                                {
                                    int startIndex = sqlException.Message.IndexOf('(');
                                    int lastIndex = sqlException.Message.IndexOf(')');
                                    string serialNo = sqlException.Message.Substring(startIndex, lastIndex - startIndex + 1);
                                    throw new ApplicationException($"The {serialNo} serial number alread exists in the database");
                                }
                            default:
                                // A custom exception of yours for other DB issues
                                throw exception;
                        }
                    }
                }
            }
        }


        public PurchaseOrderVM FindPurchaseOrderByPoOrContractNumber(string poOrContractNumber)
        {
            try
            {
                var purchaseOrder = FindPurchaseOrder(poOrContractNumber);
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    if (purchaseOrder != null)
                    {
                        int[] itemsIDs = purchaseOrder.PurchaseOrderLineItems.Select(li => li.ItemID).Distinct().ToArray();
                        var items = context.Items.AsNoTracking().Where(i => itemsIDs.Contains(i.ItemID)).ToList();
                        purchaseOrder.PurchaseOrderLineItems = purchaseOrder.PurchaseOrderLineItems.Join(items, li => li.ItemID, i => i.ItemID, (li, i) => { li.ItemDescription = i.Description; li.PartNumber = i.PartNumber; return li; }).ToList();
                    }
                }
                return AutoMapper.Mapper.Map<PurchaseOrderVM>(purchaseOrder);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public PurchaseOrderVM FindPurchaseOrderIncludeReceivedItemsByPoOrContractNumber(string poOrContractNumber)
        {
            try
            {
                var purchaseOrder = FindPurchaseOrder(poOrContractNumber);
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    if (purchaseOrder != null)
                    {
                        int[] itemsIDs = purchaseOrder.PurchaseOrderLineItems.Select(li => li.ItemID).Distinct().ToArray();
                        var items = context.Items.AsNoTracking().Where(i => itemsIDs.Contains(i.ItemID)).ToList();
                        purchaseOrder.PurchaseOrderLineItems.Join(items, li => li.ItemID, i => i.ItemID, (li, i) => { li.ItemDescription = i.Description; li.PartNumber = i.PartNumber; return li; }).ToList();
                        if (purchaseOrder.ReceivedLineItems != null && purchaseOrder.ReceivedLineItems.Count > 0)
                            purchaseOrder.ReceivedLineItems.Join(items, rli => rli.ItemID, i => i.ItemID, (rli, i) => { rli.ItemDescription = i.Description; rli.PartNumber = i.PartNumber; return rli; }).ToList();
                    }
                }
                return AutoMapper.Mapper.Map<PurchaseOrderVM>(purchaseOrder);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private PurchaseOrder FindPurchaseOrder(string poOrContractNumber)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    var purchaseOrder = context.PurchaseOrders.FirstOrDefault(po => po.IsActive == true && po.PoOrContractNumber == poOrContractNumber);
                    if (purchaseOrder != null)
                    {
                        context.Entry(purchaseOrder)
                               .Collection(po => po.PurchaseOrderLineItems)
                               .Load();
                        context.Entry(purchaseOrder)
                               .Collection(po => po.ReceivedLineItems)
                               .Load();
                    }
                    return purchaseOrder;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



        public bool IsPurchaseOrderExists(string poOrContractNumber)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    var purchaseOrder = context.PurchaseOrders.AsNoTracking().FirstOrDefault(po => po.PoOrContractNumber == poOrContractNumber);
                    if (purchaseOrder != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string[] AreSerialNumbersExists(string[] serialNo)
        {
            try
            {
                using (InventoryContext context = new InventoryContext())
                {
                    return context.ReceivedLineItems
                                  .Where(ri => serialNo.Contains(ri.SerialNo))
                                  .Select(ri => ri.SerialNo)
                                  .ToArray();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
