using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{VendorID}, Name:{Name}")]
    public class VendorVM : EntityBaseVM
    {
        public int VendorID { get; set; }
        public string Name { get; set; }
    }
}
