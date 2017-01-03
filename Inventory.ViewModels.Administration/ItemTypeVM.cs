using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{ItemTypeID}, Name:{Name}, IsActive:{IsActive}")]
    public class ItemTypeVM : EntityBaseVM
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
    }
}
