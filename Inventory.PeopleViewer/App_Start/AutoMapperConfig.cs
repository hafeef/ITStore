using AutoMapper;
using Inventory.DomainClasses.Administration;
using Inventory.DomainClasses.Inventory;
using Inventory.ViewModels.Inventory;

namespace Inventory.PeopleViewer.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMpas()
        {
            Mapper.Initialize(config =>
            {
                #region GlobalSettings
                config.AddGlobalIgnore("CreatedDateTime");
                config.AddGlobalIgnore("ModifiedDateTime");
                #endregion

                #region AdministrationMappings
                config.CreateMap<ViewModels.Administration.RackVM, DomainClasses.Administration.Rack>();
                config.CreateMap<DomainClasses.Administration.Rack, ViewModels.Administration.RackVM>();

                config.CreateMap<ViewModels.Administration.ShelfVM, DomainClasses.Administration.Shelf>();
                config.CreateMap<DomainClasses.Administration.Shelf, ViewModels.Administration.ShelfVM>();

                config.CreateMap<ViewModels.Administration.BrandVM, DomainClasses.Administration.Brand>();
                config.CreateMap<DomainClasses.Administration.Brand, ViewModels.Administration.BrandVM>();

                config.CreateMap<ViewModels.Administration.LocationVM, DomainClasses.Administration.Location>();
                config.CreateMap<DomainClasses.Administration.Location, ViewModels.Administration.LocationVM>();

                config.CreateMap<ViewModels.Administration.EmployeeVM, DomainClasses.Administration.Employee>();
                config.CreateMap<DomainClasses.Administration.Employee, ViewModels.Administration.EmployeeVM>();

                config.CreateMap<ViewModels.Administration.CategoryVM, Category>();
                config.CreateMap<Category, ViewModels.Administration.CategoryVM>();

                config.CreateMap<ViewModels.Administration.ItemTypeVM, DomainClasses.Administration.ItemType>();
                config.CreateMap<DomainClasses.Administration.ItemType, ViewModels.Administration.ItemTypeVM>();

                config.CreateMap<ViewModels.Administration.VendorVM, DomainClasses.Administration.Vendor>();
                config.CreateMap<DomainClasses.Administration.Vendor, ViewModels.Administration.VendorVM>();

                config.CreateMap<ViewModels.Administration.WareHouseVM, DomainClasses.Administration.Warehouse>();
                config.CreateMap<DomainClasses.Administration.Warehouse, ViewModels.Administration.WareHouseVM>()
                      .ForMember(wvm => wvm.LocationName, wh => wh.MapFrom(src => src.Location.Name));

                config.CreateMap<ViewModels.Administration.ItemVM, DomainClasses.Administration.Item>();
                config.CreateMap<DomainClasses.Administration.Item, ViewModels.Administration.ItemVM>()
                      .ForMember(ivm => ivm.BrandName, item => item.MapFrom(src => src.Brand.Name))
                      .ForMember(ivm => ivm.CategoryName, item => item.MapFrom(src => src.Category.Name))
                      .ForMember(ivm => ivm.ItemTypeName, item => item.MapFrom(src => src.ItemType.Name));
                #endregion

                #region InventoryMappings
                config.CreateMap<ViewModels.Inventory.VendorVM, DomainClasses.Inventory.Vendor>();
                config.CreateMap<DomainClasses.Inventory.Vendor, ViewModels.Inventory.VendorVM>();

                config.CreateMap<ViewModels.Inventory.LocationVM, DomainClasses.Inventory.Location>();
                config.CreateMap<DomainClasses.Inventory.Location, ViewModels.Inventory.LocationVM>();

                config.CreateMap<ViewModels.Inventory.RackVM, DomainClasses.Inventory.Rack>();
                config.CreateMap<DomainClasses.Inventory.Rack, ViewModels.Inventory.RackVM>();

                config.CreateMap<ViewModels.Inventory.ShelfVM, DomainClasses.Inventory.Shelf>();
                config.CreateMap<DomainClasses.Inventory.Shelf, ViewModels.Inventory.ShelfVM>();

                config.CreateMap<ViewModels.Inventory.WareHouseVM, DomainClasses.Inventory.Warehouse>();
                config.CreateMap<DomainClasses.Inventory.Warehouse, ViewModels.Inventory.WareHouseVM>();

                config.CreateMap<ViewModels.Inventory.ItemVM, DomainClasses.Inventory.Item>();
                config.CreateMap<DomainClasses.Inventory.Item, ViewModels.Inventory.ItemVM>();

                config.CreateMap<PurchaseOrderVM, PurchaseOrder>();
                config.CreateMap<PurchaseOrder, PurchaseOrderVM>();

                config.CreateMap<PurchaseOrderLineItemVM, PurchaseOrderLineItem>();
                config.CreateMap<PurchaseOrderLineItem, PurchaseOrderLineItemVM>();
                config.CreateMap<PurchaseOrderLineItemVM, PurchaseOrderLineItemVM>();

                config.CreateMap<ReceivedLineItemVM, ReceivedLineItem>();
                config.CreateMap<ReceivedLineItem, ReceivedLineItemVM>();
                config.CreateMap<ReceivedLineItemVM, ReceivedLineItemVM>();

                config.CreateMap<TransferVM, Transfer>();
                config.CreateMap<Transfer, TransferVM>();
                #endregion



            });
        }
    }
}