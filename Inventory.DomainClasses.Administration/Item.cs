using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Item : EntityBase
    {
        public int ItemID { get; set; }
        public string Description { get; set; }
        public string PartNumber { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }

        public int BrandID { get; set; }
        public Brand Brand { get; set; }
    }
}
