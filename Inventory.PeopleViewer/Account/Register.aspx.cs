using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Inventory.PeopleViewer.Models;
using Core.Common.ErrorMessages;

namespace Inventory.PeopleViewer.Account
{
    public partial class Register : Page
    {

        protected override void OnPreLoad(EventArgs e)
        {

            if (!IsPostBack)
            {
                SetTextBoxFocus();
                ClearControls();
            }
            base.OnPreLoad(e);
        }

        private void SetTextBoxFocus()
        {
            txtFirstName.Focus();
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (Page.IsValid)
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

                    var user = new ApplicationUser() { UserName = txtEmail.Text, Email = txtEmail.Text, FirstName = txtFirstName.Text, LastName = txtLastName.Text, Gender = ddlGender.SelectedIndex };
                    IdentityResult result = manager.Create(user, txtPassword.Text);
                    if (result.Succeeded)
                    {
                        lblInfo.Text = "<strong>User created successfully.</strong>";
                        lblInfo.CssClass = "text-success";
                        ClearControls();
                    }
                    else
                    {
                        lblInfo.Text = $"<strong>{result.Errors.FirstOrDefault()}</strong>";
                        lblInfo.CssClass = "text-danger";
                    }
                }
            }
            catch (ApplicationException Ae)
            {
                lblInfo.Text = $"<strong>{Ae.Message}</strong>";
                lblInfo.CssClass = "text-danger";
            }
            catch (Exception)
            {
                lblInfo.Text = $"<strong>{InformationHelper.GenericErrorMessage}</strong>";
                lblInfo.CssClass = "text-danger";
            }
        }


        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            SetTextBoxFocus();
            lblInfo.Text = string.Empty;
        }

        private void ClearControls()
        {
            txtConfirmPassword.Text = txtEmail.Text = txtFirstName.Text = txtLastName.Text = txtPassword.Text = string.Empty;
            ddlGender.ClearSelection();
        }
    }
}