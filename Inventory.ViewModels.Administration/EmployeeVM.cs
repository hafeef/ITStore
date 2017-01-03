using Core.Common.BaseTypes;
using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Administration
{
    [Serializable, DebuggerDisplay("ID:{EmployeeID}, Name:{Name}, CivilID:{CivilID}, IsActive:{IsActive}")]
    public class EmployeeVM : EntityBaseVM
    {
        public int EmployeeID { get; set; }
        public string CivilID { get; set; }
        public string Name { get; set; }
    }
}
