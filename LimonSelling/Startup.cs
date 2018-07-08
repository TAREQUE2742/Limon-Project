using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LimonSelling.Startup))]
namespace LimonSelling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
