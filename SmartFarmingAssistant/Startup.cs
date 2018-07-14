using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartFarmingAssistant.Startup))]
namespace SmartFarmingAssistant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
