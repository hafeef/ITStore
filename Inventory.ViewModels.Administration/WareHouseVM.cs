using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{WareHouseID}, Name:{Name}, Location:{LocationName}, IsActive:{IsActive}")]
    public class WareHouseVM : EntityBaseVM
    {
        public int WareHouseID { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
}
