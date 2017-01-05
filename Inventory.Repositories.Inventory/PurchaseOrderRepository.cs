﻿using System;
using System.Linq;
using System.Data.Entity;
using Inventory.Contracts.Inventory;
using Inventory.DomainClasses.Inventory;
using Inventory.Data.Inventory;
using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

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
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    var purchaseOrder = AutoMapper.Mapper.Map<PurchaseOrder>(newPurchaseOrder);
                    UpdateGraph(purchaseOrder);
                    transaction.Commit();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public PurchaseOrderVM FindPurchaseOrderByPoOrContractNumber(string poOrContractNumber)
        {
            try
            {
                var purchaseOrder = FindPurchaseOrder(poOrContractNumber);
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    string itemsIDs = string.Join(",", purchaseOrder.PurchaseOrderLineItems.Select(li => li.ItemID.ToString()));
                    purchaseOrder.PurchaseOrderLineItems = purchaseOrder.PurchaseOrderLineItems.Join(context.Items, li => li.ItemID, i => i.ItemID, (li, i) => { li.ItemDescription = i.Description; li.PartNumber = i.PartNumber; return li; }).ToList();
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
                               .Query()
                               .Where(li => li.IsActive == true)
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

        public List<VendorVM> GetAllVendors()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var vendors = context.Vendors.ToList();
                    return AutoMapper.Mapper.Map<List<Vendor>, List<VendorVM>>(vendors);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ItemVM> GetItemByDescription(string itemDescription)
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var items = context.Items.Where(i => i.Description.Contains(itemDescription)).ToList();
                    return AutoMapper.Mapper.Map<List<Item>, List<ItemVM>>(items);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<LocationVM> GetAllLocations()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var locations = context.Locations.ToList();
                    return AutoMapper.Mapper.Map<List<Location>, List<LocationVM>>(locations);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<WareHouseVM> GetAllWarehouses()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var warehouses = context.Warehouses.ToList();
                    return AutoMapper.Mapper.Map<List<Warehouse>, List<WareHouseVM>>(warehouses);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<RackVM> GetAllRacks()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var racks = context.Racks.ToList();
                    return AutoMapper.Mapper.Map<List<Rack>, List<RackVM>>(racks);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ShelfVM> GetAllShelves()
        {
            try
            {
                using (AdminReferenceContext context = new AdminReferenceContext())
                {
                    var shelves = context.Shelves.ToList();
                    return AutoMapper.Mapper.Map<List<Shelf>, List<ShelfVM>>(shelves);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}