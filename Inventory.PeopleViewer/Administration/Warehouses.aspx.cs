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
    public partial class Warehouses : System.Web.UI.Page
    {
        IWarehouseRepository _warehouseRepository = new WarehouseRepository(new AdminContext());
        ILocationRepository _locationRepository = new LocationRepository(new AdminContext());

        TextBox TextBoxWarehouse = null;
        DropDownList DropDownLocation = null;

        List<LocationVM> _Locations = null;
        List<WareHouseVM> _WareHouses = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetAllLocations();
                if (!Page.IsPostBack)
                {
                    BindWarehousesToGrid();
                    ClearFormData();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void GetAllLocations()
        {
            _Locations = _locationRepository.GetAllLocations();
        }

        private void ClearFormData()
        {
            txtWarehouseSearch.Text = string.Empty;
            SetFooterData();
            if (TextBoxWarehouse != null)
                TextBoxWarehouse.Text = string.Empty;
            if (DropDownLocation != null)
                DropDownLocation.SelectedIndex = 0;
            ViewState[ViewStateKeys.SearchResult] = null;
        }

        private void SetFooterData()
        {
            if (gridWarehouse.FooterRow != null)
            {
                TextBoxWarehouse = gridWarehouse.FooterRow.FindControl("txtNewWarehouse") as TextBox;
                DropDownLocation = gridWarehouse.FooterRow.FindControl("ddlFooterLocation") as DropDownList;
            }
        }

        private void BindWarehousesToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _WareHouses = ViewState[ViewStateKeys.SearchResult] as List<WareHouseVM>;
                else
                {
                    _WareHouses = _warehouseRepository.GetAllWarehouses();
                    if (_WareHouses.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _WareHouses.Add(new WareHouseVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridWarehouse.DataSource = _WareHouses;
                gridWarehouse.DataBind();
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
                if (string.IsNullOrWhiteSpace(txtWarehouseSearch.Text.Trim()))
                    throw new ApplicationException("The warehouse name field is required.");
                _WareHouses = _warehouseRepository.SearchWarehouseByName(txtWarehouseSearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _WareHouses;
                BindWarehousesToGrid();
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridRowIndexToMinusOne();
            BindWarehousesToGrid();
        }

        protected void gridWarehouse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var warehouseID = gridWarehouse.DataKeys[e.RowIndex]["WareHouseID"].ToString();
                _warehouseRepository.DeleteWarehouse(int.Parse(warehouseID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindWarehousesToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridWarehouse_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var warehouseID = int.Parse(gridWarehouse.DataKeys[e.RowIndex]["WareHouseID"].ToString());
                TextBoxWarehouse = gridWarehouse.Rows[e.RowIndex].FindControl("txtUpdateWarehouse") as TextBox;
                DropDownLocation = gridWarehouse.Rows[e.RowIndex].FindControl("ddlEditLocation") as DropDownList;
                ValidateWarehouse();
                _warehouseRepository.UpdateWarehouse(new WareHouseVM() { WareHouseID = warehouseID, LocationID = int.Parse(DropDownLocation.SelectedItem.Value), Name = TextBoxWarehouse.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindWarehousesToGrid();
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

        protected void gridWarehouse_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridRowIndexToMinusOne();
            BindWarehousesToGrid();
        }

        protected void gridWarehouse_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridWarehouse.EditIndex = e.NewEditIndex;
                BindWarehousesToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetGridRowIndexToMinusOne()
        {
            gridWarehouse.EditIndex = -1;
        }

        protected void gridWarehouse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                    if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                        e.Row.Visible = false;

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    BindLocationDropDownList(e, "ddlFooterLocation", null);
                }

                if (e.Row.RowType == DataControlRowType.DataRow && gridWarehouse.EditIndex == e.Row.RowIndex)
                {
                    var selectedValue = gridWarehouse.DataKeys[e.Row.RowIndex].Values["LocationID"].ToString();
                    BindLocationDropDownList(e, "ddlEditLocation", selectedValue);
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindLocationDropDownList(GridViewRowEventArgs e, string dropDownID, string selectedValue)
        {
            try
            {
                var dropDownList = e.Row.FindControl(dropDownID) as DropDownList;
                dropDownList.DataSource = _Locations;
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

        protected void gridWarehouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridWarehouse.PageIndex = e.NewPageIndex;
            BindWarehousesToGrid();
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SetFooterData();
                ValidateWarehouse();
                _warehouseRepository.CreateNewWarehouse(new WareHouseVM() { Name = TextBoxWarehouse.Text.Trim(), LocationID = int.Parse(DropDownLocation.SelectedValue) });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindWarehousesToGrid();
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

        private void ValidateWarehouse()
        {
            if (TextBoxWarehouse == null || string.IsNullOrWhiteSpace(TextBoxWarehouse.Text))
                throw new ApplicationException("The warehouse name field is required.");
            if (DropDownLocation == null || DropDownLocation.SelectedIndex == 0)
                throw new ApplicationException("The location field is required.");
        }
    }
}