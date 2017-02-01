using Inventory.PeopleViewer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Security;

namespace Inventory.PeopleViewer.Account
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ClearControls();
        }

        protected void linkButtonLogin_Click(object sender, EventArgs e)
        {

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            var userIdentiry = manager.CreateIdentity(manager.FindByName(Email.Text), DefaultAuthenticationTypes.ApplicationCookie);

            var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                        FormsAuthentication.RedirectFromLoginPage(Email.Text, RememberMe.Checked);
                    break;

                default:
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                    break;
            }

        }

        protected void linkButtonReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            Email.Text = Password.Text = string.Empty;
            ViewState.Clear();
        }
    }
}