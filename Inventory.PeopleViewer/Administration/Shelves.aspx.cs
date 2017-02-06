using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Administration
{
    public partial class Shelves : System.Web.UI.Page
    {
        IShelfRepository _shelfRepository = new ShelfRepository(new AdminContext());
        List<ShelfVM> _shelves = null;
        TextBox txtShelfName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindShelvesToGrid();
                ClearFormData();
            }
        }

        private void ClearFormData()
        {
            txtShelfSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (txtShelfName != null)
                txtShelfName.Text = string.Empty;
        }

        private void SetFooterData()
        {
            if (gridShelves.FooterRow != null)
                txtShelfName = gridShelves.FooterRow.FindControl("txtNewShelf") as TextBox;
        }

        private void BindShelvesToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                {
                    _shelves = ViewState[ViewStateKeys.SearchResult] as List<ShelfVM>;
                }
                else
                {
                    _shelves = _shelfRepository.GetAllShelves();
                    if (_shelves.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _shelves.Add(new ShelfVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridShelves.DataSource = _shelves;
                gridShelves.DataBind();
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
                if (IsValid)
                {
                    SetFooterData();
                    _shelfRepository.CreateNewShelf(new ShelfVM() { Name = txtShelfName.Text });
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindShelvesToGrid();
                    ucInformation.ShowSaveInfomationMessage();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetGridEditIndexToMinusOne()
        {
            gridShelves.EditIndex = -1;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            BindShelvesToGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    _shelves = _shelfRepository.SearchShelfByName(txtShelfSearch.Text.Trim());
                    ViewState[ViewStateKeys.SearchResult] = _shelves;
                    BindShelvesToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridShelves_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var rackID = gridShelves.DataKeys[e.RowIndex]["ShelfID"].ToString();
                _shelfRepository.DeleteShelf(int.Parse(rackID));
                SetGridEditIndexToMinusOne();
                ClearFormData();
                BindShelvesToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridShelves_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var shelfID = int.Parse(gridShelves.DataKeys[e.RowIndex]["ShelfID"].ToString());
                    txtShelfName = gridShelves.Rows[e.RowIndex].FindControl("txtUpdateShelf") as TextBox;
                    _shelfRepository.UpdateShelf(new ShelfVM() { ShelfID = shelfID, Name = txtShelfName.Text.Trim() });
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindShelvesToGrid();
                    ucInformation.ShowModifyInfomationMessage();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridShelves_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridEditIndexToMinusOne();
            BindShelvesToGrid();
        }

        protected void gridShelves_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridShelves.PageIndex = e.NewPageIndex;
            BindShelvesToGrid();
        }

        protected void gridShelves_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridShelves.EditIndex = e.NewEditIndex;
            BindShelvesToGrid();
        }

        protected void gridShelves_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }
    }
}