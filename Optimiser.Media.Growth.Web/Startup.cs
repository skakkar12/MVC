using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Optimiser.Media.Growth.Web.Startup))]
namespace Optimiser.Media.Growth.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
