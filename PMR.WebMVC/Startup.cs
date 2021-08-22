using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PMR.WebMVC.Startup))]
namespace PMR.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
