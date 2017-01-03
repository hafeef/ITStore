using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.Repositories.Inventory;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;

namespace Inventory.PeopleViewer.Services
{
    /// <summary>
    /// Summary description for AutoCompleteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteService : System.Web.Services.WebService
    {
        IPurchaseOrderRepository _purchaseOrderRepository = new PurchaseOrderRepository(new InventoryContext());

        [WebMethod, ScriptMethod]
        public List<string> GetItemDescription(string prefixText, int count)
        {
            var items = _purchaseOrderRepository.GetItemByDescription(prefixText);
            var resutls = new List<string>();
            foreach (var item in items)
            {
                string resutl = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.Description, item.ItemID.ToString());
                resutls.Add(resutl);
            }
            return resutls;
        }
    }
}
