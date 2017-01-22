using Inventory.ViewModels.Inventory;

namespace Inventory.Contracts.Inventory
{
    public interface IPurchaseOrderRepository
    {
        void CreatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        PurchaseOrderVM FindPurchaseOrderByPoOrContractNumber(string poOrContractNumber);
        PurchaseOrderVM FindPurchaseOrderIncludeReceivedItemsByPoOrContractNumber(string poOrContractNumber);
        void UpdatePurchaseOrder(PurchaseOrderVM newPurchaseOrder);
        bool IsPurchaseOrderExists(string poOrContractNumber);
        string[] AreSerialNumbersExists(string[] serialNo);
    }
}
