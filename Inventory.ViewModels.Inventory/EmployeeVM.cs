using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{EmployeeID}, Name:{Name}, CivilID:{CivilID}")]
    public class EmployeeVM
    {
        public int EmployeeID { get; set; }
        public string CivilID { get; set; }
        public string Name { get; set; }
    }
}
