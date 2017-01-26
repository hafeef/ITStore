using Core.Common.Enums;
using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class Issue : System.Web.UI.Page
    {
        IInventoryIssueRepository _InventoryIssueRepository = new InventoryIssueRepository(new InventoryContext());
        List<InventoryIssueVM> _InventoryIssues = null;
        ReceivedLineItemVM _ReceivedLineItem = null;
        InventoryIssueVM _InventoryIssue = null;

        private TextBox TextBoxItemDescription { get; set; }
        private TextBox TextBoxSerialNo { get; set; }
        private TextBox TextBoxHeplDeskTicket { get; set; }
        private CheckBox CheckBoxIsReturned { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearFormData();
            }
        }

        private void ClearFormData()
        {
            ClearControls();
            ClearInventoryIssues();
        }

        private void ClearControls()
        {
            txtCivilID.Text = txtEmployeeName.Text = txtHelpDeskTicket.Text =
                 hiddenFieldEmployeeID.Value = string.Empty;
            ClearItemID();
            GridInventoryIssue.Visible = false;
        }

        private void ClearItemID()
        {
            hiddenFieldItemID.Value = string.Empty;
        }

        private void ClearInventoryIssues()
        {
            GetInventoryIssuesFromViewstate();
            if (_InventoryIssues != null)
                _InventoryIssues.Clear();
            ViewState.Clear();
        }

        private void PutInventoryIssuesBackToViewstate()
        {
            ViewState[ViewStateKeys.SearchResult] = _InventoryIssues;
        }

        private void GetInventoryIssuesFromViewstate()
        {
            if (ViewState[ViewStateKeys.SearchResult] != null)
                _InventoryIssues = ViewState[ViewStateKeys.SearchResult] as List<InventoryIssueVM>;
        }

        private void BindInventoryIssues()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _InventoryIssues = ViewState[ViewStateKeys.SearchResult] as List<InventoryIssueVM>;

                if (_InventoryIssues.Where(ii => ii.EntityState != ObjectState.Deleted).ToList().Count == 0)
                {
                    ViewState[ViewStateKeys.IsEmpty] = true;
                    _InventoryIssues.Add(new InventoryIssueVM());
                }
                else
                    ViewState[ViewStateKeys.IsEmpty] = false;

                GridInventoryIssue.Visible = true;
                GridInventoryIssue.DataSource = _InventoryIssues.Where(ii => ii.EntityState != ObjectState.Deleted).ToList();
                GridInventoryIssue.DataBind();

                if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                {
                    _InventoryIssues.Clear();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetInventoryIssuesFromViewstate();
                if (_InventoryIssues != null && _InventoryIssues.Count > 0)
                {
                    _InventoryIssueRepository.SaveInventoryIssue(_InventoryIssues);
                    ucInformation.ShowSaveInfomationMessage();
                    ClearFormData();
                }
                else
                    throw new ApplicationException("Please select an employee and add one or more item in order to save inventory issue.");

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

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void GridInventoryIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var serialNumber = e.Keys["SerialNo"].ToString();
                var itemID = int.Parse(e.Keys["ItemID"].ToString());
                var inventoryIssueID = int.Parse(e.Keys["InventoryIssueID"].ToString());

                GetInventoryIssuesFromViewstate();
                if (inventoryIssueID == 0)
                    _InventoryIssues.RemoveAll(ii => ii.SerialNo == serialNumber && ii.ItemID == itemID && ii.InventoryIssueID == inventoryIssueID);
                else
                    _InventoryIssues.Find(ii => ii.SerialNo == serialNumber && ii.ItemID == itemID && ii.InventoryIssueID == inventoryIssueID).EntityState = ObjectState.Deleted;
                PutInventoryIssuesBackToViewstate();
                BindInventoryIssues();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridInventoryIssue_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var serialNumber = e.Keys["SerialNo"].ToString();
                var itemID = int.Parse(e.Keys["ItemID"].ToString());
                var inventoryIssueID = int.Parse(e.Keys["InventoryIssueID"].ToString());

                SetEditData(e);
                GetInventoryIssuesFromViewstate();

                if (string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                    hiddenFieldItemID.Value = itemID.ToString();

                ValidateFooterData();

                PrepareModifiedData(serialNumber, itemID, inventoryIssueID);

                PutInventoryIssuesBackToViewstate();
                SetGridViewEditRowIndexToMinusOne();
                ClearItemID();
                BindInventoryIssues();
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

        private void PrepareModifiedData(string serialNumber, int itemID, int inventoryIssueID)
        {
            _InventoryIssue = _InventoryIssues.FirstOrDefault(ii => ii.SerialNo == serialNumber && ii.ItemID == itemID && ii.InventoryIssueID == inventoryIssueID);

            if (_InventoryIssue.InventoryIssueID == 0)
                _InventoryIssue.EntityState = ObjectState.Added;
            else
                _InventoryIssue.EntityState = ObjectState.Modified;

            _InventoryIssue.ItemID = int.Parse(hiddenFieldItemID.Value);
            _InventoryIssue.ItemDescription = TextBoxItemDescription.Text;
            _InventoryIssue.SerialNo = TextBoxSerialNo.Text;
            _InventoryIssue.HelpDeskTicket = TextBoxHeplDeskTicket.Text;
            _InventoryIssue.ReceivedLineItemID = _ReceivedLineItem.ReceivedLineItemID;

            if (inventoryIssueID > 0)
            {
                _InventoryIssue.IsReturned = CheckBoxIsReturned.Checked;
                if (CheckBoxIsReturned.Checked)
                    _InventoryIssue.ReturnedDate = DateTime.Now;
                else
                    _InventoryIssue.ReturnedDate = null;
            }
        }

        private void SetEditData(GridViewUpdateEventArgs e)
        {
            TextBoxItemDescription = GridInventoryIssue.Rows[e.RowIndex].FindControl("txtItemDescription") as TextBox;
            TextBoxSerialNo = GridInventoryIssue.Rows[e.RowIndex].FindControl("txtSerialNo") as TextBox;
            TextBoxHeplDeskTicket = GridInventoryIssue.Rows[e.RowIndex].FindControl("txtHelpDeskTicket") as TextBox;
            CheckBoxIsReturned = GridInventoryIssue.Rows[e.RowIndex].FindControl("chkIsReturned") as CheckBox;
        }

        protected void GridInventoryIssue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridViewEditRowIndexToMinusOne();
            BindInventoryIssues();
        }

        void SetGridViewEditRowIndexToMinusOne()
        {
            GridInventoryIssue.EditIndex = -1;
        }

        protected void GridInventoryIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SetGridViewEditRowIndexToMinusOne();
            GridInventoryIssue.EditIndex = e.NewPageIndex;
            BindInventoryIssues();
        }

        protected void GridInventoryIssue_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SetGridViewEditRowIndexToMinusOne();
            GridInventoryIssue.EditIndex = e.NewEditIndex;
            BindInventoryIssues();
        }

        protected void GridInventoryIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        protected void linkButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SetFooterData();
                GetInventoryIssuesFromViewstate();
                ValidateFooterData();
                PrepareInventoryIssueData();
                PutInventoryIssuesBackToViewstate();
                SetGridViewEditRowIndexToMinusOne();
                BindInventoryIssues();
                ClearItemID();
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

        private void PrepareInventoryIssueData()
        {

            _InventoryIssues.Add(new InventoryIssueVM()
            {
                EntityState = ObjectState.Added,
                EmployeeID = int.Parse(hiddenFieldEmployeeID.Value),
                HelpDeskTicket = txtHelpDeskTicket.Text.Trim(),
                ItemID = int.Parse(hiddenFieldItemID.Value),
                ItemDescription = TextBoxItemDescription.Text.Trim(),
                SerialNo = TextBoxSerialNo.Text.Trim(),
                IsReturned = false,
                IssuedDate = DateTime.Now,
                ReturnedDate = null,
                ReceivedLineItemID = _ReceivedLineItem.ReceivedLineItemID
            });

        }

        private void ValidateFooterData()
        {
            if (TextBoxSerialNo == null || string.IsNullOrWhiteSpace(TextBoxSerialNo.Text))
                throw new ApplicationException("The serial no field is required.");
            if (TextBoxItemDescription == null || string.IsNullOrWhiteSpace(TextBoxItemDescription.Text) || string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                throw new ApplicationException("The item field is required.");
            if (string.IsNullOrWhiteSpace(TextBoxHeplDeskTicket.Text))
                throw new ApplicationException("The Helpdesk ticket number is required.");


            _ReceivedLineItem = _InventoryIssueRepository.FindReceivedLineItemBySerialNumber(TextBoxSerialNo.Text, int.Parse(hiddenFieldItemID.Value));
            if (_ReceivedLineItem == null)
                throw new ApplicationException("Please provide valid serial number.");

            _InventoryIssue = _InventoryIssueRepository.IsAssignToOtherEmployee(int.Parse(hiddenFieldItemID.Value), TextBoxSerialNo.Text);
            if (_InventoryIssue != null && int.Parse(hiddenFieldEmployeeID.Value) != _InventoryIssue.EmployeeID)
            {
                throw new ApplicationException($"The item with this serial number has been assigned to another employee. Name: {_InventoryIssue.EmployeeName}, Civil ID: {_InventoryIssue.CivilID}.");
            }

        }

        private void SetFooterData()
        {
            if (GridInventoryIssue.FooterRow != null)
            {
                TextBoxItemDescription = GridInventoryIssue.FooterRow.FindControl("txtItemDescription") as TextBox;
                TextBoxSerialNo = GridInventoryIssue.FooterRow.FindControl("txtSerialNo") as TextBox;
                TextBoxHeplDeskTicket = txtHelpDeskTicket;
            }
        }

        protected void txtCivilID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmployeeName.Text) || string.IsNullOrWhiteSpace(hiddenFieldEmployeeID.Value))
                    throw new ApplicationException("Please select an employee.");

                ClearInventoryIssues();
                _InventoryIssues = _InventoryIssueRepository.FindInventoryIssueByEmployee(int.Parse(hiddenFieldEmployeeID.Value));
                PutInventoryIssuesBackToViewstate();
                SetGridViewEditRowIndexToMinusOne();
                BindInventoryIssues();
            }
            catch(ApplicationException Ae)
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