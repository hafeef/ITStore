using Core.Common.BaseTypes;
using System;

namespace Inventory.DomainClasses.Inventory
{
    public class Transfer : EntityBase
    {
        public int TransferID { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
        public int FromWarehouseID { get; set; }
        public int ToWarehouseID { get; set; }
        public int FromRackID { get; set; }
        public int ToRackID { get; set; }
        public int FromShelfID { get; set; }
        public int ToShelfID { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
