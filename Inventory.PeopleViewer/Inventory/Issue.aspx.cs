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
                hiddenFieldItemID.Value = hiddenFieldEmployeeID.Value = string.Empty;
            GridInventoryIssue.Visible = false;
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

                if (_InventoryIssues.Count == 0)
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

        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {

        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetInventoryIssuesFromViewstate();
                if (_InventoryIssues.Count > 0)
                {
                    _InventoryIssueRepository.SaveInventoryIssue(_InventoryIssues);
                    ucInformation.ShowSaveInfomationMessage();
                    ClearFormData();
                }
                else
                    throw new ApplicationException("Please add one or more item in order to save inventory issue.");

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

        }

        protected void GridInventoryIssue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridInventoryIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridInventoryIssue_RowEditing(object sender, GridViewEditEventArgs e)
        {

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
                ReturnedDate = null
            });

        }

        private void ValidateFooterData()
        {
            if (TextBoxSerialNo == null || string.IsNullOrWhiteSpace(TextBoxSerialNo.Text))
                throw new ApplicationException("The serial no field is required.");
            if (TextBoxItemDescription == null || string.IsNullOrWhiteSpace(TextBoxItemDescription.Text) || string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                throw new ApplicationException("The item field is required.");
            if (string.IsNullOrWhiteSpace(txtHelpDeskTicket.Text))
                throw new ApplicationException("The Helpdesk ticket number is required.");

            var duplicateItems = _InventoryIssues.FirstOrDefault(ii => ii.SerialNo == TextBoxSerialNo.Text.Trim() && ii.ItemID == int.Parse(hiddenFieldItemID.Value));
            if (duplicateItems != null)
                throw new ApplicationException("The item with this serial number already has been assigned to this employee.");

            _ReceivedLineItem = _InventoryIssueRepository.FindReceivedLineItemBySerialNumber(TextBoxSerialNo.Text, int.Parse(hiddenFieldItemID.Value));
            if (_ReceivedLineItem == null)
                throw new ApplicationException("Please provide valid serial number.");

            _InventoryIssue = _InventoryIssueRepository.IsAssignToOtherEmployee(int.Parse(hiddenFieldItemID.Value), TextBoxSerialNo.Text);
            if (_InventoryIssues != null)
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
            }
        }

        protected void txtCivilID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearInventoryIssues();
                _InventoryIssues = _InventoryIssueRepository.FindInventoryIssueByEmployee(int.Parse(hiddenFieldEmployeeID.Value));
                PutInventoryIssuesBackToViewstate();
                BindInventoryIssues();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }
    }
}