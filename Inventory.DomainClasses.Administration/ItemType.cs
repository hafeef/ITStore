using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class ItemType : EntityBase
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
    }
}
