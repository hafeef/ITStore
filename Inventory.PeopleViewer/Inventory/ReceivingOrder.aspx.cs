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
using Core.Common.Enums;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class ReceivingOrder : Page
    {
        IPurchaseOrderRepository _purchaseOrderRepository = new PurchaseOrderRepository(new InventoryContext());
        IAdministrationRepository _administrationRepository = new AdministrationRepository();
        List<LocationVM> _Locations = null;
        List<VendorVM> _vendors = null;
        List<WareHouseVM> _Warehouses = null;
        List<RackVM> _Racks = null;
        List<ShelfVM> _Shelves = null;
        PurchaseOrderLineItemVM _POLineItem = null;
        List<ReceivedLineItemVM> _ReceivedLineItems = new List<ReceivedLineItemVM>();
        PurchaseOrderVM _PurchaseOrder = null;

        DropDownList _DropDownLocations = null;
        DropDownList _DropDownRacks = null;
        DropDownList _DropDownShelves = null;
        DropDownList _DropDownWarehouses = null;
        TextBox _TextBoxSerialNo = null;
        TextBox _TextBoxReceivingQuantity = null;
        TextBox _TextBoxReceivedDate = null;
        TextBox _TextBoxWarrantyDate = null;
        TextBox _TextBoxExpiryDate = null;
        private ReceivedLineItemVM _ReceivedLineItem;

        private bool IsEmptyGridRow { get; set; } = false;
        private bool IsValidGridRow { get; set; } = false;
        private string[] SerialNumbers { get; set; } = null;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _vendors = _administrationRepository.GetAllVendors();
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
            _Locations = _administrationRepository.GetAllLocations();
        }

        private void GetRacks()
        {
            _Racks = _administrationRepository.GetAllRacks();
        }

        private void GetShelves()
        {
            _Shelves = _administrationRepository.GetAllShelves();
        }

        private void GetWarehouses()
        {
            _Warehouses = _administrationRepository.GetAllWarehouses();
        }

        private void BindPurchasedLineItems()
        {
            GetPurchaseOrderFromViewState();
            gridLineItems.DataSource = _PurchaseOrder.PurchaseOrderLineItems.Where(poli => poli.PurchasedQuantity > poli.ReceivedQuantity).ToList();
            gridLineItems.DataBind();
        }

        private void GetPurchaseOrderFromViewState()
        {
            if (ViewState[ViewStateKeys.PurchaseOrder] != null)
                _PurchaseOrder = ViewState[ViewStateKeys.PurchaseOrder] as PurchaseOrderVM;
        }

        private void PutPurchaseOrderBackToViewState()
        {
            ViewState[ViewStateKeys.PurchaseOrder] = _PurchaseOrder;
        }

        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPoOrContractNumber.Text.Trim()))
                    throw new ApplicationException("The po or contract no is required.");

                _PurchaseOrder = _purchaseOrderRepository.FindPurchaseOrderIncludeReceivedItemsByPoOrContractNumber(txtPoOrContractNumber.Text.Trim());
                if (_PurchaseOrder != null)
                {
                    ddlVendors.SelectedValue = _PurchaseOrder.VendorID.ToString();
                    ddlPOType.SelectedValue = _PurchaseOrder.POTypeValue.ToString();
                    txtPoCreatedDate.Text = _PurchaseOrder.POCreatedDate.ToString("yyyy-MM-dd");
                    GetRacks();
                    GetShelves();
                    GetWarehouses();
                    PutPurchaseOrderBackToViewState();
                    if (_PurchaseOrder.ReceivedLineItems.Count > 0)
                    {
                        _PurchaseOrder.ReceivedLineItems.Join(_Warehouses, rli => rli.WarehouseID, wh => wh.WareHouseID, (rli, w) => { rli.WarehouseName = w.Name; return rli; }).ToList();
                        _PurchaseOrder.ReceivedLineItems.Join(_Shelves, rli => rli.ShelfID, sh => sh.ShelfID, (rli, sh) => { rli.ShelfName = sh.Name; return rli; }).ToList();
                        _PurchaseOrder.ReceivedLineItems.Join(_Racks, rli => rli.RackID, r => r.RackID, (rli, r) => { rli.RackName = r.Name; return rli; }).ToList();
                        GridViewReceivedItems.Visible = true;
                        divReceivedItems.Visible = true;
                        PutPurchaseOrderBackToViewState();
                        BindReceivedItems();
                    }
                    if (_PurchaseOrder.PurchaseOrderLineItems.Count > 0)
                    {
                        gridLineItems.Visible = true;
                        BindPurchasedLineItems();
                    }
                }
                else
                {
                    ucInformation.ShowErrorMessage("There is no purchase ordere in the database with this PO/Contract number.");
                    linkButtonReset_Click(sender, e);
                }
            }
            catch (ApplicationException Ae)
            {
                ucInformation.ShowErrorMessage(Ae.Message);
            }
            catch (Exception Ex)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindReceivedItems()
        {
            GetPurchaseOrderFromViewState();
            GridViewReceivedItems.DataSource = _PurchaseOrder.ReceivedLineItems.Where(rli => rli.EntityState != ObjectState.Deleted).ToList();
            GridViewReceivedItems.DataBind();
        }

        protected void linkButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                GetPurchaseOrderFromViewState();
                foreach (GridViewRow row in gridLineItems.Rows)
                {
                    SetRowCssClassToEmpty(row);
                    FindPurchaseOrderLineItemsControls(row);
                    ValidateReceivingLinteItems(row);
                    if (IsValidGridRow && !IsEmptyGridRow)
                        CreateReceivedLineItems(row);
                }
                _purchaseOrderRepository.UpdatePurchaseOrder(_PurchaseOrder);
                ucInformation.ShowSaveInfomationMessage();
                ClearFormData();
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

        private void CreateReceivedLineItems(GridViewRow row)
        {
            var itemID = int.Parse(gridLineItems.DataKeys[row.RowIndex]["ItemID"].ToString());
            var purchaseOrderID = int.Parse(gridLineItems.DataKeys[row.RowIndex]["PurchaseOrderID"].ToString());
            var purchaseOrderLineItemID = int.Parse(gridLineItems.DataKeys[row.RowIndex]["PurchaseOrderLineItemID"].ToString());
            var recevideLineItems = SerialNumbers.Select(srNo => new ReceivedLineItemVM
            {
                EntityState = ObjectState.Added,
                ItemID = itemID,
                PurchaseOrderID = purchaseOrderID,
                PurchaseOrderLineItemID = purchaseOrderLineItemID,
                SerialNo = srNo,
                ReceivedDate = Convert.ToDateTime(_TextBoxReceivedDate.Text),
                RackID = int.Parse(_DropDownRacks.SelectedValue),
                WarehouseID = int.Parse(_DropDownWarehouses.SelectedValue),
                ShelfID = int.Parse(_DropDownShelves.SelectedValue),
                ExpiryDate = Convert.ToDateTime(_TextBoxExpiryDate.Text),
                WarrantyDate = Convert.ToDateTime(_TextBoxWarrantyDate.Text),
            });
            _POLineItem = _PurchaseOrder.PurchaseOrderLineItems.Find(poli => poli.PurchaseOrderLineItemID == purchaseOrderLineItemID);
            _POLineItem.ReceivedQuantity += int.Parse(_TextBoxReceivingQuantity.Text);
            _PurchaseOrder.ReceivedLineItems.AddRange(recevideLineItems);
            _PurchaseOrder.ReceivedTotal = _PurchaseOrder.PurchaseOrderLineItems.Sum(poli => poli.ReceivedQuantity * poli.Price);
            _POLineItem.EntityState = ObjectState.Modified;
            _PurchaseOrder.EntityState = ObjectState.Modified;
        }

        private void ValidateReceivingLinteItems(GridViewRow row)
        {


            if (string.IsNullOrWhiteSpace(_TextBoxExpiryDate.Text) && string.IsNullOrWhiteSpace(_TextBoxReceivedDate.Text) &&
                string.IsNullOrWhiteSpace(_TextBoxReceivingQuantity.Text) && string.IsNullOrWhiteSpace(_TextBoxSerialNo.Text) &&
                string.IsNullOrWhiteSpace(_TextBoxWarrantyDate.Text) && _DropDownRacks.SelectedIndex == 0 &&
                _DropDownShelves.SelectedIndex == 0 && _DropDownWarehouses.SelectedIndex == 0)
            {
                IsEmptyGridRow = IsValidGridRow = true;
            }
            else if (!string.IsNullOrWhiteSpace(_TextBoxExpiryDate.Text) && !string.IsNullOrWhiteSpace(_TextBoxReceivedDate.Text) &&
                !string.IsNullOrWhiteSpace(_TextBoxReceivingQuantity.Text) && !string.IsNullOrWhiteSpace(_TextBoxSerialNo.Text) &&
                !string.IsNullOrWhiteSpace(_TextBoxWarrantyDate.Text) && _DropDownRacks.SelectedIndex > 0 &&
                _DropDownShelves.SelectedIndex > 0 && _DropDownWarehouses.SelectedIndex > 0)
            {
                SerialNumbers = _TextBoxSerialNo.Text.Trim().Split(Environment.NewLine.ToCharArray());
                if (SerialNumbers.Length > int.Parse(_TextBoxReceivingQuantity.Text) || SerialNumbers.Length < int.Parse(_TextBoxReceivingQuantity.Text))
                {
                    SetRowCssClassToDanger(row);
                    throw new ApplicationException("Serial numbers should be equal to the receiving quantity of an item.");
                }
                IsValidGridRow = true;
                IsEmptyGridRow = false;
            }
            else
                IsValidGridRow = false;

            if (!IsValidGridRow)
            {
                SetRowCssClassToDanger(row);
                throw new ApplicationException("Please provide all details in order to receive an item.");
            }
        }

        private static void SetRowCssClassToDanger(GridViewRow row)
        {
            row.CssClass = "danger";
        }
        private static void SetRowCssClassToEmpty(GridViewRow row)
        {
            row.CssClass = string.Empty;
        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            try
            {
                GetPurchaseOrderFromViewState();
                txtPoCreatedDate.Text = txtPoOrContractNumber.Text = string.Empty;
                ddlPOType.ClearSelection();
                ddlVendors.ClearSelection();
                gridLineItems.Visible = false;
                GridViewReceivedItems.Visible = false;
                GridViewReceivedItems.PageIndex = 0;
                divReceivedItems.Visible = false;
                _PurchaseOrder.ReceivedLineItems.Clear();
                _PurchaseOrder.PurchaseOrderLineItems.Clear();
                _PurchaseOrder = null;
                PutPurchaseOrderBackToViewState();
                ViewState.Clear();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void ClearPurchaseOrderLineItemsControls()
        {
            _TextBoxWarrantyDate.Text = _TextBoxSerialNo.Text =
                _TextBoxReceivingQuantity.Text = _TextBoxReceivedDate.Text = _TextBoxExpiryDate.Text = string.Empty;
            _DropDownWarehouses.ClearSelection();
            _DropDownShelves.ClearSelection();
            _DropDownRacks.ClearSelection();
        }

        private void FindPurchaseOrderLineItemsControls(GridViewRow row)
        {
            _TextBoxSerialNo = row.FindControl("txtSerialNos") as TextBox;
            _TextBoxExpiryDate = row.FindControl("txtExpiry") as TextBox;
            _TextBoxReceivedDate = row.FindControl("txtReceivedDate") as TextBox;
            _TextBoxWarrantyDate = row.FindControl("txtWarrantyYear") as TextBox;
            _TextBoxReceivingQuantity = row.FindControl("txtQuantity") as TextBox;
            _DropDownRacks = row.FindControl("ddlRacks") as DropDownList;
            _DropDownShelves = row.FindControl("ddlShelves") as DropDownList;
            _DropDownWarehouses = row.FindControl("ddlWarehouses") as DropDownList;
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

        protected void GridViewReceivedItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridViewReceivedItemsEditRowIndexToMinusOne();
                BindReceivedItems();
            }
            catch (Exception Ex)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        void SetGridViewReceivedItemsEditRowIndexToMinusOne()
        {
            GridViewReceivedItems.EditIndex = -1;
        }

        protected void GridViewReceivedItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GetPurchaseOrderFromViewState();
                var receivedLineItemdID = int.Parse(e.Keys["ReceivedLineItemID"].ToString());
                var purchaseOrderLineItemdID = int.Parse(e.Keys["PurchaseOrderLineItemID"].ToString());
                _POLineItem = _PurchaseOrder.PurchaseOrderLineItems.Find(li => li.PurchaseOrderLineItemID == purchaseOrderLineItemdID);
                _POLineItem.EntityState = ObjectState.Modified;
                _POLineItem.ReceivedQuantity--;
                _PurchaseOrder.ReceivedLineItems.Find(rli => rli.ReceivedLineItemID == receivedLineItemdID).EntityState = ObjectState.Deleted;
                PutPurchaseOrderBackToViewState();
                BindReceivedItems();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridViewReceivedItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewReceivedItems.EditIndex = e.NewEditIndex;
            BindReceivedItems();
        }

        protected void GridViewReceivedItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var receivedLineItem = int.Parse(e.Keys["ReceivedLineItemID"].ToString());
                GetPurchaseOrderFromViewState();
                _ReceivedLineItem = _PurchaseOrder.ReceivedLineItems.Find(rli => rli.ReceivedLineItemID == receivedLineItem);
                FindReceivedLineItemControls(e);
                ValidateReceivedLineItemControls();
                UpdateReceivedLineItem();
                PutPurchaseOrderBackToViewState();
                SetGridViewReceivedItemsEditRowIndexToMinusOne();
                BindReceivedItems();
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



        private void UpdateReceivedLineItem()
        {
            _ReceivedLineItem.EntityState = ObjectState.Modified;
            _ReceivedLineItem.SerialNo = _TextBoxSerialNo.Text.Trim();
            _ReceivedLineItem.ExpiryDate = Convert.ToDateTime(_TextBoxExpiryDate.Text);
            _ReceivedLineItem.WarehouseID = int.Parse(_DropDownWarehouses.SelectedValue);
            _ReceivedLineItem.WarehouseName = _DropDownWarehouses.SelectedItem.Text;
            _ReceivedLineItem.WarrantyDate = Convert.ToDateTime(_TextBoxWarrantyDate.Text);
            _ReceivedLineItem.ReceivedDate = Convert.ToDateTime(_TextBoxReceivedDate.Text);
            _ReceivedLineItem.RackID = int.Parse(_DropDownRacks.SelectedValue);
            _ReceivedLineItem.RackName = _DropDownRacks.SelectedItem.Text;
            _ReceivedLineItem.ShelfID = int.Parse(_DropDownShelves.SelectedValue);
            _ReceivedLineItem.ShelfName = _DropDownShelves.SelectedItem.Text;
        }

        private void ValidateReceivedLineItemControls()
        {
            if (_TextBoxExpiryDate == null && string.IsNullOrWhiteSpace(_TextBoxExpiryDate.Text))
                throw new ApplicationException("The expiry date field is required.");
            if (_TextBoxReceivedDate == null && string.IsNullOrWhiteSpace(_TextBoxReceivedDate.Text))
                throw new ApplicationException("The received date field is required.");
            if (_TextBoxWarrantyDate == null && string.IsNullOrWhiteSpace(_TextBoxWarrantyDate.Text))
                throw new ApplicationException("The warranty date field is required.");
            if (_TextBoxSerialNo == null && string.IsNullOrWhiteSpace(_TextBoxSerialNo.Text))
                throw new ApplicationException("The serial no field is required.");
            if (_DropDownRacks == null && _DropDownRacks.SelectedIndex == 0)
                throw new ApplicationException("The rack field is required.");
            if (_DropDownShelves == null && _DropDownShelves.SelectedIndex == 0)
                throw new ApplicationException("The shelf field is required.");
            if (_DropDownWarehouses == null && _DropDownWarehouses.SelectedIndex == 0)
                throw new ApplicationException("The warehouse field is required.");

        }

        private void FindReceivedLineItemControls(GridViewUpdateEventArgs e)
        {
            var row = GridViewReceivedItems.Rows[e.RowIndex];
            _TextBoxSerialNo = row.FindControl("txtSerialNos") as TextBox;
            _TextBoxReceivedDate = row.FindControl("txtReceivedDate") as TextBox;
            _TextBoxWarrantyDate = row.FindControl("txtWarrantyYear") as TextBox;
            _TextBoxExpiryDate = row.FindControl("txtExpiry") as TextBox;
            _DropDownRacks = row.FindControl("ddlRacks") as DropDownList;
            _DropDownShelves = row.FindControl("ddlShelves") as DropDownList;
            _DropDownWarehouses = row.FindControl("ddlWarehouses") as DropDownList;
        }

        protected void GridViewReceivedItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && GridViewReceivedItems.EditIndex == e.Row.RowIndex)
                {
                    GetRacks();
                    GetWarehouses();
                    GetShelves();

                    var rackID = GridViewReceivedItems.DataKeys[e.Row.RowIndex].Values["RackID"].ToString();
                    var warehouseID = GridViewReceivedItems.DataKeys[e.Row.RowIndex].Values["WarehouseID"].ToString();
                    var shelfID = GridViewReceivedItems.DataKeys[e.Row.RowIndex].Values["ShelfID"].ToString();

                    BindDropDownLists(e, "ddlRacks", rackID, _Racks);
                    BindDropDownLists(e, "ddlWarehouses", warehouseID, _Warehouses);
                    BindDropDownLists(e, "ddlShelves", shelfID, _Shelves);
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindDropDownLists(GridViewRowEventArgs e, string dropDownID, string selectedValue, IEnumerable<EntityBaseVM> dropDownSource)
        {
            try
            {
                var dropDownList = e.Row.FindControl(dropDownID) as DropDownList;
                dropDownList.DataSource = dropDownSource;
                dropDownList.DataBind();
                dropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
                if (!string.IsNullOrWhiteSpace(selectedValue))
                    dropDownList.SelectedValue = selectedValue;
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridViewReceivedItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SetGridViewReceivedItemsEditRowIndexToMinusOne();
                GridViewReceivedItems.PageIndex = e.NewPageIndex;
                BindReceivedItems();
            }
            catch (Exception Ex)
            {
                ucInformation.ShowErrorMessage();
            }
        }
    }
}