using Inventory.Contracts.Inventory;
using Inventory.Data.Inventory;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class Issue : System.Web.UI.Page
    {
        IInventoryIssueRepository _InventoryIssueRepository = new InventoryIssueRepository(new InventoryContext());
        List<InventoryIssueVM> _InventoryIssues = null;

        private TextBox TextBoxItemDescription { get; set; }
        private TextBox TextBoxSerialNo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearFormData();
                BindInventoryIssues();
            }
        }

        private void ClearFormData()
        {
            txtCivilID.Text = txtEmployeeName.Text = txtHelpDeskTicket.Text =
                hiddenFieldItemID.Value = hiddenFieldEmployeeID.Value = string.Empty;


        }

        private void BindInventoryIssues()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _InventoryIssues = ViewState[ViewStateKeys.SearchResult] as List<InventoryIssueVM>;
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _InventoryIssues = new List<InventoryIssueVM>()
                        {
                            new InventoryIssueVM() { }
                        };
                    }

                }
                GridInventoryIssue.DataSource = _InventoryIssues;
                GridInventoryIssue.DataBind();
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

        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
        }

        protected void GridInventoryIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

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
                ValidateFooterData();
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

        private void ValidateFooterData()
        {
            if (TextBoxSerialNo == null || string.IsNullOrWhiteSpace(TextBoxSerialNo.Text))
                throw new ApplicationException("The serial no field is required.");
            if (TextBoxItemDescription == null || string.IsNullOrWhiteSpace(TextBoxItemDescription.Text))
                throw new ApplicationException("The item field is required.");
        }

        private void SetFooterData()
        {
            if (GridInventoryIssue.FooterRow != null)
            {
                var footerRow = GridInventoryIssue.FooterRow;
                TextBoxItemDescription = footerRow.FindControl("txtItemDescription") as TextBox;
                TextBoxSerialNo = footerRow.FindControl("txtSerialNo") as TextBox;
            }
        }
    }
}