using Core.Common.BaseTypes;
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
    public partial class Items : System.Web.UI.Page
    {
        IItemRepository _itemRepository = new ItemRepository(new AdminContext());
        IBrandRepository _brandRepository = new BrandRepository(new AdminContext());
        ICategoryRepository _categoryRepository = new CategoryRepository(new AdminContext());
        IItemTypeRepository _itemTypeRepository = new ItemTypeRepository(new AdminContext());

        TextBox TextBoxItemDescription = null;
        TextBox TextBoxPartNumber = null;
        DropDownList DropDownCategories = null;
        DropDownList DropDownBrands = null;
        DropDownList DropDownItemTypes = null;

        List<BrandVM> _Brands = null;
        List<CategoryVM> _Categories = null;
        List<ItemTypeVM> _ItemTypes = null;
        List<ItemVM> _Items = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetAllBrands();
                GetAllCategories();
                GetAllItemTypes();
                if (!Page.IsPostBack)
                {
                    BindItemsToGrid();
                    ClearFormData();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            ucInformation.ShowErrorMessage();
            Server.ClearError();
        }

        private void GetAllItemTypes()
        {
            _ItemTypes = _itemTypeRepository.GetAllItemTypes();
        }

        private void GetAllCategories()
        {
            _Categories = _categoryRepository.GetAllCategory();
        }

        private void GetAllBrands()
        {
            _Brands = _brandRepository.GetAllBrands();
        }

        private void ClearFormData()
        {
            txtItemSearch.Text = string.Empty;
            SetFooterData();

            if (TextBoxItemDescription != null)
                TextBoxItemDescription.Text = string.Empty;
            if (TextBoxPartNumber != null)
                TextBoxPartNumber.Text = string.Empty;

            if (DropDownCategories != null)
                DropDownCategories.SelectedIndex = 0;
            if (DropDownBrands != null)
                DropDownBrands.SelectedIndex = 0;
            if (DropDownItemTypes != null)
                DropDownItemTypes.SelectedIndex = 0;

            ViewState[ViewStateKeys.SearchResult] = null;
        }

        private void SetFooterData()
        {
            try
            {
                if (gridItems.FooterRow != null)
                {
                    TextBoxItemDescription = gridItems.FooterRow.FindControl("txtNewItemDescription") as TextBox;
                    TextBoxPartNumber = gridItems.FooterRow.FindControl("txtNewPartNumber") as TextBox;

                    DropDownBrands = gridItems.FooterRow.FindControl("ddlFooterBrand") as DropDownList;
                    DropDownCategories = gridItems.FooterRow.FindControl("ddlFooterCategory") as DropDownList;
                    DropDownItemTypes = gridItems.FooterRow.FindControl("ddlFooterItemType") as DropDownList;
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindItemsToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Items = ViewState[ViewStateKeys.SearchResult] as List<ItemVM>;
                else
                {
                    _Items = _itemRepository.GetAllItems();
                    if (_Items.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Items.Add(new ItemVM() { });
                    }
                    else
                    {
                        ViewState[ViewStateKeys.IsEmpty] = false;
                    }
                }
                gridItems.DataSource = _Items;
                gridItems.DataBind();
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
                _Items = _itemRepository.SearchItemByDescription(txtItemSearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _Items;
                BindItemsToGrid();

            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridRowIndexToMinusOne();
            BindItemsToGrid();
        }

        private void SetGridRowIndexToMinusOne()
        {
            gridItems.EditIndex = -1;
        }

        protected void gridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var itemID = gridItems.DataKeys[e.RowIndex]["ItemID"].ToString();
                _itemRepository.DeleteItem(int.Parse(itemID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemsToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var itemID = gridItems.DataKeys[e.RowIndex]["ItemID"].ToString();

                TextBoxItemDescription = gridItems.Rows[e.RowIndex].FindControl("txtUpdateItemDescription") as TextBox;
                TextBoxPartNumber = gridItems.Rows[e.RowIndex].FindControl("txtUpdatePartNumber") as TextBox;

                DropDownCategories = gridItems.Rows[e.RowIndex].FindControl("ddlEditCategory") as DropDownList;
                DropDownBrands = gridItems.Rows[e.RowIndex].FindControl("ddlEditBrand") as DropDownList;
                DropDownItemTypes = gridItems.Rows[e.RowIndex].FindControl("ddlEditItemType") as DropDownList;

                ValidateItem();

                _itemRepository.UpdateItem(new ItemVM()
                {
                    ItemID = int.Parse(itemID),
                    Description = TextBoxItemDescription.Text,
                    PartNumber = TextBoxPartNumber.Text,
                    BrandID = int.Parse(DropDownBrands.SelectedValue),
                    CategoryID = int.Parse(DropDownCategories.SelectedValue),
                    ItemTypeID = int.Parse(DropDownItemTypes.SelectedValue)
                });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemsToGrid();
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

        private void ValidateItem()
        {
            if (string.IsNullOrWhiteSpace(TextBoxItemDescription.Text))
                throw new ApplicationException("The item description field is required.");
            if (string.IsNullOrWhiteSpace(TextBoxPartNumber.Text))
                throw new ApplicationException("The part number field is required.");
            if (DropDownCategories.SelectedIndex == 0)
                throw new ApplicationException("The category field is required.");
            if (DropDownBrands.SelectedIndex == 0)
                throw new ApplicationException("The brand field is required.");
            if (DropDownItemTypes.SelectedIndex == 0)
                throw new ApplicationException("The item type field is required.");
        }

        protected void gridItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridRowIndexToMinusOne();
            BindItemsToGrid();
        }

        protected void gridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridItems.EditIndex = e.NewEditIndex;
                BindItemsToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                    if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                        e.Row.Visible = false;

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    BindDropDownLists(e, "ddlFooterBrand", null, _Brands);
                    BindDropDownLists(e, "ddlFooterCategory", null, _Categories);
                    BindDropDownLists(e, "ddlFooterItemType", null, _ItemTypes);
                }

                if (e.Row.RowType == DataControlRowType.DataRow && gridItems.EditIndex == e.Row.RowIndex)
                {
                    var brandID = gridItems.DataKeys[e.Row.RowIndex].Values["BrandID"].ToString();
                    var categoryID = gridItems.DataKeys[e.Row.RowIndex].Values["CategoryID"].ToString();
                    var itemTypeID = gridItems.DataKeys[e.Row.RowIndex].Values["ItemTypeID"].ToString();

                    BindDropDownLists(e, "ddlEditBrand", brandID, _Brands);
                    BindDropDownLists(e, "ddlEditCategory", categoryID, _Categories);
                    BindDropDownLists(e, "ddlEditItemType", itemTypeID, _ItemTypes);
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridItems.PageIndex = e.NewPageIndex;
            BindItemsToGrid();
        }

        private void BindDropDownLists(GridViewRowEventArgs e, string dropDownID, string selectedValue, IEnumerable<EntityBaseVM> dropDownSource)
        {
            try
            {
                var dropDownList = e.Row.FindControl(dropDownID) as DropDownList;
                dropDownList.DataSource = dropDownSource;
                dropDownList.DataBind();
                dropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
                if (!string.IsNullOrWhiteSpace(selectedValue))
                    dropDownList.SelectedValue = selectedValue;
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
                ValidateItem();
                _itemRepository.CreateNewItem(new ItemVM()
                {
                    Description = TextBoxItemDescription.Text.Trim(),
                    PartNumber = TextBoxPartNumber.Text.Trim(),
                    BrandID = int.Parse(DropDownBrands.SelectedValue),
                    CategoryID = int.Parse(DropDownCategories.SelectedValue),
                    ItemTypeID = int.Parse(DropDownItemTypes.SelectedValue)
                });

                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindItemsToGrid();
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
    }
}