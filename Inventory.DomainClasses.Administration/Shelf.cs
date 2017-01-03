using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Shelf : EntityBase
    {
        public int ShelfID { get; set; }
        public string Name { get; set; }

    }
}
