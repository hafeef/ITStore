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
    public partial class Categories : System.Web.UI.Page
    {
        ICategoryRepository _CategoryRepository = new CategoryRepository(new AdminContext());
        TextBox textBoxCategory = null;
        List<CategoryVM> _Categories = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategoriesToGrid();
                ClearFormData();
            }
        }

        private void BindCategoriesToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Categories = ViewState[ViewStateKeys.SearchResult] as List<CategoryVM>;
                else
                {
                    _Categories = _CategoryRepository.GetAllCategory();
                    if (_Categories.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Categories.Add(new CategoryVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridCategory.DataSource = _Categories;
                gridCategory.DataBind();
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
                ValidateCategory(txtCategorySearch);
                _Categories = _CategoryRepository.SearchCategoryByName(txtCategorySearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _Categories;
                BindCategoriesToGrid();
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

        protected void gridCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridRowIndexToMinusOne();
            BindCategoriesToGrid();
        }

        private void ClearFormData()
        {
            txtCategorySearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (textBoxCategory != null)
            {
                textBoxCategory.Text = string.Empty;
            }
        }

        protected void gridCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var categoryID = gridCategory.DataKeys[e.RowIndex]["CategoryID"].ToString();
                _CategoryRepository.DeleteCategory(int.Parse(categoryID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindCategoriesToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                textBoxCategory = gridCategory.Rows[e.RowIndex].FindControl("txtUpdateCategory") as TextBox;
                var categoryID = int.Parse(gridCategory.DataKeys[e.RowIndex]["CategoryID"].ToString());
                ValidateCategory(textBoxCategory);
                _CategoryRepository.UpdateCategory(new CategoryVM() { CategoryID = categoryID, Name = textBoxCategory.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindCategoriesToGrid();
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

        private void SetGridRowIndexToMinusOne()
        {
            gridCategory.EditIndex = -1;
        }

        protected void gridCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategory.PageIndex = e.NewPageIndex;
            BindCategoriesToGrid();
        }

        protected void gridCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridRowIndexToMinusOne();
                BindCategoriesToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridCategory.EditIndex = e.NewEditIndex;
                BindCategoriesToGrid();
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
                SetFooterData();
                ValidateCategory(textBoxCategory);
                _CategoryRepository.CreateNewCategory(new CategoryVM() { Name = textBoxCategory.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindCategoriesToGrid();
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

        private void SetFooterData()
        {
            if (gridCategory.FooterRow != null)
                textBoxCategory = gridCategory.FooterRow.FindControl("txtNewCategory") as TextBox;
        }

        private void ValidateCategory(TextBox textBoxCategory)
        {
            if (textBoxCategory == null || string.IsNullOrWhiteSpace(textBoxCategory.Text))
                throw new ApplicationException("The category name field is required.");
        }
    }
}