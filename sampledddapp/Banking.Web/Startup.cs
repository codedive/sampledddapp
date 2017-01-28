using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banking.Web.Startup))]
namespace Banking.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
