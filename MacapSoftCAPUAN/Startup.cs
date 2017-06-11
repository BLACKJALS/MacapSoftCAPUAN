using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MacapSoftCAPUAN.Startup))]
namespace MacapSoftCAPUAN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
