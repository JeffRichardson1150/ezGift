using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ezGift.WebMVC.Startup))]
namespace ezGift.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
