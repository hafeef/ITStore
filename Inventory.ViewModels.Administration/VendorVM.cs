using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{VendorID}, Name:{Name}, IsActive:{IsActive}")]
    public class VendorVM : EntityBaseVM
    {
        public int VendorID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
    }
}
