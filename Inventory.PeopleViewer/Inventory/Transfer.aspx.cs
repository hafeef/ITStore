using Inventory.Contracts.Inventory;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class Transfer : System.Web.UI.Page
    {
        IAdministrationRepository _AdministrationRepository = new AdministrationRepository();


        List<WareHouseVM> _Warehouses = null;
        List<RackVM> _Racks = null;
        List<ShelfVM> _Shelves = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRacks();
                BindWarehouses();
                BindShelves();
            }
        }

        private void BindRacks()
        {
            _Racks = _AdministrationRepository.GetAllRacks();
            ddlToRacks.DataSource = ddlFromRacks.DataSource = _Racks;
            ddlToRacks.DataBind();
            ddlFromRacks.DataBind();
            ddlFromRacks.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlToRacks.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private void BindShelves()
        {
            _Shelves = _AdministrationRepository.GetAllShelves();
            ddlFromshelves.DataSource = ddlToshelves.DataSource = _Shelves;
            ddlToshelves.DataBind();
            ddlFromshelves.DataBind();
            ddlFromshelves.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlToshelves.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private void BindWarehouses()
        {
            _Warehouses = _AdministrationRepository.GetAllWarehouses();
            ddlFromWarehouses.DataSource = ddlToWarehouses.DataSource = _Warehouses;
            ddlToWarehouses.DataBind();
            ddlFromWarehouses.DataBind();
            ddlFromWarehouses.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlToWarehouses.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {

        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            try
            {
                txtTransferredDate.Text = hiddenFieldItemID.Value = txtSerialNo.Text = txtItemDescription.Text = string.Empty;
                ddlFromRacks.ClearSelection();
                ddlToRacks.ClearSelection();
                ddlFromshelves.ClearSelection();
                ddlToshelves.ClearSelection();
                ddlFromWarehouses.ClearSelection();
                ddlToWarehouses.ClearSelection();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {

        }
    }
}