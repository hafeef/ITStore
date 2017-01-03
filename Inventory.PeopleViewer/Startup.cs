using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inventory.PeopleViewer.Startup))]
namespace Inventory.PeopleViewer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
