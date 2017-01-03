using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{RackID}, Name:{Name}, IsActive:{IsActive}")]
    public class RackVM : EntityBaseVM
    {
        public int RackID { get; set; }
        public string Name { get; set; }
    }
}
