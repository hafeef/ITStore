using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Location : EntityBase
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
    }
}
