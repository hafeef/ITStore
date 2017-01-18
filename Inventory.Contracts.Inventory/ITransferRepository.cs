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
        IList<TransferVM> SearchTransfers(int itemID);
        IList<TransferVM> SearchTransfers(int itemID, string[] serialNos);
        void SaveTransfers(IEnumerable<TransferVM> transfers);
        void DeleteTransfers(IEnumerable<TransferVM> transfers);
    }
}
