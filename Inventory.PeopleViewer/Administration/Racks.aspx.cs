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
    public partial class Racks : System.Web.UI.Page
    {
        IRackRepository _rackRepository = new RackRepository(new AdminContext());
        List<RackVM> _racks = null;
        TextBox txtRackName = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRacksToGrid();
                ClearFormData();
            }
        }

        private void ClearFormData()
        {
            txtRackSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (txtRackName != null)
                txtRackName.Text = string.Empty;
        }

        private void SetFooterData()
        {
            if (gridRack.FooterRow != null)
                txtRackName = gridRack.FooterRow.FindControl("txtNewRack") as TextBox;
        }

        private void BindRacksToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                {
                    _racks = ViewState[ViewStateKeys.SearchResult] as List<RackVM>;
                }

                else
                {
                    _racks = _rackRepository.GetAllRacks();
                    if (_racks.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _racks.Add(new RackVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridRack.DataSource = _racks;
                gridRack.DataBind();
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
                    _rackRepository.CreateNewRack(new RackVM() { Name = txtRackName.Text });
                    SetGridEditIndexToMinusOne();
                    BindRacksToGrid();
                    ClearFormData();
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
            gridRack.EditIndex = -1;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridEditIndexToMinusOne();
            BindRacksToGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    _racks = _rackRepository.SearchRackByName(txtRackSearch.Text.Trim());
                    ViewState[ViewStateKeys.SearchResult] = _racks;
                    BindRacksToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridRack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var rackID = gridRack.DataKeys[e.RowIndex]["RackID"].ToString();
                _rackRepository.DeleteRack(int.Parse(rackID));
                SetGridEditIndexToMinusOne();
                BindRacksToGrid();
                ClearFormData();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridRack_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var rackID = int.Parse(gridRack.DataKeys[e.RowIndex]["RackID"].ToString());
                    txtRackName = gridRack.Rows[e.RowIndex].FindControl("txtUpdateRack") as TextBox;
                    _rackRepository.UpdateRack(new RackVM() { RackID = rackID, Name = txtRackName.Text.Trim() });
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindRacksToGrid();
                    ucInformation.ShowModifyInfomationMessage();
                }
            }

            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridRack_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridEditIndexToMinusOne();
            BindRacksToGrid();
        }

        protected void gridRack_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRack.PageIndex = e.NewPageIndex;
            BindRacksToGrid();
        }

        protected void gridRack_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridRack.EditIndex = e.NewEditIndex;
            BindRacksToGrid();
        }

        protected void gridRack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }
    }
}