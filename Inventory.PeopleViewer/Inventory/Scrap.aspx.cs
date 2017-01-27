using Core.Common.Enums;
using Inventory.Contracts.Administration;
using Inventory.Contracts.Inventory;
using Inventory.Data.Administration;
using Inventory.Data.Inventory;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Administration;
using Inventory.Repositories.Inventory;
using Inventory.ViewModels.Administration;
using Inventory.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class Scrap : System.Web.UI.Page
    {
        IInventoryScrapRepository _InventoryScrapRepository = new InventoryScrapRepository(new InventoryContext());
        IInventoryIssueRepository _InventoryIssueRepository = new InventoryIssueRepository(new InventoryContext());

        HashSet<InventoryScrapVM> _Scraps = null;
        ReceivedLineItemVM _ReceivedLineItem = null;
        InventoryIssueVM _InventoryIssue = null;

        public TextBox TextBoxItemDescription { get; set; }
        public TextBox TextBoxSerialNumber { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearFormData();
                BindInventoryScrapsToGrid();
                PutInventoryScrapItemsBackToViewState();
            }
        }

        private void ClearFormData()
        {
            ClearItemID();
            txtItemDescription.Text = string.Empty;
            ViewState.Clear();
            SetFooterData();
            if (TextBoxItemDescription != null)
                TextBoxItemDescription.Text = string.Empty;
            if (TextBoxSerialNumber != null)
                TextBoxSerialNumber.Text = string.Empty;
        }

        private void SetFooterData()
        {
            if (GridViewInventoryScraps.FooterRow != null)
            {
                TextBoxItemDescription = GridViewInventoryScraps.FooterRow.FindControl("txtItemDescription") as TextBox;
                TextBoxSerialNumber = GridViewInventoryScraps.FooterRow.FindControl("txtSerialNumber") as TextBox;
            }

        }

        private void BindInventoryScrapsToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Scraps = ViewState[ViewStateKeys.SearchResult] as HashSet<InventoryScrapVM>;
                else
                {
                    if (ViewState[ViewStateKeys.ScrapItems] == null)
                        _Scraps = _InventoryScrapRepository.GetAllInventoryScraps();

                    if (_Scraps.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Scraps.Add(new InventoryScrapVM() { ItemID = 0, SerialNo = string.Empty });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                GridViewInventoryScraps.DataSource = _Scraps.Where(ins => ins.EntityState != ObjectState.Deleted).ToList();
                GridViewInventoryScraps.DataBind();

                if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                {
                    _Scraps.Clear();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }


        private void PutInventoryScrapItemsBackToViewState()
        {
            ViewState[ViewStateKeys.ScrapItems] = _Scraps;
        }

        private void GetInventoryScrapItemsFromViewState()
        {
            if (ViewState[ViewStateKeys.ScrapItems] != null)
                _Scraps = ViewState[ViewStateKeys.ScrapItems] as HashSet<InventoryScrapVM>;
        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            BindInventoryScrapsToGrid();
        }



        protected void GridViewInventoryScraps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }





        protected void GridViewInventoryScraps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewInventoryScraps.PageIndex = e.NewPageIndex;
            BindInventoryScrapsToGrid();
        }



        protected void GridViewInventoryScraps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        private void ValidateScrap()
        {
            if (TextBoxItemDescription == null || string.IsNullOrWhiteSpace(TextBoxItemDescription.Text) || string.IsNullOrWhiteSpace(hiddenFieldItemID.Value))
                throw new ApplicationException("Please select a valid item.");

            if (TextBoxSerialNumber == null || string.IsNullOrWhiteSpace(TextBoxSerialNumber.Text))
                throw new ApplicationException("The serial number field is reuired.");

            _ReceivedLineItem = _InventoryIssueRepository.FindReceivedLineItemBySerialNumber(TextBoxSerialNumber.Text.Trim(), int.Parse(hiddenFieldItemID.Value));
            if (_ReceivedLineItem == null)
                throw new ApplicationException("Please provide valid serial number.");

            _InventoryIssue = _InventoryIssueRepository.IsAssignToOtherEmployee(int.Parse(hiddenFieldItemID.Value), TextBoxSerialNumber.Text.Trim());
            if (_InventoryIssue != null)
            {
                throw new ApplicationException($"The item with this serial number has been assigned to an employee. Name: {_InventoryIssue.EmployeeName}, Civil ID: {_InventoryIssue.CivilID}.");
            }
        }

        protected void linkButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SetFooterData();
                ValidateScrap();
                GetInventoryScrapItemsFromViewState();
                bool isDuplicate = _Scraps.Add(new InventoryScrapVM() { ItemID = int.Parse(hiddenFieldItemID.Value), EntityState = ObjectState.Added, ItemDescription = TextBoxItemDescription.Text, SerialNo = TextBoxSerialNumber.Text });
                if (!isDuplicate)
                    throw new ApplicationException("The item with this serial number has been scrapped.");
                PutInventoryScrapItemsBackToViewState();
                BindInventoryScrapsToGrid();
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

        protected void txtItemDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hiddenFieldItemID.Value) || string.IsNullOrWhiteSpace(txtItemDescription.Text))
                    throw new ApplicationException("Please select a valid item.");


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

        private void ClearItemID()
        {
            hiddenFieldItemID.Value = string.Empty;
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetInventoryScrapItemsFromViewState();
                _InventoryScrapRepository.SaveInventoryScrap(_Scraps.Where(ins => ins.EntityState != ObjectState.Unchanged));
                ucInformation.ShowSaveInfomationMessage();
                ClearFormData();
                BindInventoryScrapsToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }
    }
}