using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SnapTextWeb.Startup))]
namespace SnapTextWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
