using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Rack : EntityBase
    {
        public int RackID { get; set; }
        public string Name { get; set; }

    }
}
