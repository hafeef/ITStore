using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Inventory.PeopleViewer.Models;

namespace Inventory.PeopleViewer.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(txtEmail.Text);
                if (user == null || !manager.IsEmailConfirmed(user.Id))
                {
                    ucInformation.ShowErrorMessage("The user does not exist.");
                    return;
                }
            }
        }

        protected void linkButtonSave_Click(object sender, EventArgs e)
        {

        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            txtEmail.Text = txtConfirmPassword.Text = txtPassword.Text = string.Empty;
        }
    }
}