using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proekt2.Startup))]
namespace proekt2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
