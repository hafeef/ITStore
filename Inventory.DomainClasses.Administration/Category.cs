using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Category : EntityBase
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}
