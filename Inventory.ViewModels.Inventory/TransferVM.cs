using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{TransferID},FromWarehouse:{FromWarehouseName},ToWarehouse:{ToWarehouseName},FromRack:{FromRackName},ToRack:{ToRackName},FromShelf:{FromShelfName},ToShelf:{ToShelfName},EntityState:{EntityState}")]
    public class TransferVM : EntityBaseVM
    {
        public int TransferID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string SerialNo { get; set; }
        public int FromWarehouseID { get; set; }
        public string FromWarehouseName { get; set; }
        public int ToWarehouseID { get; set; }
        public string ToWarehouseName { get; set; }
        public int FromRackID { get; set; }
        public string FromRackName { get; set; }
        public int ToRackID { get; set; }
        public string ToRackName { get; set; }
        public int FromShelfID { get; set; }
        public string FromShelfName { get; set; }
        public int ToShelfID { get; set; }
        public string ToShelfName { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
