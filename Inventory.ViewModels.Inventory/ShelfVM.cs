using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{ShelfID}, Name:{Name}, IsActive:{IsActive}")]
    public class ShelfVM : EntityBaseVM
    {
        public int ShelfID { get; set; }
        public string Name { get; set; }
    }
}
