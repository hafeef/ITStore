using Inventory.ViewModels.Inventory;
using System.Collections.Generic;

namespace Inventory.Contracts.Inventory
{
    public interface ITransferRepository
    {
        List<TransferVM> SearchTransfers(int itemID);
        List<TransferVM> SearchTransfers(int itemID, string[] serialNos);
        void SaveTransfers(IEnumerable<TransferVM> transfers);
        void DeleteTransfers(TransferVM transfer);
    }
}
