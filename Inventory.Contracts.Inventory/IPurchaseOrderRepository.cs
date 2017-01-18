using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

namespace Inventory.Contracts.Inventory
{
    public interface IPurchaseOrderRepository
    {
        void CreatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        PurchaseOrderVM FindPurchaseOrderByPoOrContractNumber(string poOrContractNumber);
        PurchaseOrderVM FindPurchaseOrderIncludeReceivedItemsByPoOrContractNumber(string poOrContractNumber);
        void UpdatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        
        bool IsPurchaseOrderExists(string poOrContractNumber);
    }
}
