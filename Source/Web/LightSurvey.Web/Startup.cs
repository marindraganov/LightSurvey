using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LightSurvey.Web.Startup))]
namespace LightSurvey.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
