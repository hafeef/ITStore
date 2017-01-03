using System;
using System.Diagnostics;

namespace Inventory.ViewModels.Inventory
{
    [Serializable, DebuggerDisplay("ID:{ItemID}, Description:{Description}")]
    public class ItemVM 
    {
        public int ItemID { get; set; }
        public string Description { get; set; }
    }
}
