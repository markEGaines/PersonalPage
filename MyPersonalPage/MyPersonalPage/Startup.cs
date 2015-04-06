using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPersonalPage.Startup))]
namespace MyPersonalPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
