using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{CategoryID}, Name:{Name}, IsActive:{IsActive}")]
    public class CategoryVM : EntityBaseVM
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}
