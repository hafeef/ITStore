using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Brand : EntityBase
    {
        public int BrandID { get; set; }
        public string Name { get; set; }
    }
}
