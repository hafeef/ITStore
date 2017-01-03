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
    public partial class Locations : System.Web.UI.Page
    {
        ILocationRepository _LocationRepository = new LocationRepository(new AdminContext());
        TextBox textBoxLocation = null;
        List<LocationVM> _Locations = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ClearFormData();
                    BindLocationDataToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var locationID = gridLocation.DataKeys[e.RowIndex]["LocationID"].ToString();
                _LocationRepository.DeleteLocation(int.Parse(locationID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindLocationDataToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridLocation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridLocation.EditIndex = e.NewEditIndex;
                BindLocationDataToGrid();
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
                ValidateLocation(txtLocationSearch);
                _Locations = _LocationRepository.SearchLocationByName(txtLocationSearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _Locations;
                BindLocationDataToGrid();
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

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SetFooterData();
                ValidateLocation(textBoxLocation);
                _LocationRepository.CreateNewLocation(new LocationVM() { Name = textBoxLocation.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindLocationDataToGrid();
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
            if (gridLocation.FooterRow != null)
            {
                textBoxLocation = gridLocation.FooterRow.FindControl("txtNewLocation") as TextBox;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridRowIndexToMinusOne();
            BindLocationDataToGrid();
        }

        protected void gridLocation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridRowIndexToMinusOne();
                BindLocationDataToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridLocation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var locationID = int.Parse(gridLocation.DataKeys[e.RowIndex]["LocationID"].ToString());
                textBoxLocation = gridLocation.Rows[e.RowIndex].FindControl("txtUpdateLocation") as TextBox;
                ValidateLocation(textBoxLocation);
                _LocationRepository.UpdateLocation(new LocationVM() { LocationID = locationID, Name = textBoxLocation.Text.Trim() });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindLocationDataToGrid();
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
            gridLocation.EditIndex = -1;
        }

        protected void gridLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLocation.PageIndex = e.NewPageIndex;
            BindLocationDataToGrid();
        }

        private void Page_Error(object sender, EventArgs e)
        {
            ucInformation.ShowErrorMessage();
            Server.ClearError();
        }

        private void BindLocationDataToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Locations = ViewState[ViewStateKeys.SearchResult] as List<LocationVM>;
                else
                {
                    _Locations = _LocationRepository.GetAllLocations();
                    if (_Locations.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Locations.Add(new LocationVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridLocation.DataSource = _Locations;
                gridLocation.DataBind();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void ClearFormData()
        {
            txtLocationSearch.Text = string.Empty;
            SetFooterData();
            if (textBoxLocation != null)
                textBoxLocation.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
        }

        protected void gridLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;

        }

        private void ValidateLocation(TextBox textBoxLocation)
        {
            if (textBoxLocation == null || string.IsNullOrWhiteSpace(textBoxLocation.Text.Trim()))
                throw new ApplicationException("The location field is required.");
        }
    }
}