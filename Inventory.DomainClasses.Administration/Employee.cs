using Core.Common.BaseTypes;

namespace Inventory.DomainClasses.Administration
{
    public class Employee : EntityBase
    {
        public int EmployeeID { get; set; }
        public string CivilID { get; set; }
        public string Name { get; set; }
    }
}
