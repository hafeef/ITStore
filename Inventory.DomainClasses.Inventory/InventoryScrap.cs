using Core.Common.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DomainClasses.Inventory
{
    public class InventoryScrap : EntityBase
    {
        public int InventoryScrapID { get; set; }
        public int ItemID { get; set; }
        public string SerialNo { get; set; }
    }
}
