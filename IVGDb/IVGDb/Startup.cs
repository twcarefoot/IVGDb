using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IVGDb.Startup))]
namespace IVGDb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
