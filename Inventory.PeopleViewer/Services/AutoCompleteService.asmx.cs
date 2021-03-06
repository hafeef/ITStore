﻿using Inventory.Contracts.Inventory;
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
        IAdministrationRepository _administrationRepository = new AdministrationRepository();
        [WebMethod, ScriptMethod]
        public List<string> GetItemDescription(string prefixText, int count)
        {
            var items = _administrationRepository.GetItemByDescription(prefixText);
            var resutls = new List<string>();
            foreach (var item in items)
            {
                string resutl = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.Description, item.ItemID.ToString());
                resutls.Add(resutl);
            }
            return resutls;
        }

        [WebMethod, ScriptMethod]
        public List<string> FindEmployeeByCivilID(string prefixText, int count)
        {
            var employees = _administrationRepository.FindEmployeeByCivilID(prefixText);
            var resutls = new List<string>();
            foreach (var emp in employees)
            {
                string resutl = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(emp.CivilID, $"{emp.EmployeeID},{emp.Name}");
                resutls.Add(resutl);
            }
            return resutls;
        }


    }
}
