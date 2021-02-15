using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Media.Growth.OptimiserWeb.Startup))]
namespace Media.Growth.OptimiserWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
