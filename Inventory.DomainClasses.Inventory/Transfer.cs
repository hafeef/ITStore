using Core.Common.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DomainClasses.Inventory
{
    public class Transfer : EntityBase
    {
        public int TransferID { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
        public int FromWarehouse { get; set; }
        public int ToWarehouse { get; set; }
        public int FromRack { get; set; }
        public int ToRack { get; set; }
        public int FromShelf { get; set; }
        public int ToShelf { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
