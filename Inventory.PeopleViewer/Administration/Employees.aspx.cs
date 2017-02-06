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
    public partial class Employees : System.Web.UI.Page
    {
        IEmployeeRepository _EmployeeRepository = new EmployeeRepository(new AdminContext());
        TextBox TextBoxEmployeeName = null;
        TextBox TextBoxCivilID = null;
        List<EmployeeVM> _Employee = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucInformation.ClearInformationLables();
                if (!Page.IsPostBack)
                {
                    ClearFormData();
                    BindEmployeesToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void BindEmployeesToGrid()
        {
            try
            {
                if (ViewState[ViewStateKeys.SearchResult] != null)
                    _Employee = ViewState[ViewStateKeys.SearchResult] as List<EmployeeVM>;
                else
                {
                    _Employee = _EmployeeRepository.GetAllEmployees();
                    if (_Employee.Count == 0)
                    {
                        ViewState[ViewStateKeys.IsEmpty] = true;
                        _Employee.Add(new EmployeeVM() { });
                    }
                    else
                        ViewState[ViewStateKeys.IsEmpty] = false;
                }
                GridEmployee.DataSource = _Employee;
                GridEmployee.DataBind();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void ClearFormData()
        {
            txtEmployeeSearch.Text = string.Empty;
            ViewState[ViewStateKeys.SearchResult] = null;
            SetFooterData();
            if (TextBoxCivilID != null)
                TextBoxCivilID.Text = string.Empty;
            if (TextBoxEmployeeName != null)
                TextBoxEmployeeName.Text = string.Empty;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFormData();
            SetGridEditIndexToMinusOne();
            BindEmployeesToGrid();
            ucInformation.ClearInformationLables();
        }

        protected void GridEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var employeeID = GridEmployee.DataKeys[e.RowIndex]["EmployeeID"].ToString();
                _EmployeeRepository.DeleteEmployee(int.Parse(employeeID));
                SetGridEditIndexToMinusOne();
                ClearFormData();
                BindEmployeesToGrid();
                ucInformation.ShowDeleteInfomationMessage();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetGridEditIndexToMinusOne()
        {
            GridEmployee.EditIndex = -1;
        }

        protected void GridEmployee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var employeeID = int.Parse(GridEmployee.DataKeys[e.RowIndex]["EmployeeID"].ToString());
                    TextBoxEmployeeName = GridEmployee.Rows[e.RowIndex].FindControl("txtEmployeeName") as TextBox;
                    TextBoxCivilID = GridEmployee.Rows[e.RowIndex].FindControl("txtCivilID") as TextBox;
                    _EmployeeRepository.UpdateEmployee(new EmployeeVM() { EmployeeID = employeeID, Name = TextBoxEmployeeName.Text.Trim(), CivilID = TextBoxCivilID.Text.Trim() });
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindEmployeesToGrid();
                    ucInformation.ShowModifyInfomationMessage();
                }

            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                SetGridEditIndexToMinusOne();
                BindEmployeesToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEmployee.PageIndex = e.NewPageIndex;
            BindEmployeesToGrid();
        }

        protected void GridEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridEmployee.EditIndex = e.NewEditIndex;
                BindEmployeesToGrid();
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        protected void GridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToBoolean(ViewState[ViewStateKeys.IsEmpty]))
                if (e.Row.RowType == DataControlRowType.DataRow)
                    e.Row.Visible = false;
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    SetFooterData();
                    _EmployeeRepository.CreateEmployee(new EmployeeVM() { Name = TextBoxEmployeeName.Text.Trim(), CivilID = TextBoxCivilID.Text.Trim() });
                    ucInformation.ShowSaveInfomationMessage();
                    SetGridEditIndexToMinusOne();
                    ClearFormData();
                    BindEmployeesToGrid();
                }
            }
           
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }

        private void SetFooterData()
        {
            if (GridEmployee.FooterRow != null)
            {
                TextBoxCivilID = GridEmployee.FooterRow.FindControl("txtCivilID") as TextBox;
                TextBoxEmployeeName = GridEmployee.FooterRow.FindControl("txtEmployeeName") as TextBox;
            }

        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    _Employee = _EmployeeRepository.SearchEmployeeByName(txtEmployeeSearch.Text.Trim());
                    ViewState[ViewStateKeys.SearchResult] = _Employee;
                    BindEmployeesToGrid();
                }
            }
            catch (Exception)
            {
                ucInformation.ShowErrorMessage();
            }
        }
    }
}