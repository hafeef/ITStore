using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Administration
{
    public partial class ItemTypes : System.Web.UI.Page
    {
        IItemTypeRepository _ItemTypeRepository = new ItemTypeRepository(new AdminContext());
        TextBox TextBoxItemType = null;
        List<ItemTypeVM> _ItemTypes = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearFormData();
                BindItemTypeToGrid();
            }
        }

        private void BindItemTypeToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _ItemTypes = ViewState[ViewStateKeys.SearchResult] as List<ItemTypeVM>;
                else
                {
                    _ItemTypes = _ItemTypeRepository.GetAllItemTypes();
                    if (_ItemTypes.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _ItemTypes.Add(new ItemTypeVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridItemType.DataSource = _ItemTypes;
                gridItemType.DataBind();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateItemType(txtItemTypeSearch);
                _ItemTypes = _ItemTypeRepository.SearchItemTypeByName(txtItemTypeSearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _ItemTypes;
                BindItemTypeToGrid();
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



        protected void gridItemType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            ClearFormData();
            SetGridRowIndexToMinusOne();
            BindItemTypeToGrid();
        }

        private void ClearFormData()
        {
            txtItemTypeSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (TextBoxItemType != null)
                TextBoxItemType.Text = string.Empty;

        }

        protected void gridItemType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var itemTypeID = gridItemType.DataKeys[e.RowIndex]["ItemTypeID"].ToString();
                _ItemTypeRepository.DeleteItemType(int.Parse(itemTypeID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemTypeToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridItemType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var itemTypeID = int.Parse(gridItemType.DataKeys[e.RowIndex]["ItemTypeID"].ToString());
                TextBoxItemType = gridItemType.Rows[e.RowIndex].FindControl("txtUpdateItemType") as TextBox;
                ValidateItemType(TextBoxItemType);
                _ItemTypeRepository.UpdateItemType(new ItemTypeVM() { ItemTypeID = itemTypeID, Name = TextBoxItemType.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemTypeToGrid();
                ucInformation.ShowModifyInfomationMessage();
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

        protected void gridItemType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridItemType.PageIndex = e.NewPageIndex;
            BindItemTypeToGrid();
        }

        protected void gridItemType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridRowIndexToMinusOne();
            BindItemTypeToGrid();
        }

        private void SetGridRowIndexToMinusOne()
        {
            gridItemType.EditIndex = -1;
        }

        protected void gridItemType_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gridItemType.EditIndex = e.NewEditIndex;
            BindItemTypeToGrid();
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {

                SetFooterData();
                ValidateItemType(TextBoxItemType);
                _ItemTypeRepository.CreateNewItemType(new ItemTypeVM() { Name = TextBoxItemType.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemTypeToGrid();
                ucInformation.ShowSaveInfomationMessage();
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

        private void ValidateItemType(TextBox itemTypeTextBox)
        {
            if (string.IsNullOrWhiteSpace(itemTypeTextBox.Text.Trim()))
                throw new ApplicationException("The item type field is required.");
        }

        private void SetFooterData()
        {
            if (gridItemType.FooterRow != null)
                TextBoxItemType = gridItemType.FooterRow.FindControl("txtNewItemType") as TextBox;
        }
    }
}