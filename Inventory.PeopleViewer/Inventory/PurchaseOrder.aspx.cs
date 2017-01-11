using Core.Common.Enums;
using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class PurchaseOrder : Page
    {
        IPurchaseOrderRepository _purchaseOrderRepository = new PurchaseOrderRepository(new InventoryContext());


        TextBox txtItemDescription = null;
        TextBox txtQuantity = null;
        TextBox txtPrice = null;

        List<VendorVM> _vendors = null;
        List<PurchaseOrderLineItemVM> _LineItems = new List<PurchaseOrderLineItemVM>();
        PurchaseOrderVM _PurchaseOrder = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    _vendors = _purchaseOrderRepository.GetAllVendors();
                    BindDropDownList(ddlVendors, _vendors);
                    BindLineItems();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindLineItems()
        {
            GetPurchaseOrderFromViewState();

            if (!IsPostBack)
            {
                _PurchaseOrder = new PurchaseOrderVM();
                _PurchaseOrder.PurchaseOrderLineItems = new List<PurchaseOrderLineItemVM>
                {
                    new PurchaseOrderLineItemVM()
                };
                AreLineItemsEmpty(true);
            }
            gridLineItems.DataSource = _PurchaseOrder.PurchaseOrderLineItems.Where(li => li.EntityState != ObjectState.Deleted).ToList();
            gridLineItems.DataBind();

            if (!IsPostBack || Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
            {
                ClearPurchaseOrderLineItems();
                PutPurchaseOrderBackToViewState();
            }
        }

        private void AreLineItemsEmpty(bool value)
        {
            ViewState[ViewStateKeys.IsEmpty] = value;
        }

        private void ClearPurchaseOrderLineItems()
        {
            _PurchaseOrder.PurchaseOrderLineItems.Clear();
        }

        private void GetPurchaseOrderFromViewState()
        {
            if (ViewState[ViewStateKeys.PurchaseOrder] != null)
                _PurchaseOrder = ViewState[ViewStateKeys.PurchaseOrder] as PurchaseOrderVM;
        }

        private void SetFooterData()
        {
            try
            {
                if (gridLineItems.FooterRow != null)
                {
                    txtQuantity = gridLineItems.FooterRow.FindControl("txtQuantity") as TextBox;
                    txtPrice = gridLineItems.FooterRow.FindControl("txtPrice") as TextBox;
                    txtItemDescription = gridLineItems.FooterRow.FindControl("txtItemDescription") as TextBox;
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindDropDownList(DropDownList dropDownList, IEnumerable<VendorVM> dataSource)
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

        protected void gridLineItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (!Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                    e.Row.Cells[3].Text = string.Format("<b>{0:0.000}</b>", _PurchaseOrder.PurchaseOrderLineItems.Where(li => li.EntityState != ObjectState.Deleted).Sum(li => li.Total));
            }
        }

        protected void linkButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SetFooterData();
                ValidateLineItem();
                GetPurchaseOrderFromViewState();
                _PurchaseOrder.PurchaseOrderLineItems.Add(CreateLineItem(_PurchaseOrder.PurchaseOrderLineItems.Count + 1));
                PutPurchaseOrderBackToViewState();
                AreLineItemsEmpty(false);
                BindLineItems();
                SetHiddenFieldValuesToEmpty();

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

        private void SetHiddenFieldValuesToEmpty()
        {
            hiddenFieldItemID.Value = string.Empty;
            hiddenFieldLineItemID.Value = string.Empty;
        }

        private void PutPurchaseOrderBackToViewState()
        {
            ViewState[ViewStateKeys.PurchaseOrder] = _PurchaseOrder;
        }

        private PurchaseOrderLineItemVM CreateLineItem(int lineItemID)
        {
            var lineItem = new PurchaseOrderLineItemVM();
            if (!string.IsNullOrWhiteSpace(hiddenFieldPurchaseOrderID.Value))
            {
                lineItem.PurchaseOrderLineItemID = int.Parse(hiddenFieldLineItemID.Value == string.Empty ? "0" : hiddenFieldLineItemID.Value);
                lineItem.PurchaseOrderID = int.Parse(hiddenFieldPurchaseOrderID.Value);
            }
            lineItem.SrNo = lineItemID;
            lineItem.ItemID = int.Parse(hiddenFieldItemID.Value);
            lineItem.PurchasedQuantity = int.Parse(txtQuantity.Text.Trim());
            lineItem.Price = Convert.ToDouble(txtPrice.Text.Trim());
            lineItem.Total = int.Parse(txtQuantity.Text.Trim()) * Convert.ToDouble(txtPrice.Text.Trim());
            lineItem.ItemDescription = txtItemDescription.Text.Trim();
            lineItem.EntityState = hiddenFieldLineItemID.Value == string.Empty ? ObjectState.Added : ObjectState.Modified;
            return lineItem;
        }

        private void ValidateLineItem()
        {
            if (txtPrice == null || string.IsNullOrWhiteSpace(txtPrice.Text))
                throw new ApplicationException("The price field is required.");
            if (txtQuantity == null || string.IsNullOrWhiteSpace(txtQuantity.Text))
                throw new ApplicationException("The quantity field is required.");
            if (txtItemDescription == null || string.IsNullOrWhiteSpace(txtItemDescription.Text))
                throw new ApplicationException("The item field is required.");
        }

        private void ValidatePurchaseOrder()
        {
            if (ddlVendors.SelectedIndex == 0)
                throw new ApplicationException("The vendor field is required.");
            if (ddlPOType.SelectedIndex == 0)
                throw new ApplicationException("The PO type field is required.");
            if (string.IsNullOrWhiteSpace(txtPoOrContractNumber.Text))
                throw new ApplicationException("The po or contract no field is required.");
        }

        protected void gridLineItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                hiddenFieldItemID.Value = gridLineItems.DataKeys[e.NewEditIndex]["ItemID"].ToString();
                hiddenFieldLineItemID.Value = gridLineItems.DataKeys[e.NewEditIndex]["PurchaseOrderLineItemID"].ToString();
                gridLineItems.EditIndex = e.NewEditIndex;
                BindLineItems();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridLineItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridRowIndexToMinusOne();
                SetHiddenFieldValuesToEmpty();
                BindLineItems();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetGridRowIndexToMinusOne()
        {
            gridLineItems.EditIndex = -1;
        }

        protected void gridLineItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GetPurchaseOrderFromViewState();
                SetEditData(e);
                ValidateLineItem();
                int lineItemID = default(int);
                int lineItemIndex = default(int);
                var lineItem = default(PurchaseOrderLineItemVM);
                if (string.IsNullOrWhiteSpace(hiddenFieldPurchaseOrderID.Value))
                {
                    lineItemID = int.Parse(e.Keys["SrNo"].ToString());
                    lineItem = _PurchaseOrder.PurchaseOrderLineItems.Find(li => li.SrNo == lineItemID);
                    lineItemIndex = _PurchaseOrder.PurchaseOrderLineItems.FindIndex(li => li.SrNo == lineItemID);
                }
                else
                {
                    if (int.Parse(hiddenFieldLineItemID.Value) > 0)
                    {
                        lineItemID = int.Parse(e.Keys["PurchaseOrderLineItemID"].ToString());
                        lineItem = _PurchaseOrder.PurchaseOrderLineItems.Find(li => li.PurchaseOrderLineItemID == lineItemID);
                        lineItemIndex = _PurchaseOrder.PurchaseOrderLineItems.FindIndex(li => li.PurchaseOrderLineItemID == lineItemID);
                    }
                    else
                    {
                        lineItemID = int.Parse(e.Keys["SrNo"].ToString());
                        lineItem = _PurchaseOrder.PurchaseOrderLineItems.FirstOrDefault(li => li.SrNo == lineItemID);
                        lineItemIndex = _PurchaseOrder.PurchaseOrderLineItems.FindIndex(li => li.SrNo == lineItemID);
                    }
                }

                _PurchaseOrder.PurchaseOrderLineItems.Remove(lineItem);
                _PurchaseOrder.PurchaseOrderLineItems.Insert(lineItemIndex, CreateLineItem(lineItemID));

                PutPurchaseOrderBackToViewState();
                SetGridRowIndexToMinusOne();
                SetHiddenFieldValuesToEmpty();
                BindLineItems();
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

        private void SetEditData(GridViewUpdateEventArgs e)
        {
            txtItemDescription = gridLineItems.Rows[e.RowIndex].FindControl("txtItemDescription") as TextBox;
            txtPrice = gridLineItems.Rows[e.RowIndex].FindControl("txtPrice") as TextBox;
            txtQuantity = gridLineItems.Rows[e.RowIndex].FindControl("txtQuantity") as TextBox;
        }

        protected void gridLineItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GetPurchaseOrderFromViewState();
                int lineItemID = default(int);
                var lineItem = default(PurchaseOrderLineItemVM);
                if (string.IsNullOrWhiteSpace(hiddenFieldPurchaseOrderID.Value))
                {
                    lineItemID = int.Parse(gridLineItems.DataKeys[e.RowIndex]["SrNo"].ToString());
                    lineItem = _PurchaseOrder.PurchaseOrderLineItems.FirstOrDefault(li => li.SrNo == lineItemID);
                    _PurchaseOrder.PurchaseOrderLineItems.Remove(lineItem);
                }
                else
                {
                    lineItemID = int.Parse(e.Keys["PurchaseOrderLineItemID"].ToString());
                    if (lineItemID > 0)
                    {
                        _PurchaseOrder.PurchaseOrderLineItems.Find(li => li.PurchaseOrderLineItemID == lineItemID).EntityState = ObjectState.Deleted;
                        if (_PurchaseOrder.ReceivedLineItems.Any(rli => rli.PurchaseOrderLineItemID == lineItemID))
                        {
                            _PurchaseOrder.ReceivedLineItems.Where(rli => rli.PurchaseOrderLineItemID == lineItemID).ToList().ForEach(rli => rli.EntityState = ObjectState.Deleted);
                        }
                    }
                    else
                    {
                        lineItemID = int.Parse(e.Keys["SrNo"].ToString());
                        lineItem = _PurchaseOrder.PurchaseOrderLineItems.FirstOrDefault(li => li.SrNo == lineItemID);
                        _PurchaseOrder.PurchaseOrderLineItems.Remove(lineItem);
                    }
                }
                if (_PurchaseOrder.PurchaseOrderLineItems.Count == 0)
                {
                    AreLineItemsEmpty(true);
                    _PurchaseOrder.PurchaseOrderLineItems.Add(new PurchaseOrderLineItemVM());
                }
                PutPurchaseOrderBackToViewState();
                SetGridRowIndexToMinusOne();
                BindLineItems();
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

        protected void gridLineItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLineItems.PageIndex = e.NewPageIndex;
            SetGridRowIndexToMinusOne();
            BindLineItems();
        }

        protected void linkButtonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                {
                    ucInformation.ShowErrorMessage("Please add one or more items in order to create PO.");
                }
                else
                {
                    GetPurchaseOrderFromViewState();
                    ValidatePurchaseOrder();
                    CreatePurchaseOrder();
                    if (string.IsNullOrWhiteSpace(hiddenFieldPurchaseOrderID.Value))
                    {
                        _purchaseOrderRepository.CreatePurchaseOrder(_PurchaseOrder);
                        ucInformation.ShowSaveInfomationMessage();
                    }
                    else
                    {
                        _purchaseOrderRepository.UpdatePurchaseOrder(_PurchaseOrder);
                        ucInformation.ShowModifyInfomationMessage();
                    }
                    SetGridRowIndexToMinusOne();
                    ClearFormData();
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

        private void CreatePurchaseOrder()
        {
            if (_PurchaseOrder != null && _PurchaseOrder.PurchaseOrderLineItems.Count > 0)
            {
                _PurchaseOrder.PurchaseOrderID = int.Parse(hiddenFieldPurchaseOrderID.Value == string.Empty ? "0" : hiddenFieldPurchaseOrderID.Value);
                _PurchaseOrder.POTypeValue = int.Parse(ddlPOType.SelectedItem.Value);
                _PurchaseOrder.VendorID = int.Parse(ddlVendors.SelectedItem.Value);
                _PurchaseOrder.PoOrContractNumber = txtPoOrContractNumber.Text;
                _PurchaseOrder.POTypeText = ddlPOType.SelectedItem.Text;
                _PurchaseOrder.GrandTotal = _PurchaseOrder.PurchaseOrderLineItems.Where(li => li.EntityState != ObjectState.Deleted).Sum(li => li.Total);
                _PurchaseOrder.EntityState = hiddenFieldPurchaseOrderID.Value == string.Empty ? ObjectState.Added : ObjectState.Modified;
                _PurchaseOrder.POCreatedDate = Convert.ToDateTime(txtPoCreatedDate.Text);
            }
        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            BindLineItems();
        }

        private void ClearFormData()
        {
            ViewState[ViewStateKeys.PurchaseOrder] = null;
            ViewState[ViewStateKeys.IsEmpty] = true;
            ddlPOType.SelectedIndex = 0;
            ddlVendors.SelectedIndex = 0;
            txtPoOrContractNumber.Text = string.Empty;
            hiddenFieldPurchaseOrderID.Value = string.Empty;
            hiddenFieldItemID.Value = string.Empty;
            hiddenFieldLineItemID.Value = string.Empty;
            txtPoCreatedDate.Text = string.Empty;
            SetFooterData();
            ClearFooterData();
        }

        private void ClearFooterData()
        {
            if (txtItemDescription != null)
                txtItemDescription.Text = string.Empty;
            if (txtPrice != null)
                txtPrice.Text = string.Empty;
            if (txtQuantity != null)
                txtQuantity.Text = string.Empty;
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
                    hiddenFieldPurchaseOrderID.Value = _PurchaseOrder.PurchaseOrderID.ToString();
                    ddlVendors.SelectedValue = _PurchaseOrder.VendorID.ToString();
                    ddlPOType.SelectedValue = _PurchaseOrder.POTypeValue.ToString();
                    txtPoCreatedDate.Text = _PurchaseOrder.POCreatedDate.ToString("yyyy-MM-dd");
                    if (_PurchaseOrder.PurchaseOrderLineItems.Count > 0)
                        AreLineItemsEmpty(false);
                    PutPurchaseOrderBackToViewState();
                    BindLineItems();
                }
                else
                {
                    ucInformation.ShowErrorMessage("There is no purchase ordere in the database with this PO/Contract number.");
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
    }
}