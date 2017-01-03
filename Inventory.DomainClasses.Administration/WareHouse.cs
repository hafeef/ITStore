using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Warehouse : EntityBase
    {
        public int WareHouseID { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }
        public Location Location { get; set; }
    }
}
