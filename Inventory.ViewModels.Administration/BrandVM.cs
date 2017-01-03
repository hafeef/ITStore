using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{BrandID}, Name:{Name}, IsActive:{IsActive}")]
    public class BrandVM : EntityBaseVM
    {
        public int BrandID { get; set; }
        public string Name { get; set; }

    }
}
