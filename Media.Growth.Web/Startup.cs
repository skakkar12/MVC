using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Media.Growth.Web.Startup))]
namespace Media.Growth.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
