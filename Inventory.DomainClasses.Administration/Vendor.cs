using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Vendor : EntityBase
    {
        public int VendorID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
    }
}
