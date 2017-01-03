using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{ItemID}, Description:{Description}, IsActive:{IsActive}")]
    public class ItemVM : EntityBaseVM
    {
        public int ItemID { get; set; }
        public string Description { get; set; }
        public string PartNumber { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }
    }
}
