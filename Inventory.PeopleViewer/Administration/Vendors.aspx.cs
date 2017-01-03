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
    public partial class Vendors : System.Web.UI.Page
    {
        IVendorRepository _vendorRepository = new VendorRepository(new AdminContext());
        List<VendorVM> _Vendors = null;

        TextBox txtVendorName = null;
        TextBox txtEmail = null;
        TextBox txtMobileNo = null;
        TextBox txtTelephoneNo = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.IsPostBack)
                {
                    BindVendorsToGrid();
                    ClearFormData();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void ClearFormData()
        {
            txtVendorSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            ClearFooterData();
        }

        private void ClearFooterData()
        {
            if (txtEmail != null)
                txtEmail.Text = string.Empty;
            if (txtVendorName != null)
                txtVendorName.Text = string.Empty;
            if (txtMobileNo != null)
                txtMobileNo.Text = string.Empty;
            if (txtTelephoneNo != null)
                txtTelephoneNo.Text = string.Empty;
        }

        private void SetFooterData()
        {
            var footerRow = gridVendor.FooterRow;
            if (footerRow != null)
            {
                txtVendorName = footerRow.FindControl("txtNewVendor") as TextBox;
                txtMobileNo = footerRow.FindControl("txtNewMobileNo") as TextBox;
                txtEmail = footerRow.FindControl("txtNewEmail") as TextBox;
                txtTelephoneNo = footerRow.FindControl("txtNewTelePhoneNo") as TextBox;
            }
        }

        private void BindVendorsToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Vendors = ViewState[ViewStateKeys.SearchResult] as List<VendorVM>;
                else
                {
                    _Vendors = _vendorRepository.GetAllVendors().ToList();
                    if (_Vendors.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Vendors.Add(new VendorVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                gridVendor.DataSource = _Vendors;
                gridVendor.DataBind();
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
                if (string.IsNullOrWhiteSpace(txtVendorSearch.Text))
                    throw new ApplicationException("The vendor name field is required.");
                _Vendors = _vendorRepository.SearchVendorByName(txtVendorSearch.Text.Trim());
                ViewState[ViewStateKeys.SearchResult] = _Vendors;
                BindVendorsToGrid();
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
            BindVendorsToGrid();
        }

        private void SetGridRowIndexToMinusOne()
        {
            gridVendor.EditIndex = -1;
        }

        protected void gridVendor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var vendorID = gridVendor.DataKeys[e.RowIndex]["VendorID"].ToString();
                txtVendorName = gridVendor.Rows[e.RowIndex].FindControl("txtUpdateVendor") as TextBox;
                txtMobileNo = gridVendor.Rows[e.RowIndex].FindControl("txtUpdateMobileNo") as TextBox;
                txtEmail = gridVendor.Rows[e.RowIndex].FindControl("txtUpdateEmail") as TextBox;
                txtTelephoneNo = gridVendor.Rows[e.RowIndex].FindControl("txtUpdateTelePhoneNo") as TextBox;
                ValidateVendor();
                _vendorRepository.UpdateVendor(new VendorVM()
                {
                    VendorID = int.Parse(vendorID),
                    Name = txtVendorName.Text,
                    Email = txtEmail.Text,
                    MobileNo = txtMobileNo.Text,
                    TelephoneNo = txtTelephoneNo.Text
                });
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindVendorsToGrid();
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

        protected void gridVendor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var vendorID = gridVendor.DataKeys[e.RowIndex]["VendorID"].ToString();
                _vendorRepository.DeleteVendor(int.Parse(vendorID));
                SetGridRowIndexToMinusOne();
                ClearFormData();
                BindVendorsToGrid();
                ucInformation.ShowDeleteInfomationMessage();
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

        protected void gridVendor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridRowIndexToMinusOne();
                BindVendorsToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridVendor.PageIndex = e.NewPageIndex;
                SetGridRowIndexToMinusOne();
                BindVendorsToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridVendor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                    if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                        e.Row.Visible = false;

            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void gridVendor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridVendor.EditIndex = e.NewEditIndex;
                BindVendorsToGrid();
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
                ValidateVendor();
                _vendorRepository.CreateNewVendor(new VendorVM()
                {
                    Email = txtEmail.Text,
                    MobileNo = txtMobileNo.Text,
                    Name = txtVendorName.Text,
                    TelephoneNo = txtTelephoneNo.Text
                });
                SetGridRowIndexToMinusOne();
                BindVendorsToGrid();
                ClearFormData();
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

        private void ValidateVendor()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail == null)
                throw new ApplicationException("The email field is required.");
            if (string.IsNullOrWhiteSpace(txtMobileNo.Text) || txtMobileNo == null)
                throw new ApplicationException("The mobile no field is required.");
            if (string.IsNullOrWhiteSpace(txtTelephoneNo.Text) || txtTelephoneNo == null)
                throw new ApplicationException("The telephone no field is required.");
            if (string.IsNullOrWhiteSpace(txtVendorName.Text) || txtVendorName == null)
                throw new ApplicationException("The vendor name field is required.");
        }
    }
}