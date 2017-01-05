﻿using Inventory.Contracts.Inventory;
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
    public partial class ReceivingOrder : Page
    {
        IPurchaseOrderRepository _purchaseOrderRepository = new PurchaseOrderRepository(new InventoryContext());

        List<LocationVM> _Locations = null;
        List<VendorVM> _vendors = null;
        List<WareHouseVM> _Warehouses = null;
        List<RackVM> _Racks = null;
        List<ShelfVM> _Shelves = null;
        List<PurchaseOrderLineItemVM> _PurchaseOrderLineItems = new List<PurchaseOrderLineItemVM>();
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
            if (_PurchaseOrderLineItems.Count == 0)
            {
                _PurchaseOrderLineItems = new List<PurchaseOrderLineItemVM>
                {
                    new PurchaseOrderLineItemVM()
                };
                ViewState[ViewStateKeys.IsEmpty] = true;
            }
            else
                ViewState[ViewStateKeys.IsEmpty] = false;

            gridLineItems.DataSource = _PurchaseOrderLineItems.Where(li => li.IsActive == true).ToList();
            gridLineItems.DataBind();
        }

        private void GetLineItemsFromViewState()
        {
            if (ViewState[ViewStateKeys.OrderLineItems] != null)
                _PurchaseOrderLineItems = ViewState[ViewStateKeys.OrderLineItems] as List<PurchaseOrderLineItemVM>;
        }

        private void PutLineItemsBackToViewState()
        {
            ViewState[ViewStateKeys.OrderLineItems] = _PurchaseOrderLineItems;
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
                    _PurchaseOrderLineItems.Clear();
                    _PurchaseOrderLineItems = _PurchaseOrder.PurchaseOrderLineItems;
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
            try
            {
                foreach (GridViewRow row in gridLineItems.Rows)
                {
                    FindPurchaseOrderLineItemsControls(row);
                    ValidateReceivingLinteItems(row);
                    //string[] serialNos = _TextBoxSerialNo.Text.Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    //if (serialNos.Length != int.Parse(_TextBoxReceivingQuantity.Text))
                    //{

                    //}
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

        private void ValidateReceivingLinteItems(GridViewRow row)
        {
            var isValid = false;

            if (string.IsNullOrWhiteSpace(_TextBoxExpiryDate.Text) && string.IsNullOrWhiteSpace(_TextBoxReceivedDate.Text) &&
                string.IsNullOrWhiteSpace(_TextBoxReceivingQuantity.Text) && string.IsNullOrWhiteSpace(_TextBoxSerialNo.Text) &&
                string.IsNullOrWhiteSpace(_TextBoxWarrantyDate.Text) && _DropDownRacks.SelectedIndex == 0 &&
                _DropDownShelves.SelectedIndex == 0 && _DropDownWarehouses.SelectedIndex == 0)
            {
                isValid = true;
            }
            else if (!string.IsNullOrWhiteSpace(_TextBoxExpiryDate.Text) && !string.IsNullOrWhiteSpace(_TextBoxReceivedDate.Text) &&
                !string.IsNullOrWhiteSpace(_TextBoxReceivingQuantity.Text) && !string.IsNullOrWhiteSpace(_TextBoxSerialNo.Text) &&
                !string.IsNullOrWhiteSpace(_TextBoxWarrantyDate.Text) && _DropDownRacks.SelectedIndex > 0 &&
                _DropDownShelves.SelectedIndex > 0 && _DropDownWarehouses.SelectedIndex > 0)
            {
                isValid = true;
            }

            else
                isValid = false;

            if (!isValid)
            {
                row.CssClass = "danger";
                throw new ApplicationException("Please provide all details in order to receive an item.");
            }


        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            try
            {
                txtPoCreatedDate.Text = txtPoOrContractNumber.Text = string.Empty;
                ddlPOType.ClearSelection();
                ddlVendors.ClearSelection();
                _PurchaseOrderLineItems.Clear();
                gridLineItems.Visible = false;
                foreach (GridViewRow row in gridLineItems.Rows)
                {
                    FindPurchaseOrderLineItemsControls(row);
                    ClearPurchaseOrderLineItemsControls();
                }
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
    }
}