using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quotably.WebMVC.Startup))]
namespace Quotably.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
