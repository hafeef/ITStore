using Core.Common.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModels.Inventory
{
    public class InventoryScrapVM : EntityBaseVM
    {
        public int InventoryScrapID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string SerialNo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (ReferenceEquals(obj, this))
                return true;
            else if (GetType() != obj.GetType())
                return false;
            var inventoryScrap = obj as InventoryScrapVM;

            return SerialNo == inventoryScrap.SerialNo && ItemID == inventoryScrap.ItemID;
        }

        public override int GetHashCode()
        {
            return ItemID.GetHashCode() ^ SerialNo.GetHashCode();
        }
    }
}
