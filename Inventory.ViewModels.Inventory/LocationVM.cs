using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{LocationID}, Name:{Name}, IsActive:{IsActive}")]
    public class LocationVM : EntityBaseVM
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
    }
}
