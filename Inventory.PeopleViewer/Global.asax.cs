using Inventory.PeopleViewer.App_Start;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Inventory.PeopleViewer
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterAutoMpas();
        }
    }
}