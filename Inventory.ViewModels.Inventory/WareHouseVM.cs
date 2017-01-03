using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{WareHouseID}, Name:{Name}, IsActive:{IsActive}")]
    public class WareHouseVM : EntityBaseVM
    {
        public int WareHouseID { get; set; }
        public string Name { get; set; }
    }
}
