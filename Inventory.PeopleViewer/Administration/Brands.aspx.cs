using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.PeopleViewer.Keys;
using Inventory.Repositories.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Administration
{
    public partial class Brands : Page
    {
        IBrandRepository _BrandRepository = new BrandRepository(new AdminContext());
        TextBox TextBoxBrand = null;
        List<BrandVM> _Brands = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ClearFormData();
                    BindBrandsToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindBrandsToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Brands = ViewState[ViewStateKeys.SearchResult] as List<BrandVM>;
                else
                {
                    _Brands = _BrandRepository.GetAllBrands();
                    if (_Brands.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Brands.Add(new BrandVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridBrand.DataSource = _Brands;
                gridBrand.DataBind();
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
                if (IsValid)
                {
                    _Brands = _BrandRepository.SearchBrandByName(txtBrandSearch.Text.Trim());
                    ViewState[ViewStateKeys.SearchResult] = _Brands;
                    BindBrandsToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridBrand_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridEditIndexToMinusOne();
            BindBrandsToGrid();
            ucInformation.ClearInformationLables();
        }

        private void ClearFormData()
        {
            txtBrandSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (TextBoxBrand != null)
                TextBoxBrand.Text = string.Empty;
        }

        protected void gridBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var brandID = gridBrand.DataKeys[e.RowIndex]["BrandID"].ToString();
                    _BrandRepository.DeleteBrand(int.Parse(brandID));
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindBrandsToGrid();
                    ucInformation.ShowDeleteInfomationMessage();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridBrand_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var brandID = int.Parse(gridBrand.DataKeys[e.RowIndex]["BrandID"].ToString());
                    TextBoxBrand = gridBrand.Rows[e.RowIndex].FindControl("txtUpdateBrand") as TextBox;
                    _BrandRepository.UpdateBrand(new BrandVM() { BrandID = brandID, Name = TextBoxBrand.Text.Trim() });
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindBrandsToGrid();
                    ucInformation.ShowModifyInfomationMessage();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetGridEditIndexToMinusOne()
        {
            gridBrand.EditIndex = -1;
        }

        protected void gridBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBrand.PageIndex = e.NewPageIndex;
            BindBrandsToGrid();
        }

        protected void gridBrand_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridEditIndexToMinusOne();
                BindBrandsToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridBrand_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridBrand.EditIndex = e.NewEditIndex;
                BindBrandsToGrid();
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
                    _BrandRepository.CreateNewBrand(new BrandVM() { Name = TextBoxBrand.Text.Trim() });
                    ucInformation.ShowSaveInfomationMessage();
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindBrandsToGrid();
                }

            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetFooterData()
        {
            if (gridBrand.FooterRow != null)
                TextBoxBrand = gridBrand.FooterRow.FindControl("txtNewBrand") as TextBox;
        }



    }
}