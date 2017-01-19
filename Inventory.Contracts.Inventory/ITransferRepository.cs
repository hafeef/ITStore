using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Contracts.Inventory
{
    public interface ITransferRepository
    {
        List<TransferVM> SearchTransfers(int itemID);
        List<TransferVM> SearchTransfers(int itemID, string[] serialNos);
        void SaveTransfers(IEnumerable<TransferVM> transfers);
        void DeleteTransfers(IEnumerable<TransferVM> transfers);
    }
}
