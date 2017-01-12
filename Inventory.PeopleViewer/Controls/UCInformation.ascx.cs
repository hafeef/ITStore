using Core.Common.ErrorMessages;
using System;
using System.Web.UI;

namespace Inventory.PeopleViewer.Controls
{
    public partial class UCInformation : System.Web.UI.UserControl
    {
        private void RegisterClientScript()
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "script", "ClearInformationLablesAutomatically();", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearInformationLables();
        }

        public void ShowErrorMessage()
        {
            ShowErrorMessage(InformationHelper.GenericErrorMessage);
        }

        public void ShowErrorMessage(string errorMessage)
        {
            lblInformation.Text = $"<strong>{errorMessage}</strong>";
            lblInformation.CssClass = "text-danger col-md-offset-1";
            RegisterClientScript();
        }

        public void ShowSaveInfomationMessage()
        {
            ShowInformationMessage(InformationHelper.GenericSaveInformationMessage);
        }

        private void ShowInformationMessage(string message)
        {
            lblInformation.Text = $"<strong>{Page.Title}{message}</strong>";
            lblInformation.CssClass = "text-success col-md-offset-1";
            RegisterClientScript();
        }

        public void ShowModifyInfomationMessage()
        {
            ShowInformationMessage(InformationHelper.GenericModifyInformationMessage);
        }

        public void ShowDeleteInfomationMessage()
        {
            ShowInformationMessage(InformationHelper.GenericDeleteInformationMessage);
        }

        public void ClearInformationLables()
        {
            lblInformation.Text = string.Empty;
        }
    }
}