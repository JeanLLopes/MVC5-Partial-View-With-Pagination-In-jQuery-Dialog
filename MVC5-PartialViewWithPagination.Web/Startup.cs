using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5_PartialViewWithPagination.Web.Startup))]
namespace MVC5_PartialViewWithPagination.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
