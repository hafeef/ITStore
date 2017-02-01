using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Inventory.PeopleViewer.Models;

namespace Inventory.PeopleViewer.Account
{
    public partial class Register : Page
    {
        protected void linkButtonSearch_Click(object sender, EventArgs e)
        {

        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                ValidateUser();
                var user = new ApplicationUser() { UserName = txtEmail.Text, Email = txtEmail.Text, FirstName = txtFirstName.Text, LastName = txtLastName.Text, Gender = ddlGender.SelectedIndex };
                IdentityResult result = manager.Create(user, txtPassword.Text);
                if (result.Succeeded)
                {
                    lblInfo.Text = "<strong>User created successfully.</strong>";
                    ClearControls();
                }
                else
                {
                    ucInformation.ShowErrorMessage(result.Errors.FirstOrDefault());
                }
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

        private void ValidateUser()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                throw new ApplicationException("The email field is required.");
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                throw new ApplicationException("The first field is required.");
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
                throw new ApplicationException("The last name field is required.");
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
                throw new ApplicationException("The password field is required.");
            if (ddlGender.SelectedIndex == 0)
                throw new ApplicationException("The gender field is required.");
        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearControls();
            lblInfo.Text = string.Empty;
        }

        private void ClearControls()
        {
            txtConfirmPassword.Text = txtEmail.Text = txtFirstName.Text = txtLastName.Text = txtPassword.Text = string.Empty;
            ddlGender.ClearSelection();
        }
    }
}