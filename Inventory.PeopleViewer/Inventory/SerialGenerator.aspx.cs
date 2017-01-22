using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.PeopleViewer.Inventory
{
    public partial class SerialGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateSerials(object sender, EventArgs e)
        {
            for (int i = 0; i < int.Parse(txtNumberOfSerials.Text); i++)
            {
                Response.Write(Guid.NewGuid());
                Response.Write("<br/>");
            }
        }


    }
}