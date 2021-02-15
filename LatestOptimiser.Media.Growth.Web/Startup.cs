using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LatestOptimiser.Media.Growth.Web.Startup))]
namespace LatestOptimiser.Media.Growth.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
