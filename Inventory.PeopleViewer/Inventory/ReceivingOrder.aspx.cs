using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventory.ViewModels.Inventory;
using Core.Common.BaseTypes;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class ReceivingOrder : System.Web.UI.Page
    {
        IPurchaseOrderRepository _purchaseOrderRepository = new PurchaseOrderRepository(new InventoryContext());

        List<LocationVM> _Locations = null;
        List<VendorVM> _vendors = null;
        List<WareHouseVM> _Warehouses = null;
        List<RackVM> _Racks = null;
        List<ShelfVM> _Shelves = null;
        List<PurchaseOrderLineItemVM> _LineItems = new List<PurchaseOrderLineItemVM>();
        PurchaseOrderVM _PurchaseOrder = null;


        DropDownList _DropDownLocations = null;
        DropDownList _DropDownRacks = null;
        DropDownList _DropDownShelves = null;
        DropDownList _DropDownWarehouses = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _vendors = _purchaseOrderRepository.GetAllVendors();
                BindDropDownList(ddlVendors, _vendors);
            }
        }

        private void BindDropDownList(DropDownList dropDownList, IEnumerable<EntityBaseVM> dataSource)
        {
            try
            {
                dropDownList.DataSource = dataSource;
                dropDownList.DataBind();
                dropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void GetLocations()
        {
            _Locations = _purchaseOrderRepository.GetAllLocations();
        }

        private void GetRacks()
        {
            _Racks = _purchaseOrderRepository.GetAllRacks();
        }

        private void GetShelves()
        {
            _Shelves = _purchaseOrderRepository.GetAllShelves();
        }

        private void GetWarehouses()
        {
            _Warehouses = _purchaseOrderRepository.GetAllWarehouses();
        }

        private void BindLineItems()
        {
            GetLineItemsFromViewState();
            if (_LineItems.Count == 0)
            {
                _LineItems = new List<PurchaseOrderLineItemVM>
                {
                    new PurchaseOrderLineItemVM()
                };
                ViewState[ViewStateKeys.IsEmpty] = true;
            }
            else
                ViewState[ViewStateKeys.IsEmpty] = false;

            gridLineItems.DataSource = _LineItems.Where(li => li.IsActive == true).ToList();
            gridLineItems.DataBind();
        }

        private void GetLineItemsFromViewState()
        {
            if (ViewState[ViewStateKeys.OrderLineItems] != null)
                _LineItems = ViewState[ViewStateKeys.OrderLineItems] as List<PurchaseOrderLineItemVM>;
        }

        private void PutLineItemsBackToViewState()
        {
            ViewState[ViewStateKeys.OrderLineItems] = _LineItems;
        }

        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPoOrContractNumber.Text.Trim()))
                    throw new ApplicationException("The po or contract no is required.");

                _PurchaseOrder = _purchaseOrderRepository.FindPurchaseOrderByPoOrContractNumber(txtPoOrContractNumber.Text.Trim());
                if (_PurchaseOrder != null)
                {
                    hiddenFieldPurchaseOrderID.Value = _PurchaseOrder.PurchaseOrderID.ToString();
                    ddlVendors.SelectedValue = _PurchaseOrder.VendorID.ToString();
                    ddlPOType.SelectedValue = _PurchaseOrder.POTypeValue.ToString();
                    gridLineItems.Visible = true;
                    _LineItems.Clear();
                    _LineItems = _PurchaseOrder.PurchaseOrderLineItems;
                    PutLineItemsBackToViewState();
                    txtPoCreatedDate.Text = _PurchaseOrder.POCreatedDate.ToString("yyyy-MM-dd");
                    //GetLocations();
                    GetRacks();
                    GetShelves();
                    GetWarehouses();
                    BindLineItems();
                }
            }
            catch (ApplicationException Ae)
            {
                ucInformation.ShowErrorMessage(Ae.Message);
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void linkButtonCreate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridLineItems.Rows)
            {
                var textbox = row.FindControl("txtSerialNos") as TextBox;
                string[] serials = textbox.Text.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {

        }

        protected void gridLineItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FindDropDownLists(e);
                BindAllDropDownLists();
            }
        }

        private void BindAllDropDownLists()
        {
            //BindDropDownList(_DropDownLocations, _Locations);
            BindDropDownList(_DropDownWarehouses, _Warehouses);
            BindDropDownList(_DropDownRacks, _Racks);
            BindDropDownList(_DropDownShelves, _Shelves);
        }

        private void FindDropDownLists(GridViewRowEventArgs e)
        {
            //_DropDownLocations = e.Row.FindControl("ddlLocations") as DropDownList;
            _DropDownRacks = e.Row.FindControl("ddlRacks") as DropDownList;
            _DropDownShelves = e.Row.FindControl("ddlShelves") as DropDownList;
            _DropDownWarehouses = e.Row.FindControl("ddlWarehouses") as DropDownList;
        }
    }
}